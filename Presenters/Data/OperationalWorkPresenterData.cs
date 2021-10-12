using System.ComponentModel.DataAnnotations;

namespace Sapsan.Modules.Operational.Presenters.Data
{
	public class OperationalWorkPresenterData
	{
		[Display(Name = "Соответствует работе", Order = 6)]
		public bool СomplianceWorkVisible { get; set; }
		[Display(Name = "Соответствует работе всего ПГ", Order = 6)]
		public bool ComplianceWorkAllWorkVisible { get; set; }

		[Display(Name = "Плановая дата начала", Order = 7)]
		public bool DtPlanStartVisible { get; set; }

		[Display(Name = "Плановая дата окончания", Order = 8)]
		public bool DtPlanEndVisible { get; set; }

		[Display(Name = "Прогнозная дата начала", Order = 9)]
		public bool DtForecastStartVisible { get; set; }

		[Display(Name = "Прогнозная дата окончания", Order = 10)]
		public bool DtForecastEndVisible { get; set; }

		[Display(Name = "Фактическая дата начала", Order = 11)]
		public bool DtFactStartVisible { get; set; }

		[Display(Name = "Фактическая дата окончания", Order = 12)]
		public bool DtFactEndVisible { get; set; }

		[Display(Name = "Плановая смета. Основная", Order = 13)]
		public bool PlanSmetaMainVisible { get; set; }

		[Display(Name = "Плановая смета. Закрепление", Order = 15)]
		public bool PlanSmetaFixedVisible { get; set; }

		[Display(Name = "Плановая смета. 3D сканирование", Order = 16)]
		public bool PlanSmeta3DScanVisible { get; set; }

		[Display(Name = "Плановая смета. Георадарное сканирование", Order = 17)]
		public bool PlanSmetaGeoradScanVisible { get; set; }

		[Display(Name = "Плановая смета. Колич.скважин", Order = 17)]
		public bool PlanSmetaCountSkvazhinVisible { get; set; }

		[Display(Name = "Плановая смета. П.м.", Order = 17)]
		public bool PlanSmetaPmVisible { get; set; }

		[Display(Name = "Плановая смета. Количество монолитов", Order = 17)]
		public bool PlanSmetaCountMonolitVisible { get; set; }

		[Display(Name = "Плановая смета. Ответственный", Order = 18)]
		public bool PlanSmetaResponsibleUserVisible { get; set; }

		[Display(Name = "Плановая смета. Фактическая  дата начала", Order = 19)]
		public bool PlanSmetaDtFactStartVisible { get; set; }

		[Display(Name = "Плановая смета. Фактическая дата окончания", Order = 20)]
		public bool PlanSmetaDtFactEndVisible { get; set; }

		[Display(Name = "Исполнительная смета. Основная", Order = 21)]
		public bool ExecuteSmetaMainVisible { get; set; }

		[Display(Name = "Исполнительная смета. Закрепление", Order = 22)]
		public bool ExecuteSmetaFixedVisible { get; set; }

		[Display(Name = "Исполнительная смета. 3D сканирование", Order = 23)]
		public bool ExecuteSmeta3DScanVisible { get; set; }

		[Display(Name = "Исполнительная смета. Георадарное сканирование", Order = 24)]
		public bool ExecuteSmetaGeoradScanVisible { get; set; }

		[Display(Name = "Исполнительная  смета. Колич.скважин", Order = 24)]
		public bool ExecuteSmetaCountSkvazhinVisible { get; set; }

		[Display(Name = "Исполнительная  смета. П.м.", Order = 24)]
		public bool ExecuteSmetaPmVisible { get; set; }

		[Display(Name = "Исполнительная  смета. Количество монолитов", Order = 24)]
		public bool ExecuteSmetaCountMonolitVisible { get; set; }

		[Display(Name = "Исполнительная смета. Ответственный", Order = 25)]
		public bool ExecuteSmetaResponsibleUserVisible { get; set; }

		[Display(Name = "Исполнительная смета. Фактическая дата начала", Order = 26)]
		public bool ExecuteSmetaDtFactStartVisible { get; set; }

		[Display(Name = "Исполнительная смета. Фактическая дата окончания", Order = 27)]
		public bool ExecuteSmetaDtFactEndVisible { get; set; }

		[Display(Name = "Исполнительная смета. Дополнительная", Order = 28)]
		public bool ExecuteSmetaAdditionalVisible { get; set; }

		[Display(Name = "Акт сдачи полосовой съемки", Order = 29)]
		public bool DtActPassingStripSurveyVisible { get; set; }

		[Display(Name = "Акт сдачи реперов", Order = 30)]
		public bool DtActPassingRapperVisible { get; set; }

		[Display(Name = "Топопланы", Order = 31)]
		public bool DtTopoplanVisible { get; set; }

		[Display(Name = "Загрузка в параллел. проект", Order = 32)]
		public bool DtLoadInParallelProjectionVisible { get; set; }

		[Display(Name = "Профили и ведомости", Order = 33)]
		public bool DtProfilAndStatementVisible { get; set; }

		[Display(Name = "Передача профилей ИГИ и ИГМИ", Order = 34)]
		public bool DtTransferProfileIgiAndIgmiVisible { get; set; }

		[Display(Name = "Выгрузка отчета в Сапсан2020", Order = 35)]
		public bool DtUnloadingReportFromSapsanVisible { get; set; }

		[Display(Name = "Нормоконтроль", Order = 36)]
		public bool DtNormocontrolVisible { get; set; }

		[Display(Name = "Наличие замечания", Order = 37)]
		public bool HasRemarkVisible { get; set; }

		[Display(Name = "Окончание камеральных ИГДИ", Order = 38)]
		public bool DtKameralIgdiEndVisible { get; set; }

		[Display(Name = "Прогноз.срок окончания ИГДИ", Order = 39)]
		public bool DtForecastEndIgdiVisible { get; set; }

		[Display(Name = "Число водотоков", Order = 40)]
		public bool VodotokCountVisible { get; set; }

		[Display(Name = "Количество расчетов", Order = 41)]
		public bool CalculationsCountVisible { get; set; }

		[Display(Name = "Дата подписания", Order = 44)]
		public bool DtSigningVisible { get; set; }

		[Display(Name = "ПВО, всего шт", Order = 45)]
		public bool PvoCountVisible { get; set; }

		[Display(Name = "Изыскания площадок, всего га", Order = 46)]
		public bool SurvayFieldVisible { get; set; }

		[Display(Name = "Изыскания трасс, всего км", Order = 47)]
		public bool SurvayTrackVisible { get; set; }

		[Display(Name = "ТЗ", Order = 48)] public bool TZVisible { get; set; }

		[Display(Name = "Дата окончания", Order = 49)]
		public bool DtEndVisible { get; set; }

		[Display(Name = "ТФО", Order = 50)] public bool TfoVisible { get; set; }

		[Display(Name = "Плановая дата составления", Order = 53)]
		public bool DtPlanCompilationVisible { get; set; }

		[Display(Name = "Дата отправки на согласование", Order = 54)]
		public bool DtSendCompilationVisible { get; set; }

		[Display(Name = "Дата отправки", Order = 55)]
		public bool DtSendVisible { get; set; }

		[Display(Name = "ФОН", Order = 56)] public bool FonVisible { get; set; }

		[Display(Name = "РХХ", Order = 57)] public bool PHHVisible { get; set; }

		[Display(Name = "Предписание ГО дата выдачи", Order = 58)]
		public bool DtIssuePrescriptionGOVisible { get; set; }

		[Display(Name = "ПГР (ИГИ)", Order = 59)]
		public bool PGRVisible { get; set; }

		[Display(Name = "ИГС (ИГИ)", Order = 60)]
		public bool IGSVisible { get; set; }

		[Display(Name = "ПЧВ ОА", Order = 61)] public bool PchvOaVisible { get; set; }

		[Display(Name = "ПЧВ БАК", Order = 62)]
		public bool PchvBakVisible { get; set; }

		[Display(Name = "ПЧВ ПРЗ", Order = 63)]
		public bool PchvPrzVisible { get; set; }

		[Display(Name = "АХ Ш", Order = 64)] public bool AhSHVisible { get; set; }

		[Display(Name = "АХ ПР", Order = 65)] public bool AhPrVisible { get; set; }

		[Display(Name = "ПЧВ ЗСО", Order = 66)]
		public bool PchvZsoVisible { get; set; }

		[Display(Name = "ПОВ. ВОД", Order = 67)]
		public bool PovVodVisible { get; set; }

		[Display(Name = "ПОДЗ.ВОД ИПВС", Order = 68)]
		public bool PodzVodIpvsVisible { get; set; }

		[Display(Name = "ПОДЗ.ВОД ИГС", Order = 69)]
		public bool PodzVodIgsVisible { get; set; }

		[Display(Name = "ДОН", Order = 70)] public bool DonVisible { get; set; }

		[Display(Name = "АВ", Order = 71)] public bool AvVisible { get; set; }

		[Display(Name = "ГраС", Order = 72)] public bool GraSVisible { get; set; }

		[Display(Name = "РАД", Order = 73)] public bool RadVisible { get; set; }

		[Display(Name = "ЕРН", Order = 74)] public bool ErnVisible { get; set; }

		[Display(Name = "ФФ", Order = 75)] public bool FfVisible { get; set; }

		[Display(Name = "Протокол радиации", Order = 76)]
		public bool ProtocolRadiationVisible { get; set; }

		[Display(Name = "ИГДИ.Прогнозная дата окончания камеральных работ ", Order = 77)]
		public bool DtForecastEndKameralIgdiVisible { get; set; }

		[Display(Name = "ИГИ.Прогнозная дата окончания камеральных работ", Order = 78)]
		public bool DtForecastKameralIgiVisible { get; set; }

		[Display(Name = "ИГМИ.Прогнозная дата окончания камеральных работ", Order = 79)]
		public bool DtForecastCameralIgmiVisible { get; set; }

		[Display(Name = "Наличие протоколов полевого обследования", Order = 80)]
		public bool HasProtocolFieldSurvayVisible { get; set; }

		[Display(Name = "Наличие графической части", Order = 81)]
		public bool HasGraficPartVisible { get; set; }

		[Display(Name = "Ветстанция", Order = 81)]
		public bool VetstanciaVisible { get; set; }

		[Display(Name = "Районы", Order = 81)]
		public bool DistrictsVisible { get; set; }

		[Display(Name = "Главный специалист", Order = 82)]
		public bool MainSpecialistVisible { get; set; }

		[Display(Name = "Ответственный", Order = 83)]
		public bool ResponsibleUserVisible { get; set; }

		[Display(Name = "Ответственный 2", Order = 84)]
		public bool ResponsibleUser2Visible { get; set; }

		[Display(Name = "Комментарий", Order = 85)]
		public bool CommentVisible { get; set; }

		[Display(Name = "Причина", Order = 86)]
		public bool ReasonVisible { get; set; }

		[Display(Name = "Статус", Order = 87)] 
		public bool StatusVisible { get; set; }
	}
}