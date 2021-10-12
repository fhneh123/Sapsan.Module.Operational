using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using Sapsan.Modules.Operational.Dialogs;
using Sapsan.Modules.Operational.Enums;
using Sapsan.Modules.Operational.Presenters.Contracts;
using Sapsan.Modules.Operational.Presenters.Data;
using Sapsan.Modules.Operational.Services.Contracts;
using Sapsan.Modules.Operational.Services.Data;
using Sapsan.Modules.ProjectScheduler.Enums;
using Sapsan.Modules.ProjectScheduler.Services;
using Sapsan2.Contracts.Enums;
using Sapsan2.Contracts.Exceptions;
using Sapsan2.Core.Contracts;
using Sapsan2.Core.Service.Contracts;
using SUVPP.UI.Controls;

namespace Sapsan.Modules.Operational.Controls
{
	public partial class OperationalControl : UserControl
	{
		private IOperationalLineService _operationalLineService;
		private IOperationalLinePresenter _operationalLinePresenter;
		private IOperationalService _operationalService;
		private IOperationalWorkService _operationalWorkService;
		private IOperationalWorkLineService _operationalWorkLineService;
		private IErrorCatcher _errorCatcher;
		private ILogService _logService;
		private IUserService _userService;
		private PlanService _planService;
		private WorkStructureService _workStructureService;
		private TaskService _taskService;
		private IIdentityService _identityService;
		private IContractService _contractService;
		private IDesignBasicService _designBasicService;
		private IOperationalEntityStatusService _operationalWorkStatusService;
		private IApplicationPathService _applicationPathService;
		private IOperationalWorkPresenter _operationalWorkPresenter;
		private ISubdivisionService _subdivisionService;
		private IUserSettingsService _userSettingsService;
		private IOperationalTypeTaskService _operationalTypeTaskService;

		private GridControlLayoutManager _layoutManager;

		private List<OperationalLinePresenterData> _operationalLineDatas;
		private List<OperationalWorkLineData> _workLineDatas;
		private OperationalEntityStatusData[] _statusDatas;
		private List<SimpleTaskModel> _taskDatas;
		private List<SimpleTaskModel> _allPgTasks;
		private List<SimpleUserModel> _userDatas;
		private List<LayoutManagerModel> _layoutManagerModels = new List<LayoutManagerModel>();
		private RepositoryItem _usersResponsibleRepositoryItem;
		private RepositoryItem _usersMainSpecialistRepositoryItem;

		private List<StatusRiModel> _statusRiModel;
		private Dictionary<Guid, RepositoryItem> _riComplinceWork;
		private Dictionary<Guid, RepositoryItem> _riComplinceWorkAllWork;

		private string _lastSelectedOperationalId = "lastSelectedOperationalId";


		public bool IsInitUi { get; private set; }

		public object InitializeUIControl(
			IOperationalLineService operationalLineService,
			IOperationalLinePresenter operationalLinePresenter,
			IOperationalService operationalService,
			IOperationalWorkService operationalWorkService,
			IOperationalWorkLineService operationalWorkLineService,
			IErrorCatcher errorCatcher,
			ILogService logService,
			IUserService userService,
			PlanService planService,
			WorkStructureService workStructureService,
			TaskService taskService,
			IIdentityService identityService,
			IContractService contractService,
			IDesignBasicService designBasicService,
			IOperationalEntityStatusService operationalWorkStatusService,
			IApplicationPathService applicationPathService,
			IOperationalWorkPresenter operationalWorkPresenter,
			ISubdivisionService subdivisionService,
			IUserSettingsService userSettingsService,
			IOperationalTypeTaskService operationalTypeTaskService)
		{
			if (!IsInitUi)
			{
				_operationalLineService = operationalLineService;
				_operationalLinePresenter = operationalLinePresenter;
				_operationalService = operationalService;
				_operationalWorkService = operationalWorkService;
				_operationalWorkLineService = operationalWorkLineService;
				_errorCatcher = errorCatcher;
				_logService = logService;
				_userService = userService;
				_planService = planService;
				_workStructureService = workStructureService;
				_taskService = taskService;
				_identityService = identityService;
				_contractService = contractService;
				_designBasicService = designBasicService;
				_operationalWorkStatusService = operationalWorkStatusService;
				_applicationPathService = applicationPathService;
				_operationalWorkPresenter = operationalWorkPresenter;
				_subdivisionService = subdivisionService;
				_userSettingsService = userSettingsService;
				_operationalTypeTaskService = operationalTypeTaskService;

				InitializeComponent();


				RefreshRarelyChangedCashDatas();

				SetCacheGridControlLayoutManagers();

				comboBoxOperational.DisplayMember = nameof(OperationalData.Name);
				comboBoxOperational.ValueMember = nameof(OperationalData.Id);
				comboBoxOperational.DataSource =
					_operationalService.GetList().Where(x => x.DtDelete.HasValue == false).ToList();
				comboBoxOperational.SelectedValueChanged += comboBoxOperational_SelectedValueChanged;
				if (Guid.TryParse(_userSettingsService.GetString(_lastSelectedOperationalId),
					out var lastSelectedOperationalId))
				{
					if (comboBoxOperational.SelectedValue != null &&
						(Guid)comboBoxOperational.SelectedValue == lastSelectedOperationalId)
					{
						RefreshOperational();
					}
					else
					{
						comboBoxOperational.SelectedValue = lastSelectedOperationalId;
					}
				}
				else
				{
					RefreshOperational();
				}


				if (_identityService.IsInRole(OperationalRoleList.Добавление_редактирование) == false)
				{
					btnAdd.Enabled = simpleButtonEditWork.Enabled = false;
				}

				IsInitUi = true;
			}

			return this;
		}


		public void LoadData()
		{
			if (comboBoxOperational.SelectedValue == null)
			{
				return;
			}

			_operationalLineDatas =
				_operationalLinePresenter.GetData((Guid)comboBoxOperational.SelectedValue, checkBoxAnnul.Checked);

			RefreshTasksData();
			RefreshWorkLineDatas();

			_layoutManager.DataSource = _operationalLineDatas;
		}


		private void RefreshWorkLineDatas()
		{
			if (comboBoxOperational.SelectedValue != null)
			{
				_workLineDatas = _operationalLinePresenter.GetWorkLines((Guid)comboBoxOperational.SelectedValue);
			}
			else
			{
				_workLineDatas = new List<OperationalWorkLineData>();
			}
		}

		private void RefreshRarelyChangedCashDatas()
		{
			RefreshStatusDatas();

			_userDatas = (from u in _userService.GetList()
						  join s in _subdivisionService.GetList() on u.OtdelSubdivisionId equals s.Id into sy
						  from s in sy.DefaultIfEmpty()
						  orderby u.OtdelSubdivisionId != _identityService.OtdelSubdivisionId, u.SubdivisionId, u.Fio
						  select new SimpleUserModel
						  {
							  Id = u.Id,
							  Fio = u.Fio,
							  OtdelSubdivisionId = u.OtdelSubdivisionId,
							  OtdelSubdivision = s.ShortName,
							  Position = u.Position,
							  PositionId = u.PositionId
						  }).ToList();

			_usersResponsibleRepositoryItem = GetUserRepositoryItem(_userDatas);

			var dataSource = _userDatas.Where(x => x.PositionId == PositionIds.MainSpecialist).ToList();

			_usersMainSpecialistRepositoryItem = GetUserRepositoryItem(dataSource);
		}

		private void RefreshStatusDatas()
		{
			_statusDatas = _operationalWorkStatusService.GetList().Where(x => x.OperationalWorkId.HasValue).ToArray();

			_statusRiModel = new List<StatusRiModel>();

			foreach (var work in _operationalWorkService.GetList().ToArray())
			{
				_statusRiModel.Add(new StatusRiModel
				{
					WorkId = work.Id,
					ColumnName = nameof(OperationalWorkLineData.Status),
					Repository = GetStatusRepositoryItem(work.Id, nameof(OperationalWorkLineData.Status))
				});

				_statusRiModel.Add(new StatusRiModel
				{
					WorkId = work.Id,
					ColumnName = nameof(OperationalWorkLineData.Reason),
					Repository = GetStatusRepositoryItem(work.Id, nameof(OperationalWorkLineData.Reason))
				});
			}
		}

		private RepositoryItem GetStatusRepositoryItem(Guid workId, string columnName)
		{
			var datasource = _statusDatas.Where(x => x.OperationalWorkId == workId
													 && x.EntityId == columnName).Select(x => new SimpleModel
													 {
														 Name = x.Name
													 }).ToList();

			if (datasource.Count() == 0)
			{
				return new RepositoryItemTextEdit();
			}

			var repositoryItem = new RepositoryItemSearchLookUpEdit();
			repositoryItem.ActionButtonIndex = 1;
			repositoryItem.DisplayMember = nameof(SimpleModel.Name);
			repositoryItem.NullText = string.Empty;
			repositoryItem.ValueMember = nameof(SimpleModel.Name);
			repositoryItem.DataSource = datasource;
			repositoryItem.PopulateViewColumns();
			repositoryItem.View.Columns[nameof(SimpleModel.Id)].Visible = false;
			repositoryItem.View.Columns[nameof(SimpleModel.Name)].Caption = "Статус";
			return repositoryItem;
		}

		private void RefreshTasksData()
		{
			var operationalId = (Guid)comboBoxOperational.SelectedValue;
			var complianceGroup = _operationalLineDatas.Select(y => y.ComplianceGroupId);
			_taskDatas = _taskService.GetList().Where(x => x.Type == (int)TaskType.работа && complianceGroup.Contains(x.WorkId)).Select(x =>
						   new SimpleTaskModel
						   {
							   Id = x.Id,
							   Name = x.Name,
							   WorkId = x.WorkId
						   }).ToList();

			_riComplinceWork = new Dictionary<Guid, RepositoryItem>();
			foreach (var complianceGroupId in _operationalLineDatas.Select(y => y.ComplianceGroupId).Distinct())
			{
				var dataSource = _taskDatas.Where(x => x.WorkId == complianceGroupId).Select(x => new SimpleTaskModel
				{
					Id = x.Id,
					Name = x.Name,
					WorkId = x.WorkId
				}).ToList();

				var repositoryItem = CreateTaskRi(dataSource);

				_riComplinceWork.Add(complianceGroupId, repositoryItem);
			}


			var planIds = _operationalLineDatas.Select(y => y.PlanId);
			_allPgTasks = (from w in _workStructureService.GetList()
						   join t in _taskService.GetList() on w.Id equals t.WorkId
						   where t.Type == (int)TaskType.работа && planIds.Contains(w.PlanId)
						   select new SimpleTaskModel
						   {
							   Id = t.Id,
							   Name = w.Name + ". " + t.Name,
							   PlanId = w.PlanId
						   }).ToList();
			_riComplinceWorkAllWork = new Dictionary<Guid, RepositoryItem>();

			foreach (var planId in _operationalLineDatas.Select(y => y.PlanId).Distinct())
			{
				var dataSource = _allPgTasks.Where(x => x.PlanId == planId).Select(x => new SimpleModel
				{
					Id = x.Id,
					Name = x.Name
				}).ToList();

				var repositoryItem = CreateTaskRi(dataSource);

				_riComplinceWorkAllWork.Add(planId, repositoryItem);
			}
		}

		private RepositoryItem CreateTaskRi(object dataSource)
		{
			var repositoryItem = new RepositoryItemSearchLookUpEdit();
			repositoryItem.ActionButtonIndex = 1;
			repositoryItem.DisplayMember = nameof(SimpleTaskModel.Name);
			repositoryItem.NullText = string.Empty;
			repositoryItem.ValueMember = nameof(SimpleTaskModel.Id);

			repositoryItem.DataSource = dataSource;

			repositoryItem.PopulateViewColumns();
			repositoryItem.View.Columns[nameof(SimpleTaskModel.Id)].Visible = false;
			repositoryItem.View.Columns[nameof(SimpleTaskModel.Name)].Caption = "Работа";

			return repositoryItem;
		}

		private RepositoryItem GetUserRepositoryItem(List<SimpleUserModel> dataSource)
		{
			var repositoryItem = new RepositoryItemSearchLookUpEdit();
			repositoryItem.ActionButtonIndex = 1;
			repositoryItem.DisplayMember = nameof(SimpleUserModel.Fio);
			repositoryItem.NullText = string.Empty;
			repositoryItem.ValueMember = nameof(SimpleUserModel.Id);

			repositoryItem.DataSource = dataSource.ToArray();
			repositoryItem.PopulateViewColumns();

			repositoryItem.View.Columns[nameof(SimpleUserModel.Id)].Visible = false;
			repositoryItem.View.Columns[nameof(SimpleUserModel.PositionId)].Visible = false;
			repositoryItem.View.Columns[nameof(SimpleUserModel.OtdelSubdivisionId)].Visible = false;

			repositoryItem.View.Columns[nameof(SimpleUserModel.Fio)].Caption = "Фио";
			repositoryItem.View.Columns[nameof(SimpleUserModel.Fio)].VisibleIndex = 1;

			repositoryItem.View.Columns[nameof(SimpleUserModel.Position)].Caption = "Должность";
			repositoryItem.View.Columns[nameof(SimpleUserModel.Position)].VisibleIndex = 2;

			repositoryItem.View.Columns[nameof(SimpleUserModel.OtdelSubdivision)].Caption = "Отдел";
			repositoryItem.View.Columns[nameof(SimpleUserModel.OtdelSubdivision)].VisibleIndex = 3;

			return repositoryItem;
		}

		private void comboBoxOperational_SelectedValueChanged(object sender, EventArgs e)
		{
			if (comboBoxOperational.SelectedValue != null)
			{
				_userSettingsService.SetString(_lastSelectedOperationalId,
					comboBoxOperational.SelectedValue.ToString());
			}

			RefreshOperational();
		}

		private void RefreshOperational()
		{
			if (comboBoxOperational.SelectedValue == null)
			{
				return;
			}

			_layoutManager?.GridLayoutEventsOff();
			_layoutManager?.EventsOff();

			_layoutManager = _layoutManagerModels
				.Single(x => x.OperationalId == (Guid)comboBoxOperational.SelectedValue).GridControlLayoutManager;

			gridView.CustomRowCellEdit -= gridView_CustomRowCellEdit;
			AddColumns();
			gridView.CustomRowCellEdit += gridView_CustomRowCellEdit;

			LoadData();

			SetBandName();
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


		private void btnAdd_Click(object sender, EventArgs e)
		{
			_errorCatcher.Do(() =>
			{
				var d = new OperationalLineEditDialog();
				d.Init(_operationalLineService,
					_operationalService,
					_planService,
					_errorCatcher,
					_contractService,
					_workStructureService,
					_designBasicService,
					_subdivisionService,
					_identityService,
					_operationalTypeTaskService);
				d.LoadData(null, (Guid?)comboBoxOperational.SelectedValue);

				if (d.ShowDialog() == DialogResult.OK)
				{
					var operationalLineData = _operationalLinePresenter
						.GetData((Guid)comboBoxOperational.SelectedValue, checkBoxAnnul.Checked, d.OperationalLineId)
						.First();
					_operationalLineDatas.Add(operationalLineData);

					AddTaskData(operationalLineData.ComplianceGroupId);

					var workLineDatas = _operationalWorkLineService.GetList()
						.Where(x => x.OperationalLineId == operationalLineData.Id).ToList();
					_workLineDatas.AddRange(workLineDatas);

					_layoutManager.DataSource = _operationalLineDatas;

					var rowHandle =
						gridView.LocateByValue(nameof(OperationalLinePresenterData.Id), d.OperationalLineId);

					if (rowHandle != GridControl.InvalidRowHandle)
					{
						gridView.FocusedRowHandle = rowHandle;
					}
				}
			});
		}

		private void AddTaskData(Guid workId)
		{
			if (_taskDatas.Any(x => x.WorkId == workId) == false)
			{
				var taskDatas = _taskService.GetList().Where(x =>
						x.Type == (int)TaskType.работа && x.WorkId == workId)
					.Select(x => new SimpleTaskModel
					{
						Id = x.Id,
						Name = x.Name,
						WorkId = x.WorkId
					}).ToList();

				var repositoryItem = CreateTaskRi(taskDatas);

				_riComplinceWork.Add(workId, repositoryItem);

				_taskDatas.AddRange(taskDatas);
			}
		}

		private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			btnAdd_Click(sender, e);
		}

		private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_errorCatcher.Do(() =>
			{
				var operationalLine = (OperationalLinePresenterData)gridView.GetFocusedRow();

				var d = new OperationalLineEditDialog();
				d.Init(_operationalLineService,
					_operationalService,
					_planService,
					_errorCatcher,
					_contractService,
					_workStructureService,
					_designBasicService,
					_subdivisionService,
					_identityService,
					_operationalTypeTaskService);
				d.LoadData(operationalLine.Id);

				if (d.ShowDialog() == DialogResult.OK)
				{
					var operationalLineData = _operationalLinePresenter
						.GetData((Guid)comboBoxOperational.SelectedValue, checkBoxAnnul.Checked, d.OperationalLineId)
						.First();
					_operationalLineDatas = _operationalLineDatas.Where(x => x.Id != operationalLineData.Id).ToList();
					_operationalLineDatas.Add(operationalLineData);

					AddTaskData(operationalLineData.ComplianceGroupId);

					_layoutManager.DataSource = _operationalLineDatas;

					var rowHandle =
						gridView.LocateByValue(nameof(OperationalLinePresenterData.Id), d.OperationalLineId);

					if (rowHandle != GridControl.InvalidRowHandle)
					{
						gridView.FocusedRowHandle = rowHandle;
					}
				}
			});
		}

		private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			var data = (OperationalLinePresenterData)gridView.GetFocusedRow();

			if (data == null)
			{
				e.Cancel = true;
				return;
			}

			if (_identityService.IsInRole(OperationalRoleList.Добавление_редактирование) == false)
			{
				удалитьToolStripMenuItem.Enabled = редактироватьToolStripMenuItem.Enabled =
					добавитьToolStripMenuItem.Enabled = восстановитьToolStripMenuItem.Enabled = false;
				return;
			}

			удалитьToolStripMenuItem.Enabled = редактироватьToolStripMenuItem.Enabled = data.DtDelete.HasValue == false;
			восстановитьToolStripMenuItem.Enabled = data.DtDelete.HasValue;
		}

		private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_errorCatcher.Do(() =>
			{
				if (MessageBox.Show("Удалить выбранную строку оперативки?", "Внимание", MessageBoxButtons.OKCancel) ==
					DialogResult.OK)
				{
					var operationalLineData = (OperationalLinePresenterData)gridView.GetFocusedRow();
					_operationalLineService.Delete(operationalLineData.Id);

					_operationalLineDatas = _operationalLineDatas.Where(x => x.Id != operationalLineData.Id).ToList();
					_layoutManager.DataSource = _operationalLineDatas;
				}
			});
		}

		private void историяToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_errorCatcher.Do(() =>
			{
				var operationalLine = (OperationalLinePresenterData)gridView.GetFocusedRow();

				var d = new SUVPP.UI.Dialogs.Common.HistoryDialog(_logService, _userService);
				d.Init("История", operationalLine.Id);
				d.ShowDialog();
			});
		}


		private void gridView_CustomUnboundColumnData_1(object sender,
			DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
		{
			if (e.IsSetData)
			{
				var columnData = (WorkColumnData)e.Column.Tag;
				var operationalLinePresenterData = (OperationalLinePresenterData)e.Row;

				if (nameof(OperationalWorkLineData.ComplianceWorkId) == columnData.Name &&
					_operationalWorkLineService.CheckComplianceWorkIsAddedInAnyWorkLine(operationalLinePresenterData.Id, columnData.OperationalWorkId.Value, (Guid?)e.Value) &&
					MessageBox.Show("Выбранная работа уже существует в текущей оперативке.\nВсе равно продолжить?", "Внимание", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
				{
					RefreshWorkLineDatas();
					return;
				}

				_operationalWorkLineService.Update(operationalLinePresenterData.Id, columnData.OperationalWorkId.Value,
														columnData.Name, e.Value);
				RefreshWorkLineDatas();
			}

			if (e.IsGetData)
			{
				var operationalLinePresenterData = (OperationalLinePresenterData)e.Row;
				var columnData = (WorkColumnData)e.Column.Tag;

				var workLineData = _workLineDatas.SingleOrDefault(x =>
					x.OperationalLineId == operationalLinePresenterData.Id &&
					x.OperationalWorkId == columnData.OperationalWorkId);

				if (workLineData == null)
				{
					return;
				}

				e.Value = _operationalLinePresenter.Get(columnData.Name, workLineData);
			}
		}

		private void gridView_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
		{
			switch (((WorkColumnData)e.Column.Tag).Name)
			{
				case nameof(OperationalWorkLineData.ComplianceWorkId):
					{
						var data = (OperationalLinePresenterData)gridView.GetRow(e.RowHandle);

						if (data == null)
						{
							return;
						}

						e.RepositoryItem = _riComplinceWork[data.ComplianceGroupId];
					}
					break;
				case nameof(OperationalWorkLineData.ComplianceWorkAllWorkId):
					{
						var data = (OperationalLinePresenterData)gridView.GetRow(e.RowHandle);

						if (data == null)
						{
							return;
						}

						e.RepositoryItem = _riComplinceWorkAllWork[data.PlanId];
					}
					break;
				case nameof(OperationalWorkLineData.Status):
					{
						var t = _statusRiModel.FirstOrDefault(x =>
							x.WorkId == ((WorkColumnData)e.Column.Tag).OperationalWorkId.Value &&
							x.ColumnName == nameof(OperationalWorkLineData.Status))?.Repository;
						e.RepositoryItem = t;
					}
					break;
				case nameof(OperationalWorkLineData.Reason):
					e.RepositoryItem = _statusRiModel.FirstOrDefault(x =>
						x.WorkId == ((WorkColumnData)e.Column.Tag).OperationalWorkId.Value &&
						x.ColumnName == nameof(OperationalWorkLineData.Reason))?.Repository;
					break;
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

		private void gridView_CustomColumnDisplayText(object sender,
			DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
		{
			if (e.Value == null)
			{
				return;
			}

			if (((WorkColumnData)e.Column.Tag).Name == nameof(OperationalWorkLineData.ComplianceWorkId))
			{
				e.DisplayText = _taskDatas.SingleOrDefault(x => x.Id == (Guid)e.Value)?.Name;
			}

			if (((WorkColumnData)e.Column.Tag).Name == nameof(OperationalWorkLineData.ComplianceWorkAllWorkId))
			{
				e.DisplayText = _allPgTasks.SingleOrDefault(x => x.Id == (Guid)e.Value)?.Name;
			}
		}

		private void simpleButtonEditWork_Click(object sender, EventArgs e)
		{
			var d = new OperationalEditorDialog();
			d.Init(
				_operationalWorkPresenter,
				_userService,
				_logService,
				_errorCatcher,
				_operationalService,
				_operationalWorkService,
				_operationalWorkStatusService,
				_operationalTypeTaskService);

			d.ShowDialog();

			RefreshStatusDatas();
			RefreshOperational();
		}

		private void gridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			if (e.CellValue != null && !String.IsNullOrEmpty(e.CellValue.ToString()) &&
				(((WorkColumnData)e.Column.Tag).Name == nameof(OperationalWorkLineData.Status) ||
				 ((WorkColumnData)e.Column.Tag).Name == nameof(OperationalWorkLineData.Reason)))
			{
				var statusColor = _statusDatas.FirstOrDefault(x =>
					x.OperationalWorkId == ((WorkColumnData)e.Column.Tag).OperationalWorkId &&
					x.Name == e.CellValue.ToString())?.Color;
				if (string.IsNullOrEmpty(statusColor))
				{
					return;
				}

				e.Appearance.BackColor = ColorTranslator.FromHtml(statusColor);
			}

			if (e.Column.FieldName == nameof(OperationalLinePresenterData.Shifr))
			{
				var operationalLinePresenterData = (OperationalLinePresenterData)gridView.GetRow(e.RowHandle);
				if (operationalLinePresenterData != null && operationalLinePresenterData.PlanIsMain == false)
				{
					e.Appearance.BackColor = Color.Red;
				}
			}
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

		private void gridView_MouseUp(object sender, MouseEventArgs e)
		{
			var hitInfo = gridView.CalcHitInfo(new Point(e.X, e.Y));
			if (hitInfo.InBandPanel && hitInfo.Band != null)
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
						var hasColumnStatus = false;
						foreach (BandedGridColumn column in hitInfo.Band.Columns)
						{
							if (((WorkColumnData)column.Tag).Name != nameof(OperationalWorkLineData.Status))
							{
								column.Visible = false;
							}
							else
							{
								hasColumnStatus = true;
							}
						}

						if (hasColumnStatus == false)
						{
							hitInfo.Band.Columns[0].Visible = true;
						}
					}
					else
					{
						foreach (BandedGridColumn column in hitInfo.Band.Columns)
						{
							var name = ((WorkColumnData)column.Tag).Name;

							if (name != nameof(OperationalLinePresenterData.CustomerName)
								&& name != nameof(OperationalLinePresenterData.Shifr)
								&& name != nameof(OperationalLinePresenterData.StageNumber)
								&& name != nameof(OperationalLinePresenterData.Name))
							{
								column.Visible = false;
							}
						}
					}

					hitInfo.Band.Caption = hitInfo.Band.Caption.Replace("[-]", "[+]");
				}
			}
		}

		private void checkBoxAnnul_CheckedChanged(object sender, EventArgs e)
		{
			LoadData();
		}

		private void восстановитьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_errorCatcher.Do(() =>
			{
				if (MessageBox.Show("Восстановить выбранную строку оперативки?", "Внимание",
					MessageBoxButtons.OKCancel) == DialogResult.OK)
				{
					var operationalLinePresenterData = (OperationalLinePresenterData)gridView.GetFocusedRow();

					var data = _operationalLineService.GetList().FirstOrDefault(x =>
						x.OperationalId == operationalLinePresenterData.OperationalId
						&& x.ContractId == operationalLinePresenterData.ContractId
						&& x.PlanId == operationalLinePresenterData.PlanId
						&& x.TypeTaskId == operationalLinePresenterData.TaskTypeId
						&& x.DtDelete.HasValue == false
						&& x.SubdivisionId == operationalLinePresenterData.SubdivisionId);

					if (data != null)
					{
						throw new ClientException("Оперативка с текущими данными уже существует!");
					}

					_operationalLineService.Recover(operationalLinePresenterData.Id);

					LoadData();
				}
			});
		}

		private void SetCacheGridControlLayoutManagers()
		{
			_layoutManagerModels.Add(LoadDataLayoutManager(OperationalEnum.Igdi));
			_layoutManagerModels.Add(LoadDataLayoutManager(OperationalEnum.Igmi));
			_layoutManagerModels.Add(LoadDataLayoutManager(OperationalEnum.Igi));
			_layoutManagerModels.Add(LoadDataLayoutManager(OperationalEnum.Iei));
		}

		private LayoutManagerModel LoadDataLayoutManager(Guid operationalId)
		{
			return new LayoutManagerModel
			{
				OperationalId = operationalId,
				GridControlLayoutManager = new GridControlLayoutManager(null, gridControl, gridView,
					operationalId.ToString(), null, (x, y) =>
					{
						RefreshRarelyChangedCashDatas();
						LoadData();
					})
			};
		}

		private void AddColumns()
		{
			gridView.Columns.Clear();
			gridView.Bands.Clear();

			var band = gridView.Bands.AddBand("Общие данные");
			band.Name = band.Caption;
			band.AppearanceHeader.BackColor = Color.LightGray;
			band.AppearanceHeader.Options.UseBackColor = true;
			band.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			band.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			band.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;

			AddColumn(band,
				new WorkColumnData
				{
					Name = nameof(OperationalLinePresenterData.Priority),
					FieldName = nameof(OperationalLinePresenterData.Priority),
					IsBoundColumn = true
				}, "Приоритет", 50);
			AddColumn(band,
				new WorkColumnData
				{
					Name = nameof(OperationalLinePresenterData.CustomerName),
					FieldName = nameof(OperationalLinePresenterData.CustomerName),
					IsBoundColumn = true
				}, "Заказчик", 100, 0, false);
			AddColumn(band,
				new WorkColumnData
				{
					Name = nameof(OperationalLinePresenterData.Shifr),
					FieldName = nameof(OperationalLinePresenterData.Shifr),
					IsBoundColumn = true
				}, "Шифр", 100, 0, false);
			AddColumn(band,
				new WorkColumnData
				{
					Name = nameof(OperationalLinePresenterData.StageNumber),
					FieldName = nameof(OperationalLinePresenterData.StageNumber),
					IsBoundColumn = true
				}, "Этап", 100, 0, false);
			AddColumn(band,
				new WorkColumnData
				{
					Name = nameof(OperationalLinePresenterData.Name),
					FieldName = nameof(OperationalLinePresenterData.Name),
					IsBoundColumn = true
				}, "Название", 100, 0, false);
			AddColumn(band,
				new WorkColumnData
				{
					Name = nameof(OperationalLinePresenterData.SubdivisionShortName),
					FieldName = nameof(OperationalLinePresenterData.SubdivisionShortName),
					IsBoundColumn = true
				}, "Подразделение", 100, 0, false);
			AddColumn(band,
				new WorkColumnData
				{
					Name = nameof(OperationalLinePresenterData.Gip),
					FieldName = nameof(OperationalLinePresenterData.Gip),
					IsBoundColumn = true
				}, "ГИП", 100, 0, false);
			AddColumn(band,
				new WorkColumnData
				{
					Name = nameof(OperationalLinePresenterData.PlanName),
					FieldName = nameof(OperationalLinePresenterData.PlanName),
					IsBoundColumn = true
				}, "План-график", 150, 0, false);
			AddColumn(band,
				new WorkColumnData
				{
					Name = nameof(OperationalLinePresenterData.PlanIsMain),
					FieldName = nameof(OperationalLinePresenterData.PlanIsMain),
					IsBoundColumn = true
				}, "Целевой план", 50, 0, false);
			AddColumn(band,
				new WorkColumnData
				{
					Name = nameof(OperationalLinePresenterData.TaskType),
					FieldName = nameof(OperationalLinePresenterData.TaskType),
					IsBoundColumn = true
				}, "Тип задания", 80, 0, false);
			AddColumn(band,
				new WorkColumnData
				{
					Name = nameof(OperationalLinePresenterData.ComplianceGroupName),
					FieldName = nameof(OperationalLinePresenterData.ComplianceGroupName),
					IsBoundColumn = true
				}, "Соответствует группе", 150, 0, false);
			AddColumn(band,
				new WorkColumnData
				{
					Name = nameof(OperationalLinePresenterData.DtPlanStart),
					FieldName = nameof(OperationalLinePresenterData.DtPlanStart),
					IsBoundColumn = true
				}, "Плановая дата начала", 70, 0, false);
			AddColumn(band,
				new WorkColumnData
				{
					Name = nameof(OperationalLinePresenterData.DtPlanEnd),
					FieldName = nameof(OperationalLinePresenterData.DtPlanEnd),
					IsBoundColumn = true
				}, "Плановая дата окончания", 70, 0, false);
			AddColumn(band,
				new WorkColumnData
				{
					Name = nameof(OperationalLinePresenterData.DtForecastStart),
					FieldName = nameof(OperationalLinePresenterData.DtForecastStart),
					IsBoundColumn = true
				}, "Прогнозная дата начала", 70, 0, false);
			AddColumn(band,
				new WorkColumnData
				{
					Name = nameof(OperationalLinePresenterData.DtForecastEnd),
					FieldName = nameof(OperationalLinePresenterData.DtForecastEnd),
					IsBoundColumn = true
				}, "Прогнозная дата окончания", 70, 0, false);
			AddColumn(band,
				new WorkColumnData
				{
					Name = nameof(OperationalLinePresenterData.DtFactStart),
					FieldName = nameof(OperationalLinePresenterData.DtFactStart),
					IsBoundColumn = true
				}, "Фактическая дата начала", 70, 0, false);
			AddColumn(band,
				new WorkColumnData
				{
					Name = nameof(OperationalLinePresenterData.DtFactEnd),
					FieldName = nameof(OperationalLinePresenterData.DtFactEnd),
					IsBoundColumn = true
				}, "Фактическая дата окончания", 70, 0, false);
			AddColumn(band,
				new WorkColumnData
				{
					Name = nameof(OperationalLinePresenterData.DtCreate),
					FieldName = nameof(OperationalLinePresenterData.DtCreate),
					IsBoundColumn = true
				}, "Дата создания", 70, 0, false);
			AddColumn(band,
				new WorkColumnData
				{
					Name = nameof(OperationalLinePresenterData.DtDelete),
					FieldName = nameof(OperationalLinePresenterData.DtDelete),
					IsBoundColumn = true
				}, "Дата удаления", 70, 0, false);

			var operationalWorkList = _operationalWorkService.GetList()
				.Where(x => x.OperationalId == (Guid)comboBoxOperational.SelectedValue)
				.OrderBy(x => x.OrderNum).ToList();

			gridView.CustomUnboundColumnData -= gridView_CustomUnboundColumnData_1;

			var num = 1;

			foreach (var item in operationalWorkList)
			{
				band = gridView.Bands.AddBand(item.Name);
				band.Name = item.Id.ToString();
				band.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
				band.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

				if ((num % 2) == 0)
				{
					band.AppearanceHeader.BackColor = Color.LightGray;
					band.AppearanceHeader.Options.UseBackColor = true;
				}

				num++;

				if (item.ComplianceWorkVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.ComplianceWorkId) },
						"Соответствует работе", 150, UnboundColumnType.Object,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.ComplianceWorkAllWorkVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.ComplianceWorkAllWorkId) },
						"Соответствует работе из всего ПГ", 150, UnboundColumnType.Object,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.DtPlanStartVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.DtPlanStart) },
						"Плановая дата начала", 100, UnboundColumnType.DateTime, false);
				}

				if (item.DtPlanEndVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.DtPlanEnd) },
						"Плановая дата окончания", 100, UnboundColumnType.DateTime, false);
				}

				if (item.DtForecastStartVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.DtForecastStart) },
						"Прогнозная дата начала", 100, UnboundColumnType.DateTime,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.DtForecastEndVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.DtForecastEnd) },
						"Прогнозная дата окончания", 100, UnboundColumnType.DateTime,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.DtFactStartVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.DtFactStart) },
						"Фактическая дата начала", 100, UnboundColumnType.DateTime,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.DtFactEndVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.DtFactEnd) },
						"Фактическая дата окончания", 100, UnboundColumnType.DateTime,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.PlanSmetaMainVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.PlanSmetaMain) },
						"Плановая смета. Основная", 100, UnboundColumnType.String,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.PlanSmetaFixedVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.PlanSmetaFixed) },
						"Плановая смета. Закрепление", 100, UnboundColumnType.String,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.PlanSmeta3DScanVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.PlanSmeta3DScan) },
						"Плановая смета. 3D сканирование", 100, UnboundColumnType.String,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.PlanSmetaGeoradScanVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.PlanSmetaGeoradScan) },
						"Плановая смета. Георадарное сканирование", 100, UnboundColumnType.String,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.PlanSmetaCountSkvazhinVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.PlanSmetaCountSkvazhin) },
						"Плановая смета. Колич.скважин", 100, UnboundColumnType.String,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.PlanSmetaPmVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.PlanSmetaPm) },
						"Плановая смета. П.м.", 100, UnboundColumnType.String,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.PlanSmetaCountMonolitVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.PlanSmetaCountMonolit) },
						"Плановая смета. Количество монолитов", 100, UnboundColumnType.String,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.PlanSmetaResponsibleUserVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{
							OperationalWorkId = item.Id,
							Name = nameof(OperationalWorkLineData.PlanSmetaResponsibleUserId)
						}, "Плановая смета. Ответственный", 100, UnboundColumnType.Object,
						item.FromOperationalWorkId.HasValue == false, _usersResponsibleRepositoryItem);
				}

				if (item.PlanSmetaDtFactStartVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.PlanSmetaDtFactStart) },
						"Плановая смета. Фактическая дата начала", 100, UnboundColumnType.DateTime,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.PlanSmetaDtFactEndVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.PlanSmetaDtFactEnd) },
						"Плановая смета. Фактическая дата окончания", 100, UnboundColumnType.DateTime,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.ExecuteSmetaMainVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.ExecuteSmetaMain) },
						"Исполнительная смета. Основная", 100, UnboundColumnType.String,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.ExecuteSmetaFixedVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.ExecuteSmetaFixed) },
						"Исполнительная смета. Закрепление", 100, UnboundColumnType.String,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.ExecuteSmeta3DScanVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.ExecuteSmeta3DScan) },
						"Исполнительная смета. 3D сканирование", 100, UnboundColumnType.String,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.ExecuteSmetaGeoradScanVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{
							OperationalWorkId = item.Id,
							Name = nameof(OperationalWorkLineData.ExecuteSmetaGeoradScan)
						}, "Исполнительная смета. Георадарное сканирование", 100, UnboundColumnType.String,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.ExecuteSmetaCountSkvazhinVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{
							OperationalWorkId = item.Id,
							Name = nameof(OperationalWorkLineData.ExecuteSmetaCountSkvazhin)
						}, "Исполнительная  смета. Колич.скважин", 100, UnboundColumnType.String,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.ExecuteSmetaPmVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{
							OperationalWorkId = item.Id,
							Name = nameof(OperationalWorkLineData.ExecuteSmetaPm)
						}, "Исполнительная  смета. П.м.", 100, UnboundColumnType.String,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.ExecuteSmetaCountMonolitVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{
							OperationalWorkId = item.Id,
							Name = nameof(OperationalWorkLineData.ExecuteSmetaCountMonolit)
						}, "Исполнительная  смета. Количество монолитов", 100, UnboundColumnType.String,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.ExecuteSmetaResponsibleUserVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{
							OperationalWorkId = item.Id,
							Name = nameof(OperationalWorkLineData.ExecuteSmetaResponsibleUserId)
						}, "Исполнительная смета. Ответственный", 100, UnboundColumnType.Object,
						item.FromOperationalWorkId.HasValue == false, _usersResponsibleRepositoryItem);
				}

				if (item.ExecuteSmetaDtFactStartVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{
							OperationalWorkId = item.Id,
							Name = nameof(OperationalWorkLineData.ExecuteSmetaDtFactStart)
						}, "Исполнительная смета. Фактическая дата начала", 100, UnboundColumnType.DateTime,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.ExecuteSmetaDtFactEndVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.ExecuteSmetaDtFactEnd) },
						"Исполнительная смета. Фактическая дата окончания", 100, UnboundColumnType.DateTime,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.ExecuteSmetaAdditionalVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{
							OperationalWorkId = item.Id,
							Name = nameof(OperationalWorkLineData.ExecuteSmetaAdditional)
						}, "Исполнительная смета. Дополнительная", 100, UnboundColumnType.String,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.DtActPassingStripSurveyVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{
							OperationalWorkId = item.Id,
							Name = nameof(OperationalWorkLineData.DtActPassingStripSurvey)
						}, "Акт сдачи полосовой съемки", 100, UnboundColumnType.DateTime,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.DtActPassingRapperVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.DtActPassingRapper) },
						"Акт сдачи реперов", 100, UnboundColumnType.DateTime,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.DtTopoplanVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.DtTopoplan) },
						"Топопланы", 100, UnboundColumnType.DateTime, item.FromOperationalWorkId.HasValue == false);
				}

				if (item.DtLoadInParallelProjectionVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{
							OperationalWorkId = item.Id,
							Name = nameof(OperationalWorkLineData.DtLoadInParallelProjection)
						}, "Загрузка в параллел. проект", 100, UnboundColumnType.DateTime,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.DtProfilAndStatementVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.DtProfilAndStatement) },
						"Профили и ведомости", 100, UnboundColumnType.DateTime,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.DtTransferProfileIgiAndIgmiVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{
							OperationalWorkId = item.Id,
							Name = nameof(OperationalWorkLineData.DtTransferProfileIgiAndIgmi)
						}, "Передача профилей ИГИ и ИГМИ", 100, UnboundColumnType.DateTime,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.DtUnloadingReportFromSapsanVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{
							OperationalWorkId = item.Id,
							Name = nameof(OperationalWorkLineData.DtUnloadingReportFromSapsan)
						}, "Выгрузка отчета в Сапсан2020", 100, UnboundColumnType.DateTime,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.DtNormocontrolVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.DtNormocontrol) },
						"Нормоконтроль", 100, UnboundColumnType.DateTime, item.FromOperationalWorkId.HasValue == false);
				}

				if (item.HasRemarkVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.HasRemark) },
						"Наличие замечания", 100, UnboundColumnType.Boolean,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.DtKameralIgdiEndVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.DtKameralIgdiEnd) },
						"Окончание камеральных ИГДИ", 100, UnboundColumnType.DateTime,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.DtForecastEndIgdiVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.DtForecastEndIgdi) },
						"Прогноз.срок окончания ИГДИ", 100, UnboundColumnType.DateTime,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.VodotokCountVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.VodotokCount) },
						"Число водотоков", 100, UnboundColumnType.String, item.FromOperationalWorkId.HasValue == false);
				}

				if (item.CalculationsCountVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.CalculationsCount) },
						"Количество расчетов", 100, UnboundColumnType.String,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.DtSigningVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.DtSigning) },
						"Дата подписания", 100, UnboundColumnType.DateTime,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.PvoCountVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.PvoCount) },
						"ПВО, всего шт", 100, UnboundColumnType.String, item.FromOperationalWorkId.HasValue == false);
				}

				if (item.SurvayFieldVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.SurvayField) },
						"Изыскания площадок, всего га", 100, UnboundColumnType.String,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.SurvayTrackVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.SurvayTrack) },
						"Изыскания трасс, всего км", 100, UnboundColumnType.String,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.TZVisible)
				{
					AddColumn(band,
						new WorkColumnData { OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.TZ) },
						"ТЗ", 100, UnboundColumnType.DateTime, item.FromOperationalWorkId.HasValue == false);
				}

				if (item.DtEndVisible)
				{
					AddColumn(band,
						new WorkColumnData { OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.DtEnd) },
						"Дата окончания", 100, UnboundColumnType.DateTime,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.TfoVisible)
				{
					AddColumn(band,
						new WorkColumnData { OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.Tfo) },
						"ТФО", 100, UnboundColumnType.String, item.FromOperationalWorkId.HasValue == false);
				}

				if (item.DtPlanCompilationVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.DtPlanCompilation) },
						"Плановая дата составления", 100, UnboundColumnType.DateTime,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.DtSendCompilationVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.DtSendCompilation) },
						"Дата отправки на согласование", 100, UnboundColumnType.DateTime,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.DtSendVisible)
				{
					AddColumn(band,
						new WorkColumnData { OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.DtSend) },
						"Дата отправки", 100, UnboundColumnType.DateTime, item.FromOperationalWorkId.HasValue == false);
				}

				if (item.FonVisible)
				{
					AddColumn(band,
						new WorkColumnData { OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.Fon) },
						"ФОН", 100, UnboundColumnType.String, item.FromOperationalWorkId.HasValue == false);
				}

				if (item.PHHVisible)
				{
					AddColumn(band,
						new WorkColumnData { OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.PHH) },
						"РХХ", 100, UnboundColumnType.String, item.FromOperationalWorkId.HasValue == false);
				}

				if (item.DtIssuePrescriptionGOVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.DtIssuePrescriptionGO) },
						"Предписание ГО дата выдачи", 100, UnboundColumnType.DateTime,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.PGRVisible)
				{
					AddColumn(band,
						new WorkColumnData { OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.PGR) },
						"ПГР (ИГИ)", 100, UnboundColumnType.String, item.FromOperationalWorkId.HasValue == false);
				}

				if (item.IGSVisible)
				{
					AddColumn(band,
						new WorkColumnData { OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.IGS) },
						"ИГС (ИГИ)", 100, UnboundColumnType.String, item.FromOperationalWorkId.HasValue == false);
				}

				if (item.PchvOaVisible)
				{
					AddColumn(band,
						new WorkColumnData { OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.PchvOa) },
						"ПЧВ ОА", 100, UnboundColumnType.String, item.FromOperationalWorkId.HasValue == false);
				}

				if (item.PchvBakVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.PchvBak) }, "ПЧВ БАК",
						100, UnboundColumnType.String, item.FromOperationalWorkId.HasValue == false);
				}

				if (item.PchvPrzVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.PchvPrz) }, "ПЧВ ПРЗ",
						100, UnboundColumnType.String, item.FromOperationalWorkId.HasValue == false);
				}

				if (item.AhSHVisible)
				{
					AddColumn(band,
						new WorkColumnData { OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.AhSH) },
						"АХ Ш", 100, UnboundColumnType.String, item.FromOperationalWorkId.HasValue == false);
				}

				if (item.AhPrVisible)
				{
					AddColumn(band,
						new WorkColumnData { OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.AhPr) },
						"АХ ПР", 100, UnboundColumnType.String, item.FromOperationalWorkId.HasValue == false);
				}

				if (item.PchvZsoVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.PchvZso) }, "ПЧВ ЗСО",
						100, UnboundColumnType.String, item.FromOperationalWorkId.HasValue == false);
				}

				if (item.PovVodVisible)
				{
					AddColumn(band,
						new WorkColumnData { OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.PovVod) },
						"ПОВ.В ОД", 100, UnboundColumnType.String, item.FromOperationalWorkId.HasValue == false);
				}

				if (item.PodzVodIpvsVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.PodzVodIpvs) },
						"ПОДЗ. ВОД ИПВС", 100, UnboundColumnType.String, item.FromOperationalWorkId.HasValue == false);
				}

				if (item.PodzVodIgsVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.PodzVodIgs) },
						"ПОДЗ. ВОД ИГС", 100, UnboundColumnType.String, item.FromOperationalWorkId.HasValue == false);
				}

				if (item.DonVisible)
				{
					AddColumn(band,
						new WorkColumnData { OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.Don) },
						"ДОН", 100, UnboundColumnType.String, item.FromOperationalWorkId.HasValue == false);
				}

				if (item.AvVisible)
				{
					AddColumn(band,
						new WorkColumnData { OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.Av) },
						"АВ", 100, UnboundColumnType.String, item.FromOperationalWorkId.HasValue == false);
				}

				if (item.GraSVisible)
				{
					AddColumn(band,
						new WorkColumnData { OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.GraS) },
						"ГраС", 100, UnboundColumnType.String, item.FromOperationalWorkId.HasValue == false);
				}

				if (item.RadVisible)
				{
					AddColumn(band,
						new WorkColumnData { OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.Rad) },
						"РАД", 100, UnboundColumnType.String, item.FromOperationalWorkId.HasValue == false);
				}

				if (item.ErnVisible)
				{
					AddColumn(band,
						new WorkColumnData { OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.Ern) },
						"ЕРН", 100, UnboundColumnType.String, item.FromOperationalWorkId.HasValue == false);
				}

				if (item.FfVisible)
				{
					AddColumn(band,
						new WorkColumnData { OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.Ff) },
						"ФФ", 100, UnboundColumnType.String, item.FromOperationalWorkId.HasValue == false);
				}

				if (item.ProtocolRadiationVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.ProtocolRadiation) },
						"Протокол радиации", 100, UnboundColumnType.String,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.DtForecastEndKameralIgdiVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{
							OperationalWorkId = item.Id,
							Name = nameof(OperationalWorkLineData.DtForecastEndKameralIgdi)
						}, "ИГДИ.Прогнозная дата окончания камеральных работ", 100, UnboundColumnType.DateTime,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.DtForecastKameralIgiVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.DtForecastKameralIgi) },
						"ИГИ.Прогнозная дата окончания камеральных работ", 100, UnboundColumnType.DateTime,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.DtForecastCameralIgmiVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.DtForecastCameralIgmi) },
						"ИГМИ.Прогнозная дата окончания камеральных работ", 100, UnboundColumnType.DateTime,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.HasProtocolFieldSurvayVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{
							OperationalWorkId = item.Id,
							Name = nameof(OperationalWorkLineData.HasProtocolFieldSurvay)
						}, "Наличие протоколов полевого обследования", 100, UnboundColumnType.String,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.HasGraficPartVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.HasGraficPart) },
						"Наличие графической части", 100, UnboundColumnType.String,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.VetstanciaVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.Vetstancia) },
						"Ветстанция", 100, UnboundColumnType.String,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.DistrictsVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.Districts) },
						"Районы", 100, UnboundColumnType.String,
						item.FromOperationalWorkId.HasValue == false);
				}

				if (item.MainSpecialistVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.MainSpecialistId) },
						"Главный специалист", 100, UnboundColumnType.Object,
						item.FromOperationalWorkId.HasValue == false, _usersMainSpecialistRepositoryItem);
				}

				if (item.ResponsibleUserVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.ResponsibleUserId) },
						"Ответственный", 100, UnboundColumnType.Object, item.FromOperationalWorkId.HasValue == false,
						_usersResponsibleRepositoryItem);
				}

				if (item.ResponsibleUser2Visible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.ResponsibleUser2) },
						"Ответственный2", 100, UnboundColumnType.Object, item.FromOperationalWorkId.HasValue == false,
						_usersResponsibleRepositoryItem);
				}

				if (item.CommentVisible)
				{
					AddColumn(band,
						new WorkColumnData
						{ OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.Comment) },
						"Комментарий", 100, UnboundColumnType.String, item.FromOperationalWorkId.HasValue == false);
				}

				if (item.ReasonVisible)
				{
					AddColumn(band,
						new WorkColumnData { OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.Reason) },
						"Причина", 100, UnboundColumnType.String, item.FromOperationalWorkId.HasValue == false);
				}

				if (item.StatusVisible)
				{
					AddColumn(band,
						new WorkColumnData { OperationalWorkId = item.Id, Name = nameof(OperationalWorkLineData.Status) },
						"Статус", 100, UnboundColumnType.String, item.FromOperationalWorkId.HasValue == false);
				}
			}

			gridView.CustomUnboundColumnData += gridView_CustomUnboundColumnData_1;
		}


		private void AddColumn(GridBand band, WorkColumnData workColumnData, string columnCaption, int width,
			UnboundColumnType unboundColumnType = 0,
			bool columnAllowEdit = true, RepositoryItem repositoryItem = null)
		{
			var column = new BandedGridColumn();
			column.Caption = columnCaption;
			column.Tag = workColumnData;
			column.FieldName = workColumnData.FieldName ??
							   $"{workColumnData.OperationalWorkId.ToString()}_{workColumnData.Name}";
			column.Visible = true;

			if (unboundColumnType != 0 && workColumnData.Name != nameof(OperationalWorkLineData.Status))
			{
				column.Visible = false;
			}

			if (unboundColumnType == 0
				&& workColumnData.Name != nameof(OperationalLinePresenterData.CustomerName)
				&& workColumnData.Name != nameof(OperationalLinePresenterData.Shifr)
				&& workColumnData.Name != nameof(OperationalLinePresenterData.StageNumber)
				&& workColumnData.Name != nameof(OperationalLinePresenterData.Name))
			{
				column.Visible = false;
			}

			column.Width = width;
			column.UnboundType = unboundColumnType;
			column.OptionsColumn.AllowEdit = columnAllowEdit;

			if (repositoryItem != null)
			{
				column.ColumnEdit = repositoryItem;
			}

			band.Columns.Add(column);
		}


		private class WorkColumnData
		{
			public Guid? OperationalWorkId { get; set; }

			public string Name { get; set; }

			public string FieldName { get; set; }

			public bool IsBoundColumn { get; set; }
		}

		private class SimpleModel
		{
			public Guid Id { get; set; }
			public string Name { get; set; }
		}

		private class SimpleTaskModel
		{
			public Guid Id { get; set; }
			public string Name { get; set; }
			public Guid WorkId { get; set; }
			public Guid PlanId { get; set; }
		}

		private class SimpleUserModel
		{
			public Guid Id { get; set; }
			public string Fio { get; set; }
			public Guid? OtdelSubdivisionId { get; set; }
			public string OtdelSubdivision { get; set; }
			public string Position { get; set; }
			public Guid? PositionId { get; set; }
		}

		private class StatusRiModel
		{
			public Guid WorkId { get; set; }
			public string ColumnName { get; set; }
			public RepositoryItem Repository { get; set; }
		}

		private class LayoutManagerModel
		{
			public Guid OperationalId { get; set; }
			public GridControlLayoutManager GridControlLayoutManager { get; set; }
		}

		private void gridView_CellValueChanged(object sender,
			DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
			if (e.Column.FieldName == nameof(OperationalLinePresenterData.Priority))
			{
				var presenterData = (OperationalLinePresenterData)gridView.GetRow(e.RowHandle);

				if (presenterData == null)
				{
					return;
				}

				var operationalLineData = _operationalLineService.GetList().Single(x => x.Id == presenterData.Id);
				operationalLineData.Priority = e.Value.ToString();

				_operationalLineService.Update(operationalLineData.Id, operationalLineData);
			}
		}
	}
}