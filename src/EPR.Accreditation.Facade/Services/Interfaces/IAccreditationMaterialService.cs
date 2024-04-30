using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.Dtos.Portal;

namespace EPR.Accreditation.Facade.Services.Interfaces
{
    public interface IAccreditationMaterialService
    {
        /// <summary>
        /// Gets the waste description codes for an application and the material
        /// </summary>
        /// <param name="id">The accreditation id</param>
        /// <param name="materialId">The material id</param>
        /// <returns>List of strings that represent the waste description codes</returns>
        Task<IEnumerable<string>> GetWasteDescriptionCodes(
            Guid id,
            Guid materialId);

        /// <summary>
        /// Performs any necessary processing on the waste description codes and
        /// updates them
        /// </summary>
        /// <param name="id">The id of the accreditation</param>
        /// <param name="siteId">The id of the site (This should be an overseas site)</param>
        /// <param name="materialId">The id of the material</param>
        /// <param name="wasteDescriptionCodes">List of waste description codes</param>
        /// <returns>Async task</returns>
        Task UpdateWasteDescriptionCodes(
            Guid id,
            Guid materialId,
            IEnumerable<string> wasteDescriptionCodes);

        Task<bool?> GetReprocessedWasteLastYear(
            Guid id,
            Guid materialId);

        Task UpdateReprocessedWasteLastYear(
            Guid id,
            Guid materialId,
            ReprocessedWasteLastYear reprocessedWasteLastYear);

        public Task<MaterialReprocessorDetails> GetReprocessedWasteLastYearData(
            Guid accreditationExternalId,
            Guid materialExternalId);

        Task<bool?> GetHasNpwdAccreditationNumber(
            Guid id,
            Guid materialId);

        Task UpdateHasNpwdAccreditationNumber(
            Guid accreditationExternalId,
            Guid materialExternalId,
            HasNpwdAccreditationNumber hasNpwdAccreditationNumber);

        Task<string> GetNpwdAccreditationNumber(
            Guid id,
            Guid materialId);

        Task UpdateNpwdAccreditationNumber(
            Guid accreditationExternalId,
            Guid materialExternalId,
            NpwdAccreditationNumber npwdAccreditationNumber);
    }
}
