using System;
using System.Linq;
using System.Windows.Forms;
using Sapsan.Modules.ProjectScheduler.Contracts;
using Sapsan.Modules.ProjectScheduler.Enums;
using Sapsan.Modules.ProjectScheduler.Helpers;
using Sapsan.Modules.ProjectScheduler.Helpers.Data;
using Sapsan.Modules.ProjectScheduler.Presenters;
using Sapsan.Modules.ProjectScheduler.Services;
using Sapsan.Modules.ProjectScheduler.Services.Contracts;
using Sapsan.Modules.ProjectScheduler.Services.Data;
using Sapsan2.Core.Contracts;
using Sapsan2.Core.Presenters.Contracts;
using Sapsan2.Core.Service.Contracts;
using SUVPP.UI.Dialogs.Common;

namespace Sapsan.Modules.Operational.Dialogs
{
	public partial class PreProjectDialog : FormWithEscape
	{
		private Guid _contractId;
		private readonly IDesignBasicService _designBasicService;
		private Guid? _mainSubdivisionId;
		private readonly IWorkStructureDesignBasicService _workStructureDesignBasicService;
		private readonly IContractService _contractService;
		private readonly PlanService _planService;
		private readonly WorkStructureService _workService;
		private readonly TaskService _taskService;
		private readonly TaskTransferTaskService _taskTransferTaskService;
		private readonly TaskWorkTimeService _taskWorkTimeService;
		private readonly WorkStructureWorkTimeService _workStructureWorkTimeService;
		private readonly ICalendarService _durationCalcHelper;
		private readonly IPlanTemplateService _planTemplateService;
		private readonly IUserSettingsService _userSettingsService;
		private readonly PlanTransferTaskPresenter _planTransferTaskPresenter;
		private readonly CreateTransferTaskHelper _createTransferTaskHelper;
		private readonly ISubdivisionService _subdivisionService;
		private readonly IRegistrySaveControlService _registrySaveControlService;
		private readonly PlanCalcHelper _planCalcHelper;
		private readonly INotificationService _notificationService;
		private readonly IUserService _userService;
		private readonly IIdentityService _identityService;
		private readonly IUserPresenter _userPresenter;
		private readonly PlanForecastCalcHelper _planForecastCalcHelper;
		private readonly IErrorCatcher _errorCatcher;
		private readonly ICalendarService _calendarService;
		private readonly IDateTimeProvider _dateTimeProvider;

		public PreProjectDialog(
			PlanService planService,
			WorkStructureService workService,
			TaskService taskService,
			TaskTransferTaskService taskTransferTaskService,
			TaskWorkTimeService taskWorkTimeService,
			WorkStructureWorkTimeService workStructureWorkTimeService,
			IContractService contractService,
			ICalendarService durationCalcHelper,
			IPlanTemplateService planTemplateService,
			IUserSettingsService userSettingsService,
			ISubdivisionService subdivisionService,
			PlanTransferTaskPresenter planTransferTaskPresenter,
			CreateTransferTaskHelper createTransferTaskHelper,
			IRegistrySaveControlService registrySaveControlService,
			PlanCalcHelper planCalcHelper,
			INotificationService notificationService,
			IUserService userService,
			IIdentityService identityService,
			IUserPresenter userPresenter,
			PlanForecastCalcHelper planForecastCalcHelper,
			IErrorCatcher errorCatcher,
			ICalendarService calendarService,
			IDateTimeProvider dateTimeProvider,
			Guid contractId,
			IDesignBasicService designBasicService,
			IWorkStructureDesignBasicService workStructureDesignBasicService
		)
		{
			_planService = planService;
			_workService = workService;
			_taskService = taskService;
			_taskTransferTaskService = taskTransferTaskService;
			_taskWorkTimeService = taskWorkTimeService;
			_workStructureWorkTimeService = workStructureWorkTimeService;
			_contractService = contractService;
			_durationCalcHelper = durationCalcHelper;
			_planTemplateService = planTemplateService;

			_userSettingsService = userSettingsService;
			_planTransferTaskPresenter = planTransferTaskPresenter;
			_createTransferTaskHelper = createTransferTaskHelper;
			_subdivisionService = subdivisionService;
			_registrySaveControlService = registrySaveControlService;
			_planCalcHelper = planCalcHelper;

			_notificationService = notificationService;
			_userService = userService;
			_identityService = identityService;
			_userPresenter = userPresenter;
			_planForecastCalcHelper = planForecastCalcHelper;
			_errorCatcher = errorCatcher;
			_calendarService = calendarService;
			_dateTimeProvider = dateTimeProvider;
			_contractId = contractId;
			_designBasicService = designBasicService;
			_workStructureDesignBasicService = workStructureDesignBasicService;

			InitializeComponent();

			secondLevelControl.Init(_planTemplateService,
				_contractService,
				_userSettingsService,
				_workService,
				_userService,
				_identityService,
				_userPresenter,
				_notificationService,
				_subdivisionService,
				_taskService,
				_taskTransferTaskService,
				_planService,
				_calendarService,
				_dateTimeProvider,
				_contractId);

			workStructureLookUpControl.Init(_workService, _designBasicService, _workStructureDesignBasicService);

			luSubdivision.Init(_subdivisionService, true);

			workStructureLookUpControl.ValueChanged += WorkStructureLookUpControl_ValueChanged;
		}

		public void LoadData()
		{
			_mainSubdivisionId = _contractService.Get(_contractId).MainSubdivisionId;

			luSubdivision.SelectedValue = _mainSubdivisionId;

			var maiPlanId = _planService.GetList().FirstOrDefault(x => x.ContractId == _contractId && x.IsMain)?.Id;

			if (maiPlanId.HasValue)
			{
				var workWithoutSecondLevelIds = _workService.GetList().Where(x => x.PlanId == maiPlanId.Value &&
					x.Status == (int)StatusEnum.Активный &&
					x.SecondLevelPlanId.HasValue == false).Select(x => x.Id).ToArray();

				workStructureLookUpControl.LoadData(workWithoutSecondLevelIds);
			}

			secondLevelControl.LoadData(workStructureLookUpControl.SelectedValue);
		}

		private void ButtonSaveCancelControl1_ButtonSaveClick(object sender, EventArgs e)
		{
			_errorCatcher.Do(() =>
			{
				if (luSubdivision.SelectedValue.HasValue == false || luSubdivision.SelectedSubdivision == null)
				{
					MessageBox.Show("Укажите ведущий отдел.");
					return;
				}

				if (workStructureLookUpControl.SelectedValue.HasValue == false)
				{
					MessageBox.Show("Выберите группу работ из целевого план-графика.");
					return;
				}

				if (secondLevelControl.CheckData() != true)
				{
					return;
				}

				var helper = new CopyTaskHelper(
					_workService,
					_taskService,
					_taskTransferTaskService,
					_workStructureWorkTimeService,
					_taskWorkTimeService,
					_durationCalcHelper,
					_planCalcHelper);

				var secondLevelPlanId = _planService.Add(new PlanData
				{
					ContractId = _contractId,
					Name = secondLevelControl.PlanName,
					TypeId = PlanTypeEnum.График_передачи_заданий,
					ConformIsChecked = false
				});

				var workId = workStructureLookUpControl.SelectedValue.Value;

				var workInfo = _workService.GetList().FirstOrDefault(x => x.Id == workId);

				var copySettings = new PlanCopySettings
				{
					FromContracId = secondLevelControl.TemplateContractId.Value,
					ToContracId = _contractId,
					FromPlanId = secondLevelControl.TemplatePlanId.Value,
					ToPlanId = secondLevelPlanId,
					StartDate = secondLevelControl.StartDate.Value,
					FinishDate = secondLevelControl.FinishDate.Value,
					CopyCode = false,
					CopyNumEtap = false,
					CopyDateFact = false,
					CopyDateForecast = false,
					FromWorkId = null,
					CopyStatus = false
				};

				helper.CopyPlanToPeriod(copySettings);

				_workService.SetSecondLevelPlanId(workId, secondLevelPlanId);

				_planForecastCalcHelper.RecalcPlanForecast(workInfo.PlanId);

				_contractService.SetMainSubdivisionId(_contractId, luSubdivision.SelectedValue.Value);

				if (secondLevelControl.CreateTransferTasks)
				{
					_createTransferTaskHelper.CreateTransferTasks(_contractId,
						_planTransferTaskPresenter.GetData(secondLevelPlanId));
				}


				DialogResult = DialogResult.OK;
			});
		}

		private void ButtonSaveCancelControl1_ButtonCancelClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		private void WorkStructureLookUpControl_ValueChanged(object sender, EventArgs e)
		{
			secondLevelControl.LoadData(workStructureLookUpControl.SelectedValue);
		}
	}
}