using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.Utils;
using Sapsan.Modules.Operational.Dialogs;
using Sapsan.Modules.Operational.Presenters.Contracts;
using Sapsan.Modules.Operational.Presenters.Data;
using Sapsan.Modules.Operational.Services.Contracts;
using Sapsan.Modules.Operational.Services.Data;
using Sapsan2.Core.Service.Contracts;

namespace Sapsan.Modules.Operational.Controls.Admin
{
	public partial class OperationalWorkControl : UserControl
	{
		private IOperationalWorkPresenter _operationalWorkPresenter;
		private IOperationalService _operationalService;
		private IOperationalWorkService _operationalWorkService;
		private IUserService _userService;
		private IErrorCatcher _errorCatcher;
		private ILogService _logService;
		private IOperationalEntityStatusService _operationalWorkStatusService;

		public void Init(
			IOperationalWorkPresenter operationalWorkPresenter,
			IUserService userService,
			ILogService logService,
			IErrorCatcher errorCatcher,
			IOperationalService operationalService,
			IOperationalWorkService operationalWorkService,
			IOperationalEntityStatusService operationalWorkStatusService)
		{
			_operationalWorkPresenter = operationalWorkPresenter;
			_userService = userService;
			_logService = logService;
			_errorCatcher = errorCatcher;
			_operationalService = operationalService;
			_operationalWorkService = operationalWorkService;
			_operationalWorkStatusService = operationalWorkStatusService;

			InitializeComponent();

			dataGridControlAddBtn.Init(_errorCatcher, _logService, _userService,
				typeof(OperationalWorkFullInfoPresenterData), nameof(OperationalWorkFullInfoPresenterData.Id),
				contextMenuStrip);

			dataGridControlAddBtn.SetSimpleGridView();

			dataGridControlAddBtn.ColumnPanelRowHeight = 60;

			dataGridControlAddBtn.HeaderPanelWordWrap = WordWrap.Wrap;

			dataGridControlAddBtn.BtnAddClick += добавитьToolStripMenuItem_Click;
		}

		public void LoadData()
		{
			dataGridControlAddBtn.populateDataSource(_operationalWorkPresenter.GetData());
			dataGridControlAddBtn.SetGroupIndexForGridViewColumns(new[] { 1 });
			dataGridControlAddBtn.SetWidthForGridViewColumns(new int[] { });
		}

		private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_errorCatcher.Do(() =>
			{
				var d = new OperationalWorkEditDialog();
				d.Init(_operationalService, _operationalWorkService, _errorCatcher, _logService, _userService,
					_operationalWorkStatusService);
				d.LoadData();

				if (d.ShowDialog() == DialogResult.OK)
				{
					LoadData();
				}
			});
		}

		private void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_errorCatcher.Do(() =>
			{
				var operationalWork = (OperationalWorkFullInfoPresenterData)dataGridControlAddBtn.GetFocusedRow();

				var d = new OperationalWorkEditDialog();
				d.Init(_operationalService, _operationalWorkService, _errorCatcher, _logService, _userService,
					_operationalWorkStatusService);
				d.LoadData(operationalWork.Id);

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
				if (MessageBox.Show("Удалить выбранную работу?", "Внимание", MessageBoxButtons.OKCancel) ==
					DialogResult.OK)
				{
					var operational = (OperationalWorkFullInfoPresenterData)dataGridControlAddBtn.GetFocusedRow();
					_operationalWorkService.Delete(operational.Id);
					LoadData();
				}
			});
		}

		private void историяToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_errorCatcher.Do(() =>
			{
				var operational = (OperationalWorkFullInfoPresenterData)dataGridControlAddBtn.GetFocusedRow();

				var d = new SUVPP.UI.Dialogs.Common.HistoryDialog(_logService, _userService);
				d.Init("История", operational.Id);
				d.ShowDialog();
			});
		}

		private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			var data = (OperationalWorkFullInfoPresenterData)dataGridControlAddBtn.GetFocusedRow();
			if (data == null)
			{
				e.Cancel = true;
				return;
			}

			удалитьToolStripMenuItem.Enabled = редактироватьToolStripMenuItem.Enabled = data.DtDelete.HasValue == false;
			редакторСтатусовToolStripMenuItem.Enabled = data.DtDelete.HasValue == false && data.StatusVisible && data.FromOperationalWorkId.HasValue == false;
			редакторПричинToolStripMenuItem.Enabled = data.DtDelete.HasValue == false && data.ReasonVisible && data.FromOperationalWorkId.HasValue == false;
		}

		private void редакторСтатусовToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_errorCatcher.Do(() =>
			{
				var operationalWork = (OperationalWorkFullInfoPresenterData)dataGridControlAddBtn.GetFocusedRow();
				var d = new OperationalEntityStatusEditorDialog();
				d.Init(_errorCatcher,
					_logService,
					_userService,
					_operationalWorkStatusService,
					"Редактор статусов",
					operationalWork.Id,
					nameof(OperationalWorkLineData.Status));

				d.ShowDialog();
			});
		}

		private void редакторПричинToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_errorCatcher.Do(() =>
			{
				var operationalWork = (OperationalWorkFullInfoPresenterData)dataGridControlAddBtn.GetFocusedRow();
				var d = new OperationalEntityStatusEditorDialog();
				d.Init(_errorCatcher,
					_logService,
					_userService,
					_operationalWorkStatusService,
					"Редактор причин",
					operationalWork.Id,
					nameof(OperationalWorkLineData.Reason));

				d.ShowDialog();
			});
		}
	}
}