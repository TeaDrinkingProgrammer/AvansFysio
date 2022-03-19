using Domain;
using AutoMapper;

namespace Portal.Models {

    /// <summary>  
    /// AutoMapperProfile: Use to map domain object ot View model and reverse map  
    /// </summary>  
    public class MappingProfiles : Profile {

        public MappingProfiles() {
            CreateMap<Patient, PatientEditViewModel>()
                .IncludeMembers(s => s.PatientFile)
                .ReverseMap();
            CreateMap<PatientFile, PatientEditViewModel>()
                .ForMember(x => x.PatientFileId, opts => opts.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<Patient, PatientDetailsViewModel>().IncludeMembers(s => s.PatientFile).ReverseMap();
            CreateMap<PatientFile, PatientDetailsViewModel>();
            CreateMap<Remark, RemarkViewModel>().ReverseMap();
            CreateMap<Remark[], RemarkViewModel[]>().ReverseMap();

            CreateMap<Patient, PatientInfoViewModel>().ReverseMap();
            CreateMap<Patient, InfoDetailsViewModel>();

            CreateMap<Treatment, EditTreatmentViewModel>().ReverseMap();
        }

    }
}
