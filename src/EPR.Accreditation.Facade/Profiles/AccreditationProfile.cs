using AutoMapper;
using EPR.Accreditation.Facade.Common.Dtos;
using EPR.Accreditation.Facade.Common.Dtos.Portal;

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
        }
    }
}
