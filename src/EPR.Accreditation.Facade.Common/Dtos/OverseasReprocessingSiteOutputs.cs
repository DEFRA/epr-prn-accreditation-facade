namespace EPR.Accreditation.Facade.Common.Dtos
{
    using System.ComponentModel.DataAnnotations;

    public class OverseasReprocessingSiteOutputs
    {
        [MaxLength(500)]
        public string Outputs {  get; set; }
    }
}
