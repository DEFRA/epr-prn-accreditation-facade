using EPR.Accreditation.Facade.Common.Dtos;

namespace EPR.Accreditation.Facade.Common.RESTservices.Interfaces
{
    public interface IHttpOverseasSiteService
    {
        Task<OverseasReprocessingSite> GetOverseasReprocessingSite(
            Guid accreditationExternalId,
            Guid overseasSiteExternalId);

        Task UpdateOverseasReprocessingSite(
            Guid accreditationExternalId,
            OverseasReprocessingSite overseasReprocessingSite);
    }
}
