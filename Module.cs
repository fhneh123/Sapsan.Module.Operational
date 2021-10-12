using System;
using System.Collections.Generic;
using System.Linq;
using Sapsan.Modules.Operational.Enums;
using Sapsan2.Contracts.Contracts;
using Sapsan2.Contracts.Contracts.UI;
using Sapsan2.Contracts.Exceptions;
using Sapsan2.Core.Contracts;
using Sapsan2.Core.Service.Contracts;

namespace Sapsan.Modules.Operational
{
	public class Module : IModule
	{
		private readonly ISubdivisionService _subdivisionService;
		private readonly ITransferTaskEventHolder _transferTaskHolder;
		private readonly ISettingService _settingService;
		private readonly ITransferTaskService _transferTaskService;
		private readonly IContractService _contractService;

		public static Guid ModuleId => Guid.Parse("{de056381-83e9-41dc-a22b-c8eb10befaf1}");

		public Guid Id => ModuleId;

		public static string ModuleName => "Оперативки";

		public string Name
		{
			get { return ModuleName; }
		}

		public Module(ISubdivisionService subdivisionService,
			ITransferTaskEventHolder transferTaskHolder,
			ISettingService settingService,
			ITransferTaskService transferTaskService,
			IContractService contractService)
		{
			_subdivisionService = subdivisionService;
			_transferTaskHolder = transferTaskHolder;
			_settingService = settingService;
			_transferTaskService = transferTaskService;
			_contractService = contractService;

			_transferTaskHolder.TransferTaskBeforeCreate += _transferTaskHolder_TransferTaskBeforeCreate;
		}

		public Dictionary<Guid, string> GetRoles()
		{
			var roles = new Dictionary<Guid, string>();
			roles.Add(OperationalRoleList.Добавление_редактирование, "Доступ к основным кнопка модуля оперативок");
			return roles;
		}

		public Dictionary<string, string> GetSettings()
		{
			var settings = new Dictionary<string, string>();
			settings.Add(SettingList.Запрет_создания_заданий_в_проектах_без_ведущего_отдела,
				"Запрет создания заданий в проектах без ведущего отдела. 0 - нет, 1 - да");
			return settings;
		}

		private void _transferTaskHolder_TransferTaskBeforeCreate(Guid contractId)
		{
			var requiringMainSubdivision =
				_settingService.GetValue(SettingList.Запрет_создания_заданий_в_проектах_без_ведущего_отдела);

			if (string.IsNullOrEmpty(requiringMainSubdivision) ||
				int.TryParse(requiringMainSubdivision, out var requiringMainSubdivisionValue) == false ||
				requiringMainSubdivisionValue != 1)
			{
				return;
			}

			var mainSubdivisionId = _contractService.Get(contractId).MainSubdivisionId;

			if ((mainSubdivisionId.HasValue == false || _subdivisionService.GetListWithDeleted()
					.Where(x => x.Id == mainSubdivisionId.Value && x.DtDeleted.HasValue).Any()) &&
				_transferTaskService.GetAllActive().Where(x => x.ContractId == contractId).Any() == false)
			{
				throw new ClientException(
					"Необходимо указать ведущий отдел в свойствах проекта. Создание задания приостановлено");
			}
		}
	}

	public class SettingList
	{
		public static readonly string Запрет_создания_заданий_в_проектах_без_ведущего_отдела =
			"RequiringMainSubdivisionForCreateTransferTask";
	}
}