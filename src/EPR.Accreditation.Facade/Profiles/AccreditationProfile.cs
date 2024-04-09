using AutoMapper;
using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.Dtos.Portal;
using EPR.Accreditation.Facade.Common.Enums;

namespace EPR.Accreditation.Facade.Profiles
{
    public class AccreditationProfile : Profile
    {
        public AccreditationProfile()
        {
            CreateMap<MaterialOutputsDto, MaterialReprocessorDetails>()
                .ForMember(d => d.MaterialsNotProcessedOnSite, o => o.MapFrom(s => s.TonnesNotProcessedOnSite))
                .ForMember(d => d.Contaminents, o => o.MapFrom(s => s.TonnesContaminents))
                .ForMember(d => d.ProcessLoss, o => o.MapFrom(s => s.TonnesProcessLoss));

            CreateMap<NonWasteInputRecordDto, ReprocessorSupportingInformation>()
                .ForMember(d => d.ReprocessorSupportingInformationTypeId, o => o.MapFrom(s => ReprocessorSupportingInformationType.NonWasteInputs));
        }
    }
}
