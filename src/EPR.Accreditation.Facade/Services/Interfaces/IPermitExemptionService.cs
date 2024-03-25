using EPR.Accreditation.Facade.Common.Dtos;

namespace EPR.Accreditation.Facade.Services.Interfaces
{
    public interface IPermitExemptionService
    {
        public Task<PermitExemption> GetHasPermitExemption(Guid accreditationExternalId);
    }
}
