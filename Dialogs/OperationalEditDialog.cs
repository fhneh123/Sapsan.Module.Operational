using System;
using System.Linq;
using System.Windows.Forms;
using Sapsan.Modules.Operational.Services.Contracts;
using Sapsan.Modules.Operational.Services.Data;
using Sapsan.Modules.ProjectScheduler.Services;
using Sapsan2.Contracts.Exceptions;
using SUVPP.UI.Dialogs.Common;

namespace Sapsan.Modules.Operational.Dialogs
{
	public partial class OperationalEditDialog : FormWithEscape
	{
		private readonly IOperationalService _operationalService;
		private readonly WorkStructureSprService _workSprService;

		private Guid? _operationalId;

		public OperationalEditDialog(
			IOperationalService operationalService,
			WorkStructureSprService workSprService)
		{
			_operationalService = operationalService;
			_workSprService = workSprService;

			InitializeComponent();

			workStructureSprLookUpControl.Init(_workSprService);
		}

		public void LoadData(Guid? operationalId = null)
		{
			_operationalId = operationalId;

			workStructureSprLookUpControl.LoadData();

			if (_operationalId.HasValue)
			{
				Text = "Редактирование оперативки";

				var operationalData = _operationalService.GetList().FirstOrDefault(x => x.Id == _operationalId);

				if (operationalData != null)
				{
					tbName.Text = operationalData.Name;
					orderNumUpd.Value = operationalData.OrderNum;
					workStructureSprLookUpControl.SelectedValue = operationalData.ComplianceGroupId;
				}
			}
		}

		private void buttonSaveCancelControl1_ButtonSaveClick(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(tbName.Text))
			{
				throw new ClientException("Укажите наименование");
			}

			if (workStructureSprLookUpControl.SelectedValue == null)
			{
				throw new ClientException("Укажите соответствующую группу");
			}

			var operationalData = new OperationalData
			{
				Name = tbName.Text,
				OrderNum = (int)orderNumUpd.Value,
				ComplianceGroupId = workStructureSprLookUpControl.SelectedValue.Value
			};

			if (_operationalId.HasValue)
			{
				_operationalService.Update(_operationalId.Value, operationalData);
			}
			else
			{
				_operationalService.Add(operationalData);
			}

			DialogResult = DialogResult.OK;
		}

		private void buttonSaveCancelControl1_ButtonCancelClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}
	}
}