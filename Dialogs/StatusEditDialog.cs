using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Sapsan.Modules.Operational.Services.Contracts;
using Sapsan.Modules.Operational.Services.Data;
using Sapsan2.Contracts.Exceptions;
using SUVPP.UI.Dialogs.Common;

namespace Sapsan.Modules.Operational.Dialogs
{
	public partial class StatusEditDialog : FormWithEscape
	{
		private readonly IOperationalEntityStatusService _operationalWorkStatusService;

		private Guid? _statusId;
		private Guid? _operationalWorkId;
		private string _entityId;

		public StatusEditDialog(
			IOperationalEntityStatusService operationalWorkStatusService)
		{
			_operationalWorkStatusService = operationalWorkStatusService;

			InitializeComponent();
		}

		public void LoadData(Guid? statusId, Guid? operationalWorkId = null, string entityId = null)
		{
			_statusId = statusId;
			_operationalWorkId = operationalWorkId;
			_entityId = entityId;

			if (_statusId.HasValue)
			{
				Text = "Редактирование статуса";

				var statusData = _operationalWorkStatusService.GetListWithDeleted()
					.FirstOrDefault(x => x.Id == _statusId);

				if (statusData != null)
				{
					tbName.Text = statusData.Name;
					orderNumUpd.Value = statusData.OrderNum;
					panelColor.BackColor = string.IsNullOrWhiteSpace(statusData.Color)
						? Color.White
						: ColorTranslator.FromHtml(statusData.Color);
				}
			}
		}

		private void buttonSaveCancelControl1_ButtonSaveClick(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(tbName.Text))
			{
				throw new ClientException("Укажите наименование");
			}

			var statusData = new OperationalEntityStatusData
			{
				Name = tbName.Text,
				OrderNum = (int)orderNumUpd.Value,
				Color = ColorTranslator.ToHtml(panelColor.BackColor),
			};

			if (_statusId.HasValue)
			{
				_operationalWorkStatusService.Update(_statusId.Value, statusData);
			}
			else
			{
				statusData.OperationalWorkId = _operationalWorkId;
				statusData.EntityId = _entityId;

				_operationalWorkStatusService.Add(statusData);
			}

			DialogResult = DialogResult.OK;
		}

		private void buttonSaveCancelControl1_ButtonCancelClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		private void btnSelectColor_Click(object sender, EventArgs e)
		{
			var d = new ColorDialog();
			if (d.ShowDialog() == DialogResult.OK)
			{
				panelColor.BackColor = d.Color;
			}
		}
	}
}