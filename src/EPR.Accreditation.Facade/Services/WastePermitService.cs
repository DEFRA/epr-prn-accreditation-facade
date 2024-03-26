using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.Dtos.Portal;
using EPR.Accreditation.Facade.Common.RESTservices.Interfaces;
using EPR.Accreditation.Facade.Services.Interfaces;

namespace EPR.Accreditation.Facade.Services
{
    public class WastePermitService : IWastePermitService
    {

        protected readonly IHttpAccreditationService _httpAccreditationService;

        public WastePermitService(IHttpAccreditationService httpAccreditationService)
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

        public async Task UpdatePermitExemption(
            Guid accreditationExternalId,
            PermitExemption permitExemption)
        {
            var accreditation = new Common.Dtos.Accreditation
            {
                WastePermit = new WastePermit
                {
                    WastePermitExemption = permitExemption.HasPermitExemption
                }
            };

            await _httpAccreditationService.UpdateAccreditation(
                accreditationExternalId,
                accreditation
                );
        }

    }
}
