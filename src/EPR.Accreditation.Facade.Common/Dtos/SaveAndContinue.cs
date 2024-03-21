namespace EPR.Accreditation.Facade.Common.Dtos
{
    public class SaveAndContinue
    {
        public int AccreditationId { get; set; }

        public string Area { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public string Parameters { get; set; }
    }
}
