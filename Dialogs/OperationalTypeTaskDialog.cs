using System;
using System.Linq;
using System.Windows.Forms;
using Sapsan.Modules.Operational.Services.Contracts;
using Sapsan.Modules.Operational.Services.Data;
using Sapsan2.Contracts.Exceptions;

namespace Sapsan.Modules.Operational.Dialogs
{
	public partial class OperationalTypeTaskDialog : Form
	{
		private readonly IOperationalTypeTaskService _operationalTypeTaskService;

		private Guid? _typeTaskId;

		public OperationalTypeTaskDialog(
			IOperationalTypeTaskService operationalTypeTaskService)
		{
			_operationalTypeTaskService = operationalTypeTaskService;

			InitializeComponent();
		}

		public void LoadData(Guid? typeTaskId = null)
		{
			_typeTaskId = typeTaskId;

			if (_typeTaskId.HasValue)
			{
				Text = "Редактирование типа задания";

				var typeTaskData = _operationalTypeTaskService.GetList().FirstOrDefault(x => x.Id == _typeTaskId);

				if (typeTaskData != null)
				{
					tbName.Text = typeTaskData.Name;
					orderNumUpd.Value = typeTaskData.OrderNum;
				}
			}
		}

		private void buttonSaveCancelControl_ButtonSaveClick(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(tbName.Text))
			{
				throw new ClientException("Укажите наименование");
			}

			var typeTaskData = new OperationalTypeTaskData
			{
				Name = tbName.Text,
				OrderNum = (int)orderNumUpd.Value
			};

			if (_typeTaskId.HasValue)
			{
				_operationalTypeTaskService.Update(_typeTaskId.Value, typeTaskData);
			}
			else
			{
				_operationalTypeTaskService.Add(typeTaskData);
			}

			DialogResult = DialogResult.OK;
		}

		private void buttonSaveCancelControl_ButtonCancelClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}
	}
}