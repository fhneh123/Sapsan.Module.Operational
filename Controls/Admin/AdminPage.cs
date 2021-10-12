using System;
using System.Windows.Forms;
using Sapsan.Modules.Operational.Presenters.Contracts;
using Sapsan.Modules.Operational.Services.Contracts;
using Sapsan.Modules.ProjectScheduler.Services;
using Sapsan2.Contracts.Contracts.UI;
using Sapsan2.Core.Service.Contracts;

namespace Sapsan.Modules.Operational.Controls.Admin
{
	public partial class AdminPage : UserControl, IAdminPage
	{
		private readonly IOperationalService _operationalService;
		private readonly IErrorCatcher _errorCatcher;
		private readonly ILogService _logService;
		private readonly IUserService _userService;
		private readonly IOperationalWorkPresenter _operationalWorkPresenter;
		private readonly IOperationalWorkService _operationalWorkService;
		private readonly IOperationalPresenter _operationalPresenter;
		private readonly WorkStructureSprService _workStructureSprService;
		private readonly IOperationalEntityStatusService _operationalWorkStatusService;

		private AdminOperationalControl _adminOperationalControl;
		private OperationalWorkControl _adminOperationalWorkControl;

		public AdminPage(
			IOperationalService operationalService,
			IErrorCatcher errorCatcher,
			ILogService logService,
			IUserService userService,
			IOperationalWorkPresenter operationalWorkPresenter,
			IOperationalWorkService operationalWorkService,
			IOperationalPresenter operationalPresenter,
			WorkStructureSprService workStructureSprService,
			IOperationalEntityStatusService operationalWorkStatusService)
		{
			_operationalService = operationalService;
			_errorCatcher = errorCatcher;
			_logService = logService;
			_userService = userService;
			_operationalWorkPresenter = operationalWorkPresenter;
			_operationalWorkService = operationalWorkService;
			_operationalPresenter = operationalPresenter;
			_workStructureSprService = workStructureSprService;
			_operationalWorkStatusService = operationalWorkStatusService;
		}

		string IAdminPage.Name
		{
			get { return "Оперативка"; }
		}

		public Guid ModuleId => Module.ModuleId;

		public bool IsInitUi { get; private set; }

		public Guid Id => Guid.Parse("b23405ab-f011-4027-bf76-7afc957c527e");

		public object InitializeUIControl()
		{
			if (!IsInitUi)
			{
				InitializeComponent();
				LoadData();
				IsInitUi = true;
			}

			return this;
		}

		public void LoadData()
		{
			if (_adminOperationalControl == null)
			{
				_adminOperationalControl = new AdminOperationalControl(_operationalService,
					_errorCatcher,
					_logService,
					_userService,
					_operationalPresenter,
					_workStructureSprService);

				_adminOperationalControl.Dock = DockStyle.Fill;
				pageOperational.Controls.Add(_adminOperationalControl);
				_adminOperationalControl.LoadData();
			}
		}
	}
}