using System;
using System.ComponentModel;
using System.Windows.Forms;
using Sapsan.Modules.Operational.Dialogs;
using Sapsan.Modules.Operational.Presenters.Contracts;
using Sapsan.Modules.Operational.Presenters.Data;
using Sapsan.Modules.Operational.Services.Contracts;
using Sapsan.Modules.Operational.Services.Data;
using Sapsan.Modules.ProjectScheduler.Services;
using Sapsan2.Core.Service.Contracts;

namespace Sapsan.Modules.Operational.Controls.Admin
{
	public partial class AdminOperationalControl : UserControl
	{
		private readonly IOperationalService _operationalService;
		private readonly IErrorCatcher _errorCatcher;
		private readonly ILogService _logService;
		private readonly IUserService _userService;
		private readonly IOperationalPresenter _operationalPresenter;
		private readonly WorkStructureSprService _workStructureSprService;

		public AdminOperationalControl(
			IOperationalService operationalService,
			IErrorCatcher errorCatcher,
			ILogService logService,
			IUserService userService,
			IOperationalPresenter operationalPresenter,
			WorkStructureSprService workStructureSprService)
		{
			_operationalService = operationalService;
			_errorCatcher = errorCatcher;
			_logService = logService;
			_userService = userService;
			_operationalPresenter = operationalPresenter;
			_workStructureSprService = workStructureSprService;

			Init();
		}

		public void Init()
		{
			InitializeComponent();

			dataGridControlAddBtn.Init(_errorCatcher, _logService, _userService, typeof(OperationalData),
				nameof(OperationalData.Id), contextMenuStrip);

			dataGridControlAddBtn.SetSimpleGridView();

			dataGridControlAddBtn.BtnAddClick += добавитьToolStripMenuItem_Click;
		}

		public void LoadData()
		{
			var sourceData = _operationalPresenter.GetData();

			dataGridControlAddBtn.populateDataSource(sourceData);
		}

		private void добавитьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_errorCatcher.Do(() =>
			{
				var d = new OperationalEditDialog(_operationalService, _workStructureSprService);

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
				var operational = (OperationalPresenterData)dataGridControlAddBtn.GetFocusedRow();

				var d = new OperationalEditDialog(_operationalService, _workStructureSprService);

				d.LoadData(operational.Id);

				if (d.ShowDialog() == DialogResult.OK)
				{
					LoadData();
				}
			});
		}

		private void историяToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_errorCatcher.Do(() =>
			{
				var operational = (OperationalPresenterData)dataGridControlAddBtn.GetFocusedRow();

				var d = new SUVPP.UI.Dialogs.Common.HistoryDialog(_logService, _userService);
				d.Init("История", operational.Id);
				d.ShowDialog();
			});
		}

		private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_errorCatcher.Do(() =>
			{
				if (MessageBox.Show("Удалить выбранную оперативку?", "Внимание", MessageBoxButtons.OKCancel) ==
					DialogResult.OK)
				{
					var operational = (OperationalPresenterData)dataGridControlAddBtn.GetFocusedRow();
					_operationalService.Delete(operational.Id);
					LoadData();
				}
			});
		}

		private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			var data = (OperationalPresenterData)dataGridControlAddBtn.GetFocusedRow();
			if (data == null)
			{
				e.Cancel = true;
				return;
			}

			удалитьToolStripMenuItem.Enabled = редактироватьToolStripMenuItem.Enabled = data.DtDelete.HasValue == false;
		}
	}
}