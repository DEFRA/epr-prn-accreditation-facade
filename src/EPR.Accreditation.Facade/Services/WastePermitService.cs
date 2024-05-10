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

        public async Task<bool?> GetHasPermitExemption(Guid id)
        {
            var accreditation = await _httpAccreditationService.GetAccreditation(id);

            if (accreditation.WastePermit == null)
            {
                return null;
            }

            return accreditation.WastePermit.WastePermitExemption;
        }

        public async Task UpdatePermitExemption(
            Guid id,
            PermitExemption permitExemption)
        {
            var accreditation = await _httpAccreditationService.GetAccreditation(id);

            if (accreditation.WastePermit == null)
            {
                accreditation.WastePermit = new WastePermit();
            }

            accreditation.WastePermit.WastePermitExemption = permitExemption.HasPermitExemption;

            await _httpAccreditationService.UpdateAccreditation(
                id,
                accreditation);
        }
    }
}
