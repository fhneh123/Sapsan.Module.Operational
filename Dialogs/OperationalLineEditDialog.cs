using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Sapsan.Modules.Operational.Services.Contracts;
using Sapsan.Modules.Operational.Services.Data;
using Sapsan.Modules.ProjectScheduler.Services;
using Sapsan.Modules.ProjectScheduler.Services.Data;
using Sapsan2.Contracts.Enums;
using Sapsan2.Contracts.Exceptions;
using Sapsan2.Core.Contracts;
using Sapsan2.Core.Service.Contracts;
using SUVPP.UI.Dialogs.Common;

namespace Sapsan.Modules.Operational.Dialogs
{
	public partial class OperationalLineEditDialog : FormWithEscape
	{
		private IOperationalLineService _operationalLineService;
		private IOperationalService _operationalService;
		private PlanService _planService;
		private IErrorCatcher _errorCatcher;
		private IContractService _contractService;
		private WorkStructureService _workStructureservice;
		private IDesignBasicService _designBasicService;
		private ISubdivisionService _subdivisionService;
		private IIdentityService _identityService;
		private IOperationalTypeTaskService _operationalTypeTaskService;

		private Guid? _operationalLineId;

		public Guid? OperationalLineId
		{
			get { return _operationalLineId; }
		}

		public void Init(
			IOperationalLineService operationalLineService,
			IOperationalService operationalService,
			PlanService planService,
			IErrorCatcher errorCatcher,
			IContractService contractService,
			WorkStructureService workStructureservice,
			IDesignBasicService designBasicService,
			ISubdivisionService subdivisionService,
			IIdentityService identityService,
			IOperationalTypeTaskService operationalTypeTaskService)
		{
			_operationalLineService = operationalLineService;
			_operationalService = operationalService;
			_planService = planService;
			_errorCatcher = errorCatcher;
			_contractService = contractService;
			_workStructureservice = workStructureservice;
			_designBasicService = designBasicService;
			_subdivisionService = subdivisionService;
			_identityService = identityService;
			_operationalTypeTaskService = operationalTypeTaskService;

			InitializeComponent();

			comboBoxOperationals.DisplayMember = nameof(OperationalData.Name);
			comboBoxOperationals.ValueMember = nameof(OperationalData.Id);
			comboBoxOperationals.DataSource = _operationalService.GetList().ToList();

			planLookUpControl.Init(_planService);
			planLookUpControl.ValueChanged += comboBoxPlans_SelectedValueChanged;

			comboBoxGroup.DisplayMember = nameof(WorkStructureData.Name);
			comboBoxGroup.ValueMember = nameof(WorkStructureData.Id);

			comboBoxTaskType.DisplayMember = nameof(TaskTypeModel.Name);
			comboBoxTaskType.ValueMember = nameof(TaskTypeModel.Id);
			comboBoxTaskType.DataSource = _operationalTypeTaskService.GetList().ToList();

			contractLookUpEditControl.Init(_contractService);
			contractLookUpEditControl.ContractChanged += cbContract_SelectedIndexChanged;

			var subdivisionDatas = _subdivisionService.GetList().Where(x => x.Id == SubdivisionIds.PZGIPII
																			|| x.Id == SubdivisionIds.BRUII ||
																			x.Id == SubdivisionIds.RUIIPZS).ToArray();
			subdivisionLookUpEditControl.Init(_subdivisionService, subdivisionDatas);
			SetSelectedSubdivision();
		}

		public void LoadData(Guid? operationalLineId = null, Guid? operationalId = null)
		{
			_operationalLineId = operationalLineId;

			if (_operationalLineId.HasValue)
			{
				Text = "Редактирование строки оперативки";

				var operationalLineData = _operationalLineService.GetList().Single(x => x.Id == _operationalLineId);

				comboBoxOperationals.SelectedValue = operationalLineData.OperationalId;
				contractLookUpEditControl.SelectedValue = operationalLineData.ContractId;
				planLookUpControl.SelectedValue = operationalLineData.PlanId;
				comboBoxGroup.SelectedValue = operationalLineData.ComplianceGroupId;
				comboBoxTaskType.SelectedValue = operationalLineData.TypeTaskId;
				textBoxPriority.Text = operationalLineData.Priority;
				subdivisionLookUpEditControl.SelectedValue = operationalLineData.SubdivisionId;
			}
			else if (operationalId.HasValue)
			{
				comboBoxOperationals.SelectedValue = operationalId;
			}
		}

		private void buttonSaveCancelControl1_ButtonSaveClick(object sender, EventArgs e)
		{
			_errorCatcher.Do(() =>
			{
				if (comboBoxOperationals.SelectedValue == null)
				{
					throw new ClientException("Не указана оперативка.");
				}

				if (contractLookUpEditControl.SelectedValue == null)
				{
					throw new ClientException("Не указан проект.");
				}

				if (planLookUpControl.SelectedValue == null)
				{
					throw new ClientException("Не указан план-график.");
				}

				if (comboBoxGroup.SelectedValue == null)
				{
					throw new ClientException("Не указана группа.");
				}

				if (comboBoxTaskType.SelectedValue == null)
				{
					throw new ClientException("Не указан тип задания.");
				}

				if (subdivisionLookUpEditControl.SelectedValue == null)
				{
					throw new ClientException("Не указано подразделение.");
				}

				var operationalLineData = new OperationalLineData
				{
					ContractId = contractLookUpEditControl.SelectedValue.Value,
					OperationalId = (Guid)comboBoxOperationals.SelectedValue,
					PlanId = (Guid)planLookUpControl.SelectedValue,
					ComplianceGroupId = (Guid)comboBoxGroup.SelectedValue,
					TypeTaskId = (Guid)comboBoxTaskType.SelectedValue,
					Priority = textBoxPriority.Text,
					SubdivisionId = subdivisionLookUpEditControl.SelectedValue.Value
				};

				var data = _operationalLineService.GetList().FirstOrDefault(x =>
					x.OperationalId == operationalLineData.OperationalId
					&& x.ContractId == operationalLineData.ContractId
					&& x.PlanId == operationalLineData.PlanId
					&& x.TypeTaskId == operationalLineData.TypeTaskId
					&& x.DtDelete.HasValue == false
					&& x.SubdivisionId == operationalLineData.SubdivisionId
					&& x.ComplianceGroupId == operationalLineData.ComplianceGroupId);

				if (data != null && (_operationalLineId.HasValue == false || data.Id != _operationalLineId))
				{
					throw new ClientException("Оперативка с текущими данными уже существует!");
				}

				if (_operationalLineId.HasValue)
				{
					_operationalLineService.Update(_operationalLineId.Value, operationalLineData);
				}
				else
				{
					_operationalLineId = _operationalLineService.Add(operationalLineData);
				}

				DialogResult = DialogResult.OK;
			});
		}

		private void SetSelectedSubdivision()
		{
			var subdivisionId = _identityService.SubdivisionId;

			if (subdivisionId.HasValue == false)
			{
				return;
			}

			var parentSubdivisions = _subdivisionService.GetWithParents(subdivisionId.Value).Select(x => x.Id);

			if (parentSubdivisions.Contains(SubdivisionIds.BRUII))
			{
				subdivisionLookUpEditControl.SelectedValue = SubdivisionIds.BRUII;
			}
			else if (parentSubdivisions.Contains(SubdivisionIds.RUIIPZS))
			{
				subdivisionLookUpEditControl.SelectedValue = SubdivisionIds.RUIIPZS;
			}
			else
			{
				subdivisionLookUpEditControl.SelectedValue = SubdivisionIds.PZGIPII;
			}
		}

		private void buttonSaveCancelControl1_ButtonCancelClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		private void cbContract_SelectedIndexChanged(object sender, EventArgs e)
		{
			var contract = contractLookUpEditControl.SelectedValue;
			if (contract.HasValue)
			{
				planLookUpControl.LoadData(contract.Value);
			}
		}

		private void comboBoxPlans_SelectedValueChanged(object sender, EventArgs e)
		{
			var plan = planLookUpControl.SelectedValue;

			if (plan == null)
			{
				comboBoxGroup.Text = string.Empty;
				comboBoxGroup.DataSource = new List<WorkModel>();
				return;
			}

			comboBoxGroup.Text = string.Empty;
			comboBoxGroup.DataSource = _workStructureservice.GetList().Where(x => x.PlanId == (Guid)plan)
				.Select(x => new WorkModel
				{
					Id = x.Id,
					Name = x.Name + " " + (x.StageNumber.HasValue ? "Этап " + x.StageNumber : string.Empty)
				}).ToList();
			SetSelectedValueComboBoxGroup();
		}

		private void SetSelectedValueComboBoxGroup()
		{
			var plan = planLookUpControl.SelectedValue;
			var operational = comboBoxOperationals.SelectedValue;

			if (plan != null && operational != null)
			{
				var complianceGroupId = _operationalService.GetList().Single(x => x.Id == (Guid)operational)
					.ComplianceGroupId;

				var groupId = _workStructureservice.GetList()
					.FirstOrDefault(x => x.PlanId == (Guid)plan && x.WorkSprId == complianceGroupId)?.Id;
				if (groupId != null)
				{
					comboBoxGroup.SelectedValue = groupId;
				}
			}
		}

		private class TaskTypeModel
		{
			public int Id { get; set; }
			public string Name { get; set; }
		}


		private class WorkModel
		{
			public Guid Id { get; set; }
			public string Name { get; set; }
		}

		private void comboBoxOperationals_SelectedValueChanged(object sender, EventArgs e)
		{
			SetSelectedValueComboBoxGroup();
		}
	}
}