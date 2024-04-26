namespace EPR.Accreditation.Facade.Common.Dtos.Portal
{
    public class ReprocessingSupportingInformationDto
    {
        public bool? WasteLastYear { get; set; }

        public IEnumerable<ReprocessingSupportingInformationRecordDto> Records { get; set; }
    }
}
