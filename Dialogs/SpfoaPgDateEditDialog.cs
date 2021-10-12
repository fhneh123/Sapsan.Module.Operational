using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Sapsan.Modules.Operational.Enums;
using Sapsan.Modules.Operational.Services.Contracts;
using Sapsan.Modules.Operational.Services.Data;
using Sapsan2.Contracts.Exceptions;
using Sapsan2.Contracts.Extensions;
using Sapsan2.Core.Service.Contracts;
using SUVPP.UI.Dialogs.Common;

namespace Sapsan.Modules.Operational.Dialogs
{
	public partial class SpfoaPgDateEditDialog : FormWithEscape
	{
		private IOperationalService _operationalService;
		private IOperationalWorkService _operationalWorkService;
		private IOperationalEntityStatusService _operationalEntityStatusService;
		private IOperationalSpfoaEditorService _operationalSpfoaEditorService;
		private IErrorCatcher _errorCatcher;

		private List<DateTypeModel> _dateTypeModels;
		private Guid? _operationalSpfoaEditorId;

		public void Init(
			IOperationalService operationalService,
			IOperationalWorkService operationalWorkService,
			IOperationalEntityStatusService operationalEntityStatusService,
			IOperationalSpfoaEditorService operationalSpfoaEditorService,
			IErrorCatcher errorCatcher)
		{
			_operationalService = operationalService;
			_operationalWorkService = operationalWorkService;
			_operationalEntityStatusService = operationalEntityStatusService;
			_operationalSpfoaEditorService = operationalSpfoaEditorService;
			_errorCatcher = errorCatcher;

			InitializeComponent();

			comboBoxOperationals.DisplayMember = nameof(OperationalData.Name);
			comboBoxOperationals.ValueMember = nameof(OperationalData.Id);

			comboBoxWorks.DisplayMember = nameof(OperationalWorkData.Name);
			comboBoxWorks.ValueMember = nameof(OperationalWorkData.Id);

			comboBoxStatusOperational.Properties.DisplayMember = nameof(OperationalEntityStatusData.Name);
			comboBoxStatusOperational.Properties.ValueMember = nameof(OperationalEntityStatusData.Id);

			comboBoxDateType.DisplayMember = nameof(DateTypeModel.Name);
			comboBoxDateType.ValueMember = nameof(DateTypeModel.Id);


			comboBoxStatusII.DisplayMember = nameof(OperationalData.Name);
			comboBoxStatusII.ValueMember = nameof(OperationalData.Id);

			comboBoxOperationals.DataSource = _operationalService.GetList().ToList();

			comboBoxDateType.DataSource = GetDateTypeList();

			comboBoxStatusII.DataSource = _operationalEntityStatusService.GetList()
				.Where(x => x.EntityId == OperationalStatusEntityIdEnum.IiStatusEntityId).ToList();
		}

		public void LoadData(Guid? operationalSpfoaEditorId = null)
		{
			_operationalSpfoaEditorId = operationalSpfoaEditorId;

			if (_operationalSpfoaEditorId.HasValue)
			{
				Text = "Редактирование связи с план-графиком";
				var operationalSpfoaEditorData = _operationalSpfoaEditorService.GetList()
					.Single(x => x.Id == _operationalSpfoaEditorId);

				comboBoxOperationals.SelectedValue = _operationalWorkService.GetList()
					.Single(x => x.Id == operationalSpfoaEditorData.OperatioalWorkId).OperationalId;
				comboBoxWorks.SelectedValue = operationalSpfoaEditorData.OperatioalWorkId;
				if (operationalSpfoaEditorData.OperationalStatusId.HasValue)
				{
					comboBoxStatusOperational.EditValue = operationalSpfoaEditorData.OperationalStatusId;
				}

				comboBoxDateType.SelectedValue = operationalSpfoaEditorData.OperationalDateTypeId;
				comboBoxStatusII.SelectedValue = operationalSpfoaEditorData.SpfoaStatusId;
				orderNumUpd.Value = operationalSpfoaEditorData.OrderNum;
			}
		}

		private List<DateTypeModel> GetDateTypeList()
		{
			var result = new List<DateTypeModel>();
			foreach (OperationalSpfoaDateTypeEnum value in Enum.GetValues(typeof(OperationalSpfoaDateTypeEnum)))
			{
				result.Add(new DateTypeModel
				{
					Id = (int)value,
					Name = value.GetDisplayName()
				});
			}

			return result;
		}

		private void buttonSaveCancelControl1_ButtonSaveClick(object sender, EventArgs e)
		{
			_errorCatcher.Do(() =>
			{
				if (comboBoxOperationals.SelectedValue == null)
				{
					throw new ClientException("Не указана оперативка.");
				}

				if (comboBoxWorks.SelectedValue == null)
				{
					throw new ClientException("Не указана работа.");
				}

				if (comboBoxDateType.SelectedValue == null)
				{
					throw new ClientException("Не указан тип даты.");
				}

				if (comboBoxStatusII.SelectedValue == null)
				{
					throw new ClientException("Не указан статус в СПФОА ИИ.");
				}

				var operationalSpfoaEditorData = new OperationalSpfoaEditorData
				{
					OperatioalWorkId = (Guid)comboBoxWorks.SelectedValue,
					OperationalStatusId = (Guid?)comboBoxStatusOperational.EditValue,
					OperationalDateTypeId = (int)comboBoxDateType.SelectedValue,
					SpfoaStatusId = (Guid)comboBoxStatusII.SelectedValue,
					OrderNum = (int)orderNumUpd.Value
				};

				if (_operationalSpfoaEditorId.HasValue)
				{
					_operationalSpfoaEditorService.Update(_operationalSpfoaEditorId.Value, operationalSpfoaEditorData);
				}
				else
				{
					_operationalSpfoaEditorService.Add(operationalSpfoaEditorData);
				}

				DialogResult = DialogResult.OK;
			});
		}

		private void buttonSaveCancelControl1_ButtonCancelClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		private void comboBoxOperationals_SelectedValueChanged(object sender, EventArgs e)
		{
			var operational = comboBoxOperationals.SelectedValue;

			if (operational == null)
			{
				comboBoxWorks.Text = string.Empty;
				comboBoxWorks.DataSource = new List<OperationalWorkData>();
				return;
			}

			comboBoxWorks.Text = string.Empty;
			comboBoxWorks.DataSource = _operationalWorkService.GetList().Where(x =>
					x.OperationalId == (Guid)operational && x.DtDelete.HasValue == false)
				.OrderBy(x => x.OrderNum).ToList();
		}

		private void comboBoxWorks_SelectedValueChanged(object sender, EventArgs e)
		{
			var work = comboBoxWorks.SelectedValue;

			if (work == null)
			{
				comboBoxStatusOperational.Text = string.Empty;
				comboBoxStatusOperational.Properties.DataSource = new List<OperationalWorkData>();

				comboBoxDateType.Text = string.Empty;
				comboBoxDateType.DataSource = new List<DateTypeModel>();
				comboBoxStatusOperational.EditValue = null;
				return;
			}

			comboBoxStatusOperational.Text = string.Empty;
			comboBoxStatusOperational.Properties.DataSource = _operationalEntityStatusService.GetList().Where(x =>
				x.OperationalWorkId == (Guid)work
				&& x.EntityId == nameof(OperationalWorkLineData.Status)).OrderBy(x => x.OrderNum).ToList();
			comboBoxStatusOperational.EditValue = null;
		}

		private class DateTypeModel
		{
			public int Id { get; set; }
			public string Name { get; set; }
		}
	}
}