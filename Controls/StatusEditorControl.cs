using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Sapsan.Modules.Operational.Dialogs;
using Sapsan.Modules.Operational.Services.Contracts;
using Sapsan.Modules.Operational.Services.Data;
using Sapsan2.Core.Service.Contracts;

namespace Sapsan.Modules.Operational.Controls
{
	public partial class StatusEditorControl : UserControl
	{
		private IErrorCatcher _errorCatcher;
		private ILogService _logService;
		private IUserService _userService;
		private IOperationalEntityStatusService _operationalWorkStatusService;

		private Guid? _operationalWorkId;
		private string _entityId;

		public bool IsInitUi { get; private set; }

		public void Init(
			IErrorCatcher errorCatcher,
			ILogService logService,
			IUserService userService,
			IOperationalEntityStatusService operationalWorkStatusService,
			string dialogName,
			Guid? operationalWorkId,
			string entityId)
		{
			if (!IsInitUi)
			{
				_errorCatcher = errorCatcher;
				_logService = logService;
				_userService = userService;
				_operationalWorkStatusService = operationalWorkStatusService;

				InitializeComponent();

				Text = dialogName;

				dataGridControlAddBtn.Init(_errorCatcher, _logService, _userService,
					typeof(OperationalEntityStatusData), nameof(OperationalEntityStatusData.Id), contextMenuStrip);

				dataGridControlAddBtn.SetSimpleGridView();

				dataGridControlAddBtn.BtnAddClick += добавитьToolStripMenuItem_Click;

				_operationalWorkId = operationalWorkId;
				_entityId = entityId;

				Refresh();

				IsInitUi = true;
			}
		}

		private void Refresh()
		{
			var sourceData = _operationalWorkStatusService.GetListWithDeleted().Where(x =>
				x.OperationalWorkId == _operationalWorkId
				&& x.EntityId == _entityId).ToList();

			dataGridControlAddBtn.populateDataSource(sourceData);
		}

		private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_errorCatcher.Do(() =>
			{
				var d = new StatusEditDialog(_operationalWorkStatusService);

				d.LoadData(null, _operationalWorkId, _entityId);

				if (d.ShowDialog() == DialogResult.OK)
				{
					Refresh();
				}
			});
		}

		private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_errorCatcher.Do(() =>
			{
				var status = (OperationalEntityStatusData)dataGridControlAddBtn.GetFocusedRow();

				var d = new StatusEditDialog(_operationalWorkStatusService);

				d.LoadData(status.Id);

				if (d.ShowDialog() == DialogResult.OK)
				{
					Refresh();
				}
			});
		}

		private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_errorCatcher.Do(() =>
			{
				if (MessageBox.Show("Удалить выбранный статус?", "Внимание", MessageBoxButtons.OKCancel) ==
					DialogResult.OK)
				{
					var status = (OperationalEntityStatusData)dataGridControlAddBtn.GetFocusedRow();
					_operationalWorkStatusService.Delete(status.Id);
					Refresh();
				}
			});
		}

		private void историяToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_errorCatcher.Do(() =>
			{
				var operational = (OperationalEntityStatusData)dataGridControlAddBtn.GetFocusedRow();

				var d = new SUVPP.UI.Dialogs.Common.HistoryDialog(_logService, _userService);
				d.Init("История", operational.Id);
				d.ShowDialog();
			});
		}

		private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			var data = (OperationalEntityStatusData)dataGridControlAddBtn.GetFocusedRow();
			if (data == null)
			{
				e.Cancel = true;
				return;
			}

			удалитьToolStripMenuItem.Enabled = редактироватьToolStripMenuItem.Enabled = data.DtDelete.HasValue == false;
		}
	}
}