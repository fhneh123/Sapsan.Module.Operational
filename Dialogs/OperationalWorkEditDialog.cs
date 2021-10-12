using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Sapsan.Modules.Operational.Presenters.Data;
using Sapsan.Modules.Operational.Services.Contracts;
using Sapsan.Modules.Operational.Services.Data;
using Sapsan2.Contracts.Exceptions;
using Sapsan2.Core.Service.Contracts;
using SUVPP.UI.Dialogs.Common;

namespace Sapsan.Modules.Operational.Dialogs
{
	public partial class OperationalWorkEditDialog : FormWithEscape
	{
		private IOperationalService _operationalService;
		private IOperationalWorkService _operationalWorkService;
		private IErrorCatcher _errorCatcher;
		private ILogService _logService;
		private IUserService _userService;
		private IOperationalEntityStatusService _operationalWorkStatusService;

		private Guid? _operationalWorkId;

		public void Init(
			IOperationalService operationalService,
			IOperationalWorkService operationalWorkService,
			IErrorCatcher errorCatcher,
			ILogService logService,
			IUserService userService,
			IOperationalEntityStatusService operationalWorkStatusService)
		{
			_operationalService = operationalService;
			_operationalWorkService = operationalWorkService;
			_errorCatcher = errorCatcher;
			_logService = logService;
			_userService = userService;
			_operationalWorkStatusService = operationalWorkStatusService;

			InitializeComponent();

			comboBoxOperational.DisplayMember = nameof(OperationalData.Name);
			comboBoxOperational.ValueMember = nameof(OperationalData.Id);
			comboBoxOperational.DataSource = _operationalService.GetList().ToList();

			var data = (from w in _operationalWorkService.GetList()
						join o in _operationalService.GetList() on w.OperationalId equals o.Id
						where w.FromOperationalWorkId.HasValue == false
						orderby o.Name, w.Name
						select new
						{
							w.Id,
							Name = o.Name + " " + w.Name
						}).ToList();

			comboBoxFromOperationalWork.Properties.DataSource = data;

			comboBoxFromOperationalWork.Properties.PopulateViewColumns();
			comboBoxFromOperationalWork.Properties.View.Columns.ToList().ForEach(r => r.Visible = false);

			var column = comboBoxFromOperationalWork.Properties.View.Columns["Name"];
			column.Visible = true;
			column.Caption = "Работа";
		}

		public void LoadData(Guid? operationalWorkId = null)
		{
			_operationalWorkId = operationalWorkId;

			if (_operationalWorkId.HasValue)
			{
				Text = "Редактирование работы";

				if(_operationalWorkService.GetList().Single(x => x.Id == _operationalWorkId).FromOperationalWorkId.HasValue)
				{
					Text = "Редактирование взаимосвязанной работы";
				}

				var operationalWorkData =
					_operationalWorkService.GetListWithDeleted().Single(x => x.Id == _operationalWorkId);

				comboBoxOperational.SelectedValue = operationalWorkData.OperationalId;
				textBoxName.Text = operationalWorkData.Name;
				orderNumUpd.Value = operationalWorkData.OrderNum;

				comboBoxFromOperationalWork.Enabled = operationalWorkData.FromOperationalWorkId.HasValue;
				comboBoxFromOperationalWork.EditValue = operationalWorkData.FromOperationalWorkId;

				gridControl.DataSource = new List<OperationalWorkPresenterData>
				{
					new OperationalWorkPresenterData
					{
						СomplianceWorkVisible = operationalWorkData.ComplianceWorkVisible,
						ComplianceWorkAllWorkVisible = operationalWorkData.ComplianceWorkAllWorkVisible,
						DtPlanStartVisible = operationalWorkData.DtPlanStartVisible,
						DtPlanEndVisible = operationalWorkData.DtPlanEndVisible,
						DtForecastStartVisible = operationalWorkData.DtForecastStartVisible,
						DtForecastEndVisible = operationalWorkData.DtForecastEndVisible,
						DtFactStartVisible = operationalWorkData.DtFactStartVisible,
						DtFactEndVisible = operationalWorkData.DtFactEndVisible,
						ResponsibleUserVisible = operationalWorkData.ResponsibleUserVisible,
						CommentVisible = operationalWorkData.CommentVisible,
						StatusVisible = operationalWorkData.StatusVisible,
						PlanSmetaMainVisible = operationalWorkData.PlanSmetaMainVisible,
						PlanSmetaFixedVisible = operationalWorkData.PlanSmetaFixedVisible,
						PlanSmeta3DScanVisible = operationalWorkData.PlanSmeta3DScanVisible,
						PlanSmetaGeoradScanVisible = operationalWorkData.PlanSmetaGeoradScanVisible,
						PlanSmetaResponsibleUserVisible = operationalWorkData.PlanSmetaResponsibleUserVisible,
						PlanSmetaDtFactStartVisible = operationalWorkData.PlanSmetaDtFactStartVisible,
						PlanSmetaDtFactEndVisible = operationalWorkData.PlanSmetaDtFactEndVisible,
						ExecuteSmetaMainVisible = operationalWorkData.ExecuteSmetaMainVisible,
						ExecuteSmetaFixedVisible = operationalWorkData.ExecuteSmetaFixedVisible,
						ExecuteSmeta3DScanVisible = operationalWorkData.ExecuteSmeta3DScanVisible,
						ExecuteSmetaGeoradScanVisible = operationalWorkData.ExecuteSmetaGeoradScanVisible,
						ExecuteSmetaResponsibleUserVisible = operationalWorkData.ExecuteSmetaResponsibleUserVisible,
						ExecuteSmetaDtFactStartVisible = operationalWorkData.ExecuteSmetaDtFactStartVisible,
						ExecuteSmetaDtFactEndVisible = operationalWorkData.ExecuteSmetaDtFactEndVisible,
						ExecuteSmetaAdditionalVisible = operationalWorkData.ExecuteSmetaAdditionalVisible,
						MainSpecialistVisible = operationalWorkData.MainSpecialistVisible,
						DtActPassingStripSurveyVisible = operationalWorkData.DtActPassingStripSurveyVisible,
						DtActPassingRapperVisible = operationalWorkData.DtActPassingRapperVisible,
						DtTopoplanVisible = operationalWorkData.DtTopoplanVisible,
						DtLoadInParallelProjectionVisible = operationalWorkData.DtLoadInParallelProjectionVisible,
						DtProfilAndStatementVisible = operationalWorkData.DtProfilAndStatementVisible,
						DtTransferProfileIgiAndIgmiVisible = operationalWorkData.DtTransferProfileIgiAndIgmiVisible,
						DtUnloadingReportFromSapsanVisible = operationalWorkData.DtUnloadingReportFromSapsanVisible,
						DtNormocontrolVisible = operationalWorkData.DtNormocontrolVisible,
						ReasonVisible = operationalWorkData.ReasonVisible,
						HasRemarkVisible = operationalWorkData.HasRemarkVisible,
						DtKameralIgdiEndVisible = operationalWorkData.DtKameralIgdiEndVisible,
						DtForecastEndIgdiVisible = operationalWorkData.DtForecastEndIgdiVisible,
						VodotokCountVisible = operationalWorkData.VodotokCountVisible,
						CalculationsCountVisible = operationalWorkData.CalculationsCountVisible,
						DtSigningVisible = operationalWorkData.DtSigningVisible,
						PvoCountVisible = operationalWorkData.PvoCountVisible,
						SurvayFieldVisible = operationalWorkData.SurvayFieldVisible,
						SurvayTrackVisible = operationalWorkData.SurvayTrackVisible,
						TZVisible = operationalWorkData.TZVisible,
						DtEndVisible = operationalWorkData.DtEndVisible,
						TfoVisible = operationalWorkData.TfoVisible,
						ResponsibleUser2Visible = operationalWorkData.ResponsibleUser2Visible,
						DtPlanCompilationVisible = operationalWorkData.DtPlanCompilationVisible,
						DtSendCompilationVisible = operationalWorkData.DtSendCompilationVisible,
						DtSendVisible = operationalWorkData.DtSendVisible,
						FonVisible = operationalWorkData.FonVisible,
						PHHVisible = operationalWorkData.PHHVisible,
						DtIssuePrescriptionGOVisible = operationalWorkData.DtIssuePrescriptionGOVisible,
						PGRVisible = operationalWorkData.PGRVisible,
						IGSVisible = operationalWorkData.IGSVisible,
						PchvOaVisible = operationalWorkData.PchvOaVisible,
						PchvBakVisible = operationalWorkData.PchvBakVisible,
						PchvPrzVisible = operationalWorkData.PchvPrzVisible,
						AhSHVisible = operationalWorkData.AhSHVisible,
						AhPrVisible = operationalWorkData.AhPrVisible,
						PchvZsoVisible = operationalWorkData.PchvZsoVisible,
						PovVodVisible = operationalWorkData.PovVodVisible,
						PodzVodIpvsVisible = operationalWorkData.PodzVodIpvsVisible,
						PodzVodIgsVisible = operationalWorkData.PodzVodIgsVisible,
						DonVisible = operationalWorkData.DonVisible,
						AvVisible = operationalWorkData.AvVisible,
						GraSVisible = operationalWorkData.GraSVisible,
						RadVisible = operationalWorkData.RadVisible,
						ErnVisible = operationalWorkData.ErnVisible,
						FfVisible = operationalWorkData.FfVisible,
						ProtocolRadiationVisible = operationalWorkData.ProtocolRadiationVisible,
						DtForecastEndKameralIgdiVisible = operationalWorkData.DtForecastEndKameralIgdiVisible,
						DtForecastKameralIgiVisible = operationalWorkData.DtForecastKameralIgiVisible,
						DtForecastCameralIgmiVisible = operationalWorkData.DtForecastCameralIgmiVisible,
						HasProtocolFieldSurvayVisible = operationalWorkData.HasProtocolFieldSurvayVisible,
						HasGraficPartVisible = operationalWorkData.HasGraficPartVisible,
						VetstanciaVisible = operationalWorkData.VetstanciaVisible,
						DistrictsVisible = operationalWorkData.DistrictsVisible,
						PlanSmetaCountSkvazhinVisible = operationalWorkData.PlanSmetaCountSkvazhinVisible,
						PlanSmetaPmVisible = operationalWorkData.PlanSmetaPmVisible,
						PlanSmetaCountMonolitVisible = operationalWorkData.PlanSmetaCountMonolitVisible,
						ExecuteSmetaCountSkvazhinVisible = operationalWorkData.ExecuteSmetaCountSkvazhinVisible,
						ExecuteSmetaPmVisible = operationalWorkData.ExecuteSmetaPmVisible,
						ExecuteSmetaCountMonolitVisible = operationalWorkData.ExecuteSmetaCountMonolitVisible
					}
				};
			}
			else
			{
				gridControl.DataSource = new List<OperationalWorkPresenterData> { new OperationalWorkPresenterData() };
			}
		}

		private void gridControl_DataSourceChanged(object sender, EventArgs e)
		{
			gridView.PopulateColumns();
		}


		private void buttonSaveCancelControl1_ButtonSaveClick(object sender, EventArgs e)
		{
			_errorCatcher.Do(() =>
			{
				if (string.IsNullOrWhiteSpace(textBoxName.Text))
				{
					throw new ClientException("Не указано название работы.");
				}

				if (comboBoxOperational.SelectedValue == null)
				{
					throw new ClientException("Не указана оперативка.");
				}

				if (_operationalWorkId.HasValue && _operationalWorkService.GetList()
													.Single(x => x.Id == _operationalWorkId).FromOperationalWorkId
													.HasValue
												&& comboBoxFromOperationalWork.EditValue == null)
				{
					throw new ClientException("Не указана взаимосвязанная работа оперативки.");
				}

				var operationalWorkData = ((List<OperationalWorkPresenterData>)gridControl.DataSource).Single();

				if(operationalWorkData.СomplianceWorkVisible && operationalWorkData.ComplianceWorkAllWorkVisible)
				{
					throw new ClientException("Можно выбрать только один вариант из Соответствует работе и Соответствует работе всего ПГ");
				}

				var data = new OperationalWorkData
				{
					Name = textBoxName.Text,
					OperationalId = (Guid)comboBoxOperational.SelectedValue,
					OrderNum = (int)orderNumUpd.Value,
					ComplianceWorkVisible = operationalWorkData.СomplianceWorkVisible,
					ComplianceWorkAllWorkVisible = operationalWorkData.ComplianceWorkAllWorkVisible,
					DtPlanStartVisible = operationalWorkData.DtPlanStartVisible,
					DtPlanEndVisible = operationalWorkData.DtPlanEndVisible,
					DtForecastStartVisible = operationalWorkData.DtForecastStartVisible,
					DtForecastEndVisible = operationalWorkData.DtForecastEndVisible,
					DtFactStartVisible = operationalWorkData.DtFactStartVisible,
					DtFactEndVisible = operationalWorkData.DtFactEndVisible,
					ResponsibleUserVisible = operationalWorkData.ResponsibleUserVisible,
					CommentVisible = operationalWorkData.CommentVisible,
					StatusVisible = operationalWorkData.StatusVisible,
					PlanSmetaMainVisible = operationalWorkData.PlanSmetaMainVisible,
					PlanSmetaFixedVisible = operationalWorkData.PlanSmetaFixedVisible,
					PlanSmeta3DScanVisible = operationalWorkData.PlanSmeta3DScanVisible,
					PlanSmetaGeoradScanVisible = operationalWorkData.PlanSmetaGeoradScanVisible,
					PlanSmetaResponsibleUserVisible = operationalWorkData.PlanSmetaResponsibleUserVisible,
					PlanSmetaDtFactStartVisible = operationalWorkData.PlanSmetaDtFactStartVisible,
					PlanSmetaDtFactEndVisible = operationalWorkData.PlanSmetaDtFactEndVisible,
					ExecuteSmetaMainVisible = operationalWorkData.ExecuteSmetaMainVisible,
					ExecuteSmetaFixedVisible = operationalWorkData.ExecuteSmetaFixedVisible,
					ExecuteSmeta3DScanVisible = operationalWorkData.ExecuteSmeta3DScanVisible,
					ExecuteSmetaGeoradScanVisible = operationalWorkData.ExecuteSmetaGeoradScanVisible,
					ExecuteSmetaResponsibleUserVisible = operationalWorkData.ExecuteSmetaResponsibleUserVisible,
					ExecuteSmetaDtFactStartVisible = operationalWorkData.ExecuteSmetaDtFactStartVisible,
					ExecuteSmetaDtFactEndVisible = operationalWorkData.ExecuteSmetaDtFactEndVisible,
					ExecuteSmetaAdditionalVisible = operationalWorkData.ExecuteSmetaAdditionalVisible,
					MainSpecialistVisible = operationalWorkData.MainSpecialistVisible,
					DtActPassingStripSurveyVisible = operationalWorkData.DtActPassingStripSurveyVisible,
					DtActPassingRapperVisible = operationalWorkData.DtActPassingRapperVisible,
					DtTopoplanVisible = operationalWorkData.DtTopoplanVisible,
					DtLoadInParallelProjectionVisible = operationalWorkData.DtLoadInParallelProjectionVisible,
					DtProfilAndStatementVisible = operationalWorkData.DtProfilAndStatementVisible,
					DtTransferProfileIgiAndIgmiVisible = operationalWorkData.DtTransferProfileIgiAndIgmiVisible,
					DtUnloadingReportFromSapsanVisible = operationalWorkData.DtUnloadingReportFromSapsanVisible,
					DtNormocontrolVisible = operationalWorkData.DtNormocontrolVisible,
					ReasonVisible = operationalWorkData.ReasonVisible,
					HasRemarkVisible = operationalWorkData.HasRemarkVisible,
					DtKameralIgdiEndVisible = operationalWorkData.DtKameralIgdiEndVisible,
					DtForecastEndIgdiVisible = operationalWorkData.DtForecastEndIgdiVisible,
					VodotokCountVisible = operationalWorkData.VodotokCountVisible,
					CalculationsCountVisible = operationalWorkData.CalculationsCountVisible,
					DtSigningVisible = operationalWorkData.DtSigningVisible,
					PvoCountVisible = operationalWorkData.PvoCountVisible,
					SurvayFieldVisible = operationalWorkData.SurvayFieldVisible,
					SurvayTrackVisible = operationalWorkData.SurvayTrackVisible,
					TZVisible = operationalWorkData.TZVisible,
					DtEndVisible = operationalWorkData.DtEndVisible,
					TfoVisible = operationalWorkData.TfoVisible,
					ResponsibleUser2Visible = operationalWorkData.ResponsibleUser2Visible,
					DtPlanCompilationVisible = operationalWorkData.DtPlanCompilationVisible,
					DtSendCompilationVisible = operationalWorkData.DtSendCompilationVisible,
					DtSendVisible = operationalWorkData.DtSendVisible,
					FonVisible = operationalWorkData.FonVisible,
					PHHVisible = operationalWorkData.PHHVisible,
					DtIssuePrescriptionGOVisible = operationalWorkData.DtIssuePrescriptionGOVisible,
					PGRVisible = operationalWorkData.PGRVisible,
					IGSVisible = operationalWorkData.IGSVisible,
					PchvOaVisible = operationalWorkData.PchvOaVisible,
					PchvBakVisible = operationalWorkData.PchvBakVisible,
					PchvPrzVisible = operationalWorkData.PchvPrzVisible,
					AhSHVisible = operationalWorkData.AhSHVisible,
					AhPrVisible = operationalWorkData.AhPrVisible,
					PchvZsoVisible = operationalWorkData.PchvZsoVisible,
					PovVodVisible = operationalWorkData.PovVodVisible,
					PodzVodIpvsVisible = operationalWorkData.PodzVodIpvsVisible,
					PodzVodIgsVisible = operationalWorkData.PodzVodIgsVisible,
					DonVisible = operationalWorkData.DonVisible,
					AvVisible = operationalWorkData.AvVisible,
					GraSVisible = operationalWorkData.GraSVisible,
					RadVisible = operationalWorkData.RadVisible,
					ErnVisible = operationalWorkData.ErnVisible,
					FfVisible = operationalWorkData.FfVisible,
					ProtocolRadiationVisible = operationalWorkData.ProtocolRadiationVisible,
					DtForecastEndKameralIgdiVisible = operationalWorkData.DtForecastEndKameralIgdiVisible,
					DtForecastKameralIgiVisible = operationalWorkData.DtForecastKameralIgiVisible,
					DtForecastCameralIgmiVisible = operationalWorkData.DtForecastCameralIgmiVisible,
					HasProtocolFieldSurvayVisible = operationalWorkData.HasProtocolFieldSurvayVisible,
					HasGraficPartVisible = operationalWorkData.HasGraficPartVisible,
					FromOperationalWorkId = (Guid?)comboBoxFromOperationalWork.EditValue,
					VetstanciaVisible = operationalWorkData.VetstanciaVisible,
					DistrictsVisible = operationalWorkData.DistrictsVisible,
					PlanSmetaCountSkvazhinVisible = operationalWorkData.PlanSmetaCountSkvazhinVisible,
					PlanSmetaPmVisible = operationalWorkData.PlanSmetaPmVisible,
					PlanSmetaCountMonolitVisible = operationalWorkData.PlanSmetaCountMonolitVisible,
					ExecuteSmetaCountSkvazhinVisible = operationalWorkData.ExecuteSmetaCountSkvazhinVisible,
					ExecuteSmetaPmVisible = operationalWorkData.ExecuteSmetaPmVisible,
					ExecuteSmetaCountMonolitVisible = operationalWorkData.ExecuteSmetaCountMonolitVisible
				};

				if (_operationalWorkId.HasValue)
				{
					_operationalWorkService.Update(_operationalWorkId.Value, data);
				}
				else
				{
					_operationalWorkService.Add(data);
				}

				DialogResult = DialogResult.OK;
			});
		}

		private void buttonSaveCancelControl1_ButtonCancelClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}
	}
}