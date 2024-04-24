namespace EPR.Accreditation.Facade.Common.Dtos.Portal
{
    using EPR.Accreditation.Facade.Common.Enums;
    using System;

    public class PrnTonnesPlannedDto
    {
        public Guid ExternalId { get; set; }

        public PrnPlannedTonnesType? PrnPlannedTonnesType { get; set; }

        public decimal? PrnPlannedTonnesFee { get; set; }
    }
}
