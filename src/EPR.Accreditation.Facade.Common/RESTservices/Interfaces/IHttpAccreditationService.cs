﻿namespace EPR.Accreditation.Facade.Common.RESTservices.Interfaces
{
    public interface IHttpAccreditationService
    {
        Task<DTO.AccreditationMaterial> GetAccreditationMaterial(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId);

        Task UpdateAccreditationMaterial(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId,
            DTO.AccreditationMaterial accreditationMaterial);

        Task<DTO.Accreditation> GetAccreditation(
            Guid accreditationExternalId);

        Task UpdateAccreditation(
            Guid accreditationExternalId,
            DTO.Accreditation accreditation);
    }
}
