using System;
using System.ComponentModel;
using System.Windows.Forms;
using Sapsan.Modules.Operational.Controls;
using Sapsan.Modules.Operational.Enums;
using Sapsan.Modules.Operational.Presenters.Contracts;
using Sapsan.Modules.Operational.Presenters.Data;
using Sapsan.Modules.Operational.Services.Contracts;
using Sapsan2.Core.Service.Contracts;

namespace Sapsan.Modules.Operational.Dialogs
{
	public partial class SpfoaEditorDialog : Form
	{
		private IUserService _userService;
		private IErrorCatcher _errorCatcher;
		private ILogService _logService;
		private IOperationalEntityStatusService _operationalWorkStatusService;
		private IOperationalSpfoaEditorService _operatonalSpfoaEditorService;
		private IOperationalSpfoaEditorPresenter _operationalSpfoaEditorPresenter;
		private IOperationalService _operationalService;
		private IOperationalWorkService _operationalWorkService;

		private StatusEditorControl _iiStatusEditorControl = new StatusEditorControl();
		private StatusEditorControl _executeStatusEditorControl = new StatusEditorControl();

		public void Init(
			IUserService userService,
			ILogService logService,
			IErrorCatcher errorCatcher,
			IOperationalEntityStatusService operationalWorkStatusService,
			IOperationalSpfoaEditorService operatonalSpfoaEditorService,
			IOperationalSpfoaEditorPresenter operationalSpfoaEditorPresenter,
			IOperationalService operationalService,
			IOperationalWorkService operationalWorkService)
		{
			_userService = userService;
			_logService = logService;
			_errorCatcher = errorCatcher;
			_operationalWorkStatusService = operationalWorkStatusService;
			_operatonalSpfoaEditorService = operatonalSpfoaEditorService;
			_operationalSpfoaEditorPresenter = operationalSpfoaEditorPresenter;
			_operationalService = operationalService;
			_operationalWorkService = operationalWorkService;

			InitializeComponent();

			dataGridControlAddBtn.Init(_errorCatcher, _logService, _userService,
				typeof(OperationalSpfoaEditorPresenterData), nameof(OperationalSpfoaEditorPresenterData.Id),
				contextMenuStrip);

			dataGridControlAddBtn.SetSimpleGridView();

			dataGridControlAddBtn.BtnAddClick += добавитьToolStripMenuItem_Click;
		}

		public void LoadData()
		{
			dataGridControlAddBtn.populateDataSource(_operationalSpfoaEditorPresenter.GetData());
			dataGridControlAddBtn.SetGroupIndexForGridViewColumns(new[] { 1 });
		}

		private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_errorCatcher.Do(() =>
			{
				var d = new SpfoaPgDateEditDialog();
				d.Init(
					_operationalService,
					_operationalWorkService,
					_operationalWorkStatusService,
					_operatonalSpfoaEditorService,
					_errorCatcher);
				d.LoadData();

				if (d.ShowDialog() == DialogResult.OK)
				{
					LoadData();
				}
			});
		}

		private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (tabControl.SelectedTab == tabPageStatusII && _iiStatusEditorControl.IsInitUi == false)
			{
				_iiStatusEditorControl.Init(
					_errorCatcher,
					_logService,
					_userService,
					_operationalWorkStatusService,
					"Редактор статусов ИИ",
					null,
					OperationalStatusEntityIdEnum.IiStatusEntityId);

				_iiStatusEditorControl.Dock = DockStyle.Fill;
				tabPageStatusII.Controls.Add(_iiStatusEditorControl);
			}

			if (tabControl.SelectedTab == tabPageStatusExecute && _executeStatusEditorControl.IsInitUi == false)
			{
				_executeStatusEditorControl.Init(
					_errorCatcher,
					_logService,
					_userService,
					_operationalWorkStatusService,
					"Редактор статусов выполнения",
					null,
					OperationalStatusEntityIdEnum.ExecuteStatusEntityId);

				_executeStatusEditorControl.Dock = DockStyle.Fill;
				tabPageStatusExecute.Controls.Add(_executeStatusEditorControl);
			}
		}

		private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_errorCatcher.Do(() =>
			{
				var data = (OperationalSpfoaEditorPresenterData)dataGridControlAddBtn.GetFocusedRow();

				var d = new SpfoaPgDateEditDialog();
				d.Init(
					_operationalService,
					_operationalWorkService,
					_operationalWorkStatusService,
					_operatonalSpfoaEditorService,
					_errorCatcher);
				d.LoadData(data.Id);

				if (d.ShowDialog() == DialogResult.OK)
				{
					LoadData();
				}
			});
		}

		private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_errorCatcher.Do(() =>
			{
				if (MessageBox.Show("Удалить связь СПФОА с ПГ?", "Внимание", MessageBoxButtons.OKCancel) ==
					DialogResult.OK)
				{
					var data = (OperationalSpfoaEditorPresenterData)dataGridControlAddBtn.GetFocusedRow();
					_operatonalSpfoaEditorService.Delete(data.Id);
					LoadData();
				}
			});
		}

		private void историяToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_errorCatcher.Do(() =>
			{
				var data = (OperationalSpfoaEditorPresenterData)dataGridControlAddBtn.GetFocusedRow();

				var d = new SUVPP.UI.Dialogs.Common.HistoryDialog(_logService, _userService);
				d.Init("История", data.Id);
				d.ShowDialog();
			});
		}

		private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			var data = (OperationalSpfoaEditorPresenterData)dataGridControlAddBtn.GetFocusedRow();
			if (data == null)
			{
				e.Cancel = true;
				return;
			}

			удалитьToolStripMenuItem.Enabled = редактироватьToolStripMenuItem.Enabled = data.DtDelete.HasValue == false;
		}
	}
}