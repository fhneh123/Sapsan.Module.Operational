using System;
using System.Windows.Forms;
using Sapsan.Modules.Operational.Services.Contracts;
using Sapsan2.Core.Service.Contracts;

namespace Sapsan.Modules.Operational.Dialogs
{
	public partial class OperationalEntityStatusEditorDialog : Form
	{
		private IErrorCatcher _errorCatcher;
		private ILogService _logService;
		private IUserService _userService;
		private IOperationalEntityStatusService _operationalWorkStatusService;

		private Guid? _operationalWorkId;
		private string _entityId;

		public void Init(
			IErrorCatcher errorCatcher,
			ILogService logService,
			IUserService userService,
			IOperationalEntityStatusService operationalWorkStatusService,
			string dialogName,
			Guid? operationalWorkId,
			string entityId)
		{
			_errorCatcher = errorCatcher;
			_logService = logService;
			_userService = userService;
			_operationalWorkStatusService = operationalWorkStatusService;

			InitializeComponent();

			Text = dialogName;

			statusEditorControl.Init(
				_errorCatcher,
				_logService,
				_userService,
				_operationalWorkStatusService,
				dialogName,
				operationalWorkId,
				entityId);
		}
	}
}