using System.ComponentModel.DataAnnotations;

namespace EPR.Accreditation.Facade.Common.Dtos
{
    public class OverseasReprocessingSiteOutputs
    {
        public Guid? ExternalId { get; set; }

        [MaxLength(500)]
        public string Outputs {  get; set; }
    }
}
