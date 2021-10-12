using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Sapsan.Modules.Operational.Presenters.Contracts;
using Sapsan.Modules.Operational.Services.Contracts;
using Sapsan.Modules.Operational.Services.Data;
using Sapsan2.Core.Service.Contracts;

namespace Sapsan.Modules.Operational.Dialogs
{
	public partial class OperationalEditorDialog : Form
	{
		private IOperationalWorkPresenter _operationalWorkPresenter;
		private IOperationalService _operationalService;
		private IOperationalWorkService _operationalWorkService;
		private IUserService _userService;
		private IErrorCatcher _errorCatcher;
		private ILogService _logService;
		private IOperationalEntityStatusService _operationalWorkStatusService;
		private IOperationalTypeTaskService _operationalTypeTaskService;

		public void Init(
			IOperationalWorkPresenter operationalWorkPresenter,
			IUserService userService,
			ILogService logService,
			IErrorCatcher errorCatcher,
			IOperationalService operationalService,
			IOperationalWorkService operationalWorkService,
			IOperationalEntityStatusService operationalWorkStatusService,
			IOperationalTypeTaskService operationalTypeTaskService)
		{
			_operationalWorkPresenter = operationalWorkPresenter;
			_userService = userService;
			_logService = logService;
			_errorCatcher = errorCatcher;
			_operationalService = operationalService;
			_operationalWorkService = operationalWorkService;
			_operationalWorkStatusService = operationalWorkStatusService;
			_operationalTypeTaskService = operationalTypeTaskService;

			InitializeComponent();

			operationalWorkControl.Init(_operationalWorkPresenter,
				_userService,
				_logService,
				_errorCatcher,
				_operationalService,
				_operationalWorkService,
				_operationalWorkStatusService);
			operationalWorkControl.LoadData();

			dataGridControlAddBtn.Init(_errorCatcher, _logService, _userService, typeof(OperationalTypeTaskData),
				nameof(OperationalTypeTaskData.Id), contextMenuStrip, "OperationalTypeTask", true,
				(o, e) => { LoadDataTypeTask(); });

			dataGridControlAddBtn.SetSimpleGridView();

			dataGridControlAddBtn.BtnAddClick += добавитьToolStripMenuItem_Click;
			LoadDataTypeTask();
		}

		public void LoadDataTypeTask()
		{
			var sourceData = _operationalTypeTaskService.GetListWithDeleted().ToList();

			dataGridControlAddBtn.populateDataSource(sourceData);
		}

		private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_errorCatcher.Do(() =>
			{
				var data = (OperationalTypeTaskData)dataGridControlAddBtn.GetFocusedRow();

				var d = new OperationalTypeTaskDialog(_operationalTypeTaskService);

				d.LoadData(data.Id);

				if (d.ShowDialog() == DialogResult.OK)
				{
					LoadDataTypeTask();
				}
			});
		}

		private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_errorCatcher.Do(() =>
			{
				var d = new OperationalTypeTaskDialog(_operationalTypeTaskService);

				d.LoadData();

				if (d.ShowDialog() == DialogResult.OK)
				{
					LoadDataTypeTask();
				}
			});
		}

		private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_errorCatcher.Do(() =>
			{
				if (MessageBox.Show("Удалить выбранный тип задания?", "Внимание", MessageBoxButtons.OKCancel) ==
					DialogResult.OK)
				{
					var data = (OperationalTypeTaskData)dataGridControlAddBtn.GetFocusedRow();
					_operationalTypeTaskService.Delete(data.Id);
					LoadDataTypeTask();
				}
			});
		}

		private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			var data = (OperationalTypeTaskData)dataGridControlAddBtn.GetFocusedRow();
			if (data == null)
			{
				e.Cancel = true;
				return;
			}

			удалитьToolStripMenuItem.Enabled = редактироватьToolStripMenuItem.Enabled = data.DtDelete.HasValue == false;
		}
	}
}