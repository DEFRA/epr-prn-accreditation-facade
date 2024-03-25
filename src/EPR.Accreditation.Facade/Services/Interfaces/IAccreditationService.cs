using System;

namespace EPR.Accreditation.Facade.Services.Interfaces
{
    public interface IAccreditationService
    {
        Task<Common.Enums.OperatorType> GetOperatorType(Guid accreditationExternalId);
        Task UpdateOperatorType(
            Guid accreditationExternalId, 
            Common.Enums.OperatorType operatorTypeId);

        public Task<string> GetWasteSource(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId);
        public Task UpdateWasteSource(
            Guid accreditationExternalId,
            Guid siteExternalId,
            Guid materialExternalId,
            string wasteSource);
    }
}
