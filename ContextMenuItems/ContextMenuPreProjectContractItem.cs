using System;
using System.Drawing;
using Sapsan.Modules.Operational.Dialogs;
using Sapsan.Modules.ProjectScheduler.Contracts;
using Sapsan.Modules.ProjectScheduler.Helpers;
using Sapsan.Modules.ProjectScheduler.Presenters;
using Sapsan.Modules.ProjectScheduler.Services;
using Sapsan.Modules.ProjectScheduler.Services.Contracts;
using Sapsan2.Contracts;
using Sapsan2.Contracts.Contracts.UI.ContextMenu;
using Sapsan2.Core.Contracts;
using Sapsan2.Core.Presenters.Contracts;
using Sapsan2.Core.Service.Contracts;

namespace Sapsan.Modules.Operational.ContextMenuItems
{
	public class ContextMenuPreProjectContractItem : IContextMenuTreeContractItem
	{
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
		private readonly IErrorCatcher _errorCatcher;
		private readonly PlanCalcHelper _planCalcHelper;
		private readonly INotificationService _notificationService;
		private readonly IUserService _userService;
		private readonly IIdentityService _identityService;
		private readonly IUserPresenter _userPresenter;
		private readonly PlanForecastCalcHelper _planForecastCalcHelper;
		private readonly ICalendarService _calendarService;
		private readonly IDateTimeProvider _dateTimeProvider;
		private readonly IDesignBasicService _designBasicService;
		private readonly IWorkStructureDesignBasicService _workStructureDesignBasicService;

		public ContextMenuPreProjectContractItem(
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
			IErrorCatcher errorCatcher,
			PlanCalcHelper planCalcHelper,
			INotificationService notificationService,
			IUserService userService,
			IIdentityService identityService,
			IUserPresenter userPresenter,
			PlanForecastCalcHelper planForecastCalcHelper,
			ICalendarService calendarService,
			IDateTimeProvider dateTimeProvider,
			IDesignBasicService designBasicService,
			IWorkStructureDesignBasicService workStructureDesignBasicService)
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
			_errorCatcher = errorCatcher;
			_planCalcHelper = planCalcHelper;
			_notificationService = notificationService;
			_userService = userService;
			_identityService = identityService;
			_userPresenter = userPresenter;
			_planForecastCalcHelper = planForecastCalcHelper;
			_calendarService = calendarService;
			_dateTimeProvider = dateTimeProvider;
			_designBasicService = designBasicService;
			_workStructureDesignBasicService = workStructureDesignBasicService;
		}


		public IToolStripItem[] ChildItems
		{
			get { return null; }
		}

		public Image Image => null;

		public Guid Id
		{
			get { return Guid.Parse("6C3222C5-C693-47DF-A394-061B95D8C5AB"); }
		}

		public Guid ModuleId
		{
			get { return Module.ModuleId; }
		}

		public string Name
		{
			get { return "Предпроект..."; }
		}

		public bool CanShow(Guid contractId)
		{
			return true;
		}

		public TreeMenuItemResult Handle(Guid contractId)
		{
			_errorCatcher.Do(() =>
			{
				var d = new PreProjectDialog(_planService,
					_workService,
					_taskService,
					_taskTransferTaskService,
					_taskWorkTimeService,
					_workStructureWorkTimeService,
					_contractService,
					_durationCalcHelper,
					_planTemplateService,
					_userSettingsService,
					_subdivisionService,
					_planTransferTaskPresenter,
					_createTransferTaskHelper,
					_registrySaveControlService,
					_planCalcHelper,
					_notificationService,
					_userService,
					_identityService,
					_userPresenter,
					_planForecastCalcHelper,
					_errorCatcher,
					_calendarService,
					_dateTimeProvider,
					contractId,
					_designBasicService,
					_workStructureDesignBasicService);

				d.LoadData();

				d.ShowDialog();
			});

			return null;
		}
	}
}