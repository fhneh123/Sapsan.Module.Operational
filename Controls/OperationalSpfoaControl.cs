using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraPrinting;
using Sapsan.Modules.Operational.Dialogs;
using Sapsan.Modules.Operational.Enums;
using Sapsan.Modules.Operational.Presenters.Contracts;
using Sapsan.Modules.Operational.Presenters.Data;
using Sapsan.Modules.Operational.Services.Contracts;
using Sapsan.Modules.Operational.Services.Data;
using Sapsan2.Core.Contracts;
using Sapsan2.Core.Service.Contracts;
using SUVPP.UI.Controls;

namespace Sapsan.Modules.Operational.Controls
{
	public partial class OperationalSpfoaControl : UserControl
	{
		private IErrorCatcher _errorCatcher;
		private IApplicationPathService _applicationPathService;
		private ILogService _logService;
		private IUserService _userService;
		private IOperationalEntityStatusService _operationalWorkStatusService;
		private IOperationalSpfoaPresenter _operationalSpfoaPresenter;
		private IOperationalSpfoaEditorService _operationalSpfoaEditorService;
		private IOperationalService _operationalService;
		private IOperationalWorkService _operationalWorkService;
		private IOperationalSpfoaEditorPresenter _operationalSpfoaEditorPresenter;
		private IOperationalSpfoaService _operationalSpfoaService;
		private IIdentityService _identityService;

		private GridControlLayoutManager _layoutManager;

		private List<OperationalEntityStatusData> _statusDatas;
		private RepositoryItemSearchLookUpEdit _statusExecuteRepositoryItem;

		public bool IsInitUi { get; private set; }

		public object InitializeUIControl(
			IErrorCatcher errorCatcher,
			IApplicationPathService applicationPathService,
			ILogService logService,
			IUserService userService,
			IOperationalEntityStatusService operationalWorkStatusService,
			IOperationalSpfoaPresenter operationalSpfoaPresenter,
			IOperationalSpfoaEditorService operationalSpfoaEditorService,
			IOperationalService operationalService,
			IOperationalWorkService operationalWorkService,
			IOperationalSpfoaEditorPresenter operationalSpfoaEditorPresenter,
			IOperationalSpfoaService operationalSpfoaService,
			IIdentityService identityService)
		{
			if (!IsInitUi)
			{
				_errorCatcher = errorCatcher;
				_applicationPathService = applicationPathService;
				_logService = logService;
				_userService = userService;
				_operationalWorkStatusService = operationalWorkStatusService;
				_operationalSpfoaPresenter = operationalSpfoaPresenter;
				_operationalSpfoaEditorService = operationalSpfoaEditorService;
				_operationalService = operationalService;
				_operationalWorkService = operationalWorkService;
				_operationalSpfoaEditorPresenter = operationalSpfoaEditorPresenter;
				_operationalSpfoaService = operationalSpfoaService;
				_identityService = identityService;

				InitializeComponent();

				_layoutManager = new GridControlLayoutManager(null, gridControl, gridView, "OperationalSpfoa", null,
					(x, y) => { LoadData(); });

				LoadData();

				SetBandName();

				if (_identityService.IsInRole(OperationalRoleList.Добавление_редактирование) == false)
				{
					btnSpfoaEditor.Enabled = false;
				}

				IsInitUi = true;
			}

			return this;
		}

		public void LoadData()
		{
			var dataSource = _operationalSpfoaPresenter.GetData();

			_layoutManager.DataSource = dataSource;

			RefreshCacheData();
		}

		private void RefreshCacheData()
		{
			_statusDatas = _operationalWorkStatusService.GetList().ToList();

			bandedGridColumnExecuteStatusId.ColumnEdit = GetStatusRepositoryItemWithoutDataSource();
		}

		private RepositoryItemSearchLookUpEdit GetStatusRepositoryItemWithoutDataSource()
		{
			var repositoryItem = new RepositoryItemSearchLookUpEdit();
			repositoryItem.ActionButtonIndex = 1;
			repositoryItem.DisplayMember = nameof(SimpleModel.Name);
			repositoryItem.NullText = string.Empty;
			repositoryItem.ValueMember = nameof(SimpleModel.Id);

			var datasource = _statusDatas.Where(x => x.EntityId == OperationalStatusEntityIdEnum.ExecuteStatusEntityId
													 && x.OperationalWorkId.HasValue == false)
				.Select(x => new SimpleModel
				{
					Id = x.Id,
					Name = x.Name
				}).ToList();
			repositoryItem.DataSource = datasource;

			repositoryItem.PopulateViewColumns();
			repositoryItem.View.Columns[nameof(SimpleModel.Id)].Visible = false;
			repositoryItem.View.Columns[nameof(SimpleModel.Name)].Caption = "Статус";


			return repositoryItem;
		}

		private void SetBandName()
		{
			string additionalSymbolInBandHidden;
			foreach (GridBand band in gridView.Bands)
			{
				additionalSymbolInBandHidden = "-";
				foreach (BandedGridColumn column in band.Columns)
				{
					if (column.Visible == false)
					{
						additionalSymbolInBandHidden = "+";
						break;
					}
				}

				band.Caption = $"[{additionalSymbolInBandHidden}] {band.Caption}";
			}
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			_errorCatcher.Do(() =>
			{
				var fileName = Path.Combine(_applicationPathService.GetLocalTempPath(),
					$"{Guid.NewGuid().ToString()}.xlsx");

				gridControl.ExportToXlsx(fileName, new XlsxExportOptions(TextExportMode.Value, true, true));
				if (File.Exists(fileName))
				{
					System.Diagnostics.Process.Start(fileName);
				}
			});
		}

		private void btnSpfoaEditor_Click(object sender, EventArgs e)
		{
			_errorCatcher.Do(() =>
			{
				var d = new SpfoaEditorDialog();
				d.Init(
					_userService,
					_logService,
					_errorCatcher,
					_operationalWorkStatusService,
					_operationalSpfoaEditorService,
					_operationalSpfoaEditorPresenter,
					_operationalService,
					_operationalWorkService);

				d.LoadData();
				d.ShowDialog();

				LoadData();
			});
		}

		private void gridView_CellValueChanged(object sender,
			DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
			_errorCatcher.Do(() =>
			{
				var row = (OperationalSpfoaPresenterData)gridView.GetRow(e.RowHandle);

				if (row == null)
				{
					return;
				}

				var data = new OperationalSpfoaData
				{
					ExecuteStatusId = row.ExecuteStatusId,
					DtActirovania = row.DtActirovania,
					RemarkKoii = row.RemarkKoii
				};

				_operationalSpfoaService.Update(row.Id, data);
			});
		}

		private void gridView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
		{
			if (e.Column != bandedGridColumnExecuteStatusId && e.Column != bandedGridColumnStatusIgdi
															&& e.Column != bandedGridColumnStatusIgmi &&
															e.Column != bandedGridColumnStatusIgi
															&& e.Column != bandedGridColumnStatusIei &&
															e.Column != bandedGridColumnEconomy &&
															e.Column != bandedGridColumnStatusKoii)
			{
				return;
			}

			var row = (OperationalSpfoaPresenterData)gridView.GetRow(e.RowHandle);

			if (row == null)
			{
				return;
			}

			var statusColor = string.Empty;

			if (e.Column == bandedGridColumnExecuteStatusId)
			{
				statusColor = _statusDatas.SingleOrDefault(x => x.Id == row.ExecuteStatusId)?.Color;
			}

			if (e.Column == bandedGridColumnStatusIgdi)
			{
				statusColor = _statusDatas.SingleOrDefault(x => x.Id == row.StatusIgdiId)?.Color;
			}

			if (e.Column == bandedGridColumnStatusIgmi)
			{
				statusColor = _statusDatas.SingleOrDefault(x => x.Id == row.StatusIgmiId)?.Color;
			}

			if (e.Column == bandedGridColumnStatusIgi)
			{
				statusColor = _statusDatas.SingleOrDefault(x => x.Id == row.StatusIgiId)?.Color;
			}

			if (e.Column == bandedGridColumnStatusIei)
			{
				statusColor = _statusDatas.SingleOrDefault(x => x.Id == row.StatusIeiId)?.Color;
			}

			if (e.Column == bandedGridColumnStatusKoii)
			{
				statusColor = _statusDatas.SingleOrDefault(x => x.Id == row.StatusKoiiId)?.Color;
			}

			if (e.Column == bandedGridColumnEconomy)
			{
				if ((double?)e.CellValue == 0)
				{
					return;
				}

				e.Appearance.BackColor = (double?)e.CellValue > 0 ? Color.Green : Color.Red;
			}

			if (string.IsNullOrEmpty(statusColor))
			{
				return;
			}

			e.Appearance.BackColor = ColorTranslator.FromHtml(statusColor);
		}

		private void gridView_MouseUp(object sender, MouseEventArgs e)
		{
			var hitInfo = gridView.CalcHitInfo(new Point(e.X, e.Y));
			if (hitInfo.InBandPanel)
			{
				if (hitInfo.Band.Caption.Contains("[+]"))
				{
					foreach (BandedGridColumn column in hitInfo.Band.Columns)
					{
						column.Visible = true;
					}

					hitInfo.Band.Caption = hitInfo.Band.Caption.Replace("[+]", "[-]");
				}
				else
				{
					if (hitInfo.Band.Caption != "[-] Общие данные")
					{
						var hasColumnVisible = false;
						foreach (BandedGridColumn column in hitInfo.Band.Columns)
						{
							if (column != bandedGridColumnStatusIei &&
								column != bandedGridColumnStatusIgdi &&
								column != bandedGridColumnStatusIgi &&
								column != bandedGridColumnStatusIgmi &&
								column != bandedGridColumnStatusKoii &&
								column != bandedGridColumnPlanPriceResult &&
								column != bandedGridColumnExecutePriceResult)
							{
								column.Visible = false;
							}
							else
							{
								hasColumnVisible = true;
							}
						}
						if (hasColumnVisible == false)
						{
							hitInfo.Band.Columns[0].Visible = true;
						}
					}
					else
					{
						foreach (BandedGridColumn column in hitInfo.Band.Columns)
						{
							if (column != bandedGridColumnContractShifr
								&& column != bandedGridColumnContractName
								&& column != bandedGridColumnCustomer
								&& column != bandedGridColumnExecuteStatusId
								&& column != bandedGridColumnStageNumber)
							{
								column.Visible = false;
							}
						}
					}

					hitInfo.Band.Caption = hitInfo.Band.Caption.Replace("[-]", "[+]");
				}
			}
		}

		private class SimpleModel
		{
			public Guid Id { get; set; }
			public string Name { get; set; }
		}

		private void историяToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_errorCatcher.Do(() =>
			{
				var operationalLine = (OperationalSpfoaPresenterData)gridView.GetFocusedRow();

				var d = new SUVPP.UI.Dialogs.Common.HistoryDialog(_logService, _userService);
				d.Init("История", operationalLine.Id);
				d.ShowDialog();
			});
		}

		private void gridView_CustomDrawBandHeader(object sender, BandHeaderCustomDrawEventArgs e)
		{
			if (e.Band == null)
			{
				return;
			}

			if (e.Band.AppearanceHeader.BackColor != Color.Empty)
			{
				e.Info.AllowColoring = true;
			}
		}
	}
}