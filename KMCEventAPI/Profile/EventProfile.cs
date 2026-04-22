using AutoMapper;
using KMCEventAPI.DTO;
using KMCEventAPI.Model;

namespace KMCEventAPI.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EventWriteDTO, Event>();
            CreateMap<Event, EventReadDTO>();

            CreateMap<Registration, RegistrationReadDTO>()
                .ForMember(dest => dest.FullName,
                    opt => opt.MapFrom(src => src.Participant != null ? src.Participant.FullName : ""))
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(src => src.Participant != null ? src.Participant.Email : ""))
                .ForMember(dest => dest.Phone,
                    opt => opt.MapFrom(src => src.Participant != null ? src.Participant.Phone : ""))
                .ForMember(dest => dest.EventTitle,
                    opt => opt.MapFrom(src => src.Event != null ? src.Event.Title : ""));
        }
    }
}