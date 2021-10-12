using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Sapsan.Modules.Operational.Presenters.Contracts;
using Sapsan.Modules.Operational.Services.Contracts;
using Sapsan.Modules.ProjectScheduler.Services;
using Sapsan2.Contracts.Contracts.UI;
using Sapsan2.Core.Contracts;
using Sapsan2.Core.Service.Contracts;

namespace Sapsan.Modules.Operational.Controls
{
	public partial class OperationalMainControl : UserControl, ILentaPage
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
		private IOperationalSpfoaPresenter _operationalSpfoaPresenter;
		private IUserSettingsService _userSettingsService;
		private IOperationalSpfoaEditorService _operationalSpfoaEditorService;
		private IOperationalSpfoaEditorPresenter _operationalSpfoaEditorPresenter;
		private IOperationalSpfoaService _operationalSpfoaService;
		private IOperationalTypeTaskService _operationalTypeTaskService;
		private LentaPageButton_ItemClickHandler _lentaPageButton_ItemClicked;

		private OperationalControl _operationalControl = new OperationalControl();
		private OperationalSpfoaControl _operationalSpfoaControl = new OperationalSpfoaControl();

		public OperationalMainControl(
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
			IOperationalSpfoaPresenter operationalSpfoaPresenter,
			IUserSettingsService userSettingsService,
			IOperationalSpfoaEditorService operationalSpfoaEditorService,
			IOperationalSpfoaEditorPresenter operationalSpfoaEditorPresenter,
			IOperationalSpfoaService operationalSpfoaService,
			IOperationalTypeTaskService operationalTypeTaskService)
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
			_operationalSpfoaPresenter = operationalSpfoaPresenter;
			_userSettingsService = userSettingsService;
			_operationalSpfoaEditorService = operationalSpfoaEditorService;
			_operationalSpfoaEditorPresenter = operationalSpfoaEditorPresenter;
			_operationalSpfoaService = operationalSpfoaService;
			_operationalTypeTaskService = operationalTypeTaskService;
		}

		public Guid ModuleId => Module.ModuleId;

		public bool IsInitUi { get; private set; }

		public Guid Id => Guid.Parse("fe385ae1-f2d8-4d60-97db-1a3407d04918");

		public string PageName => "Главная";

		public string PageGroupName => "Главное меню";

		public string ButtonName
		{
			get { return Module.ModuleName; }
		}

		public Image GetButtonImage()
		{
			return Properties.Resources.icon;
		}

		public event LentaPageButton_ItemClickHandler LentaPageButton_ItemClick
		{
			add
			{
				if (_lentaPageButton_ItemClicked == null ||
					_lentaPageButton_ItemClicked.GetInvocationList().Select(x => x.Method).Contains(value.Method) == false)
				{
					_lentaPageButton_ItemClicked += value;
				}
			}
			remove { _lentaPageButton_ItemClicked -= value; }
		}

		public object InitializeUIControl()
		{
			if (!IsInitUi)
			{
				InitializeComponent();

				_operationalControl.InitializeUIControl(
					_operationalLineService,
					_operationalLinePresenter,
					_operationalService,
					_operationalWorkService,
					_operationalWorkLineService,
					_errorCatcher,
					_logService,
					_userService,
					_planService,
					_workStructureService,
					_taskService,
					_identityService,
					_contractService,
					_designBasicService,
					_operationalWorkStatusService,
					_applicationPathService,
					_operationalWorkPresenter,
					_subdivisionService,
					_userSettingsService,
					_operationalTypeTaskService);

				_operationalControl.Dock = DockStyle.Fill;
				tabPageOperationalLines.Controls.Add(_operationalControl);

				IsInitUi = true;
			}

			return this;
		}

		public void LoadData()
		{
		}

		private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (tabControl.SelectedTab == tabPageSpfoa && _operationalSpfoaControl.IsInitUi == false)
			{
				_operationalSpfoaControl.InitializeUIControl(
					_errorCatcher,
					_applicationPathService,
					_logService,
					_userService,
					_operationalWorkStatusService,
					_operationalSpfoaPresenter,
					_operationalSpfoaEditorService,
					_operationalService,
					_operationalWorkService,
					_operationalSpfoaEditorPresenter,
					_operationalSpfoaService,
					_identityService);

				_operationalSpfoaControl.Dock = DockStyle.Fill;
				tabPageSpfoa.Controls.Add(_operationalSpfoaControl);
			}
		}
	}
}