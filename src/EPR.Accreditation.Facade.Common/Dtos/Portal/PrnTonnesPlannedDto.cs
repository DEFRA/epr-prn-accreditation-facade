namespace EPR.Accreditation.Facade.Common.Dtos.Portal
{
    using EPR.Accreditation.Facade.Common.Enums;
    using System;

    /// <summary>
    /// A DTO containing PRN tonnage data
    /// </summary>
    public class PrnTonnesPlannedDto
    {
        public Guid Id { get; set; }

        public PrnPlannedTonnesType? PrnPlannedTonnesType { get; set; }

        public decimal? PrnPlannedTonnesFee { get; set; }
    }
}
