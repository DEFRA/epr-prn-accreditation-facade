﻿using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.Enums;

namespace EPR.Accreditation.Facade.Services.Interfaces
{
    public interface IAccreditationService
    {
        Task<OperatorType> GetOperatorType(Guid accreditationExternalId);
        Task<Guid> CreateAccreditation(Common.Dtos.Accreditation accreditation);

        Task<string> GetWasteSource(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId);

        Task UpdateWasteSource(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId,
            string wasteSource);

        Task<string> GetWasteMaterialName(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId,
            Language language);
    }
}
