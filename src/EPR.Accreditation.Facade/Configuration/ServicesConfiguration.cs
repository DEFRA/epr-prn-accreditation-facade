namespace EPR.Accreditation.Facade.Configuration
{
    public class ServicesConfiguration
    {
        public static string SectionName => "Services";

        public Service AccreditationAPI { get; set; }
    }

    public class Service
    {
        public string Url { get; set; }
    }
}
