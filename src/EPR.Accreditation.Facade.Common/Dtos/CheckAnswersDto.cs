namespace EPR.Accreditation.Facade.Common.Dtos
{
	public class CheckAnswersDto
	{
		public Guid Id { get; set; }

		public bool Completed { get; set; }

		public string SiteAddress { get; set; }

		public List<CheckAnswersRowDto> SectionRows { get; set; } = new();

		public Dictionary<string, string> QueryStringRouteData { get; set; } = new();
	}
}
