using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
using EPR.Accreditation.Facade.Services.Interfaces;

namespace EPR.Accreditation.Facade.Services
{
    public class PermitExemptionService : IPermitExemptionService
    {

        protected readonly IHttpPermitExemptionService _httpPermitExemptionService;

        public PermitExemptionService(IHttpPermitExemptionService httpPermitExemptionService)
        {
            _httpPermitExemptionService = httpPermitExemptionService ?? throw new ArgumentNullException(nameof(httpPermitExemptionService));
        }

        public async Task<PermitExemption> GetHasPermitExemption(Guid accreditationExternalId)
        {
            return await _httpPermitExemptionService.GetHasPermitExemption(accreditationExternalId);
        }
    }
}
