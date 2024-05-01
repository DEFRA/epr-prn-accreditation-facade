namespace EPR.Accreditation.Facade.Common.Dtos
{
    public class Site
    {
        public Guid ExternalId { get; set; }

        public Address Address { get; set; }

        public IEnumerable<string> ExemptionReferences { get; set; }

        public IEnumerable<SiteAuthority> SiteAuthorties { get; set; }
    }
}
