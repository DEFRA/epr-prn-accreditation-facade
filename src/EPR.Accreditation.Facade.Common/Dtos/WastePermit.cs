using System.ComponentModel.DataAnnotations;

namespace EPR.Accreditation.Facade.Common.Dtos
{
    public class WastePermit
    {
        public int Id { get; set; }

        public int AccreditationId { get; set; }

        public int? OverseasReprocessingSiteId { get; set; }

        [MaxLength(20)]
        public string DealerRegistrationNumber { get; set; }

        [MaxLength(20)]
        public string EnvironmentalPermitNumber { get; set; }

        [MaxLength(20)]
        public string PartAActivityReferenceNumber { get; set; }

        [MaxLength(20)]
        public string PartBActivityReferenceNumber { get; set; }

        [MaxLength(20)]
        public string DischargeConsentNumber { get; set; }

        public bool? WastePermitExemption { get; set; }
    }
}
