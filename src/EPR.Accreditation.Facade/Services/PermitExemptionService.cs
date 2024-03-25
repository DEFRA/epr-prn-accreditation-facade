using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
using EPR.Accreditation.Facade.Services.Interfaces;

namespace EPR.Accreditation.Facade.Services
{
    public class PermitExemptionService : IPermitExemptionService
    {

        protected readonly IHttpAccreditationService _httpAccreditationService;

        public PermitExemptionService(IHttpAccreditationService httpAccreditationService)
        {
            _httpAccreditationService = httpAccreditationService ?? throw new ArgumentNullException(nameof(httpAccreditationService));
        }

        public async Task<bool?> GetHasPermitExemption(Guid accreditationExternalId)
        {
            var accreditation = await _httpAccreditationService.GetAccreditation(accreditationExternalId);

            if (accreditation.WastePermit == null)
                return null;

            return accreditation.WastePermit.WastePermitExemption;
        }
    }
}
