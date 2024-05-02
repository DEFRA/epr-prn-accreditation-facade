namespace EPR.Accreditation.Facade.Common.Dtos
{
    public class CheckAnswersDto
    {
        public Guid Id { get; set; }

        public bool Completed { get; set; }

        public Address SiteAddress { get; set; }

        public List<CheckAnswersSectionDto> Sections { get; set; }
    }
}
