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

            CreateMap<MaterialWasteOutputsDto, MaterialReprocessorDetails>()
                .ForMember(d => d.UkPackagingWaste, o => o.MapFrom(s => s.UkPackagingWaste))
                .ForMember(d => d.NonUkPackagingWaste, o => o.MapFrom(s => s.NonUkPackagingWaste))
                .ForMember(d => d.NonPackagingWaste, o => o.MapFrom(s => s.NonPackagingWaste));

            CreateMap<NonWasteInputRecordDto, ReprocessorSupportingInformation>()
                .ForMember(d => d.ReprocessorSupportingInformationTypeId, o => o.MapFrom(s => ReprocessorSupportingInformationType.NonWasteInputs));

            CreateMap<AccreditationMaterial, MaterialOutputsDto > ()
                .ForMember(d => d.WasteLastYear, o => o.MapFrom(s => s.WasteLastYear))
                .ForMember(d => d.TonnesContaminents, o => o.MapFrom(s => s.MaterialReprocessorDetails == null ? null : s.MaterialReprocessorDetails.Contaminents))
                .ForMember(d => d.TonnesNotProcessedOnSite, o => o.MapFrom(s => s.MaterialReprocessorDetails == null ? null : s.MaterialReprocessorDetails.MaterialsNotProcessedOnSite))
                .ForMember(d => d.TonnesProcessLoss, o => o.MapFrom(s => s.MaterialReprocessorDetails == null ? null : s.MaterialReprocessorDetails.ProcessLoss));

            CreateMap<AccreditationMaterial, MaterialWasteOutputsDto>()
                .ForMember(d => d.WasteLastYear, o => o.MapFrom(s => s.WasteLastYear))
                .ForMember(d => d.UkPackagingWaste, o => o.MapFrom(s => s.MaterialReprocessorDetails == null ? null : s.MaterialReprocessorDetails.UkPackagingWaste))
                .ForMember(d => d.NonUkPackagingWaste, o => o.MapFrom(s => s.MaterialReprocessorDetails == null ? null : s.MaterialReprocessorDetails.NonUkPackagingWaste))
                .ForMember(d => d.NonPackagingWaste, o => o.MapFrom(s => s.MaterialReprocessorDetails == null ? null : s.MaterialReprocessorDetails.NonPackagingWaste));
            CreateMap<ReprocessingSupportingInformationRecordDto, ReprocessorSupportingInformation>()
                .ForMember(
                    d => d.ReprocessorSupportingInformationTypeId, 
                    opt => opt.MapFrom((src, dest, destMember, context) => context.Items["ReprocessorSupportingInformationType"]));
        }
    }
}

