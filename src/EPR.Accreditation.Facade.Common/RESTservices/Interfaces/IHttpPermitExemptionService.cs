using EPR.Accreditation.Facade.Common.Dtos;

namespace EPR.Accreditation.Facade.Common.RESTservices.Interfaces
{
    public interface IHttpPermitExemptionService
    {
        Task<PermitExemption> GetHasPermitExemption(Guid accreditationExternalId);
    }
}
