namespace EPR.Accreditation.Facade.Common.Dtos.Portal
{
    public class MaterialWasteInputsDto
    {
        public bool? WasteLastYear { get; set; }

        public decimal? UkPackagingWaste { get; set; }

        public decimal? NonUkPackagingWaste { get; set; }

        public decimal? NonPackagingWaste { get; set; }
    }
}
