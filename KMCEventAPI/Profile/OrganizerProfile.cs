using AutoMapper;
using KMCEventAPI.Model;
using KMCEventAPI.DTO;

namespace KMCEventAPI.Profiles
{
    public class OrganizerProfile : Profile
    {
        public OrganizerProfile()
        {
            CreateMap<OrganizerWriteDTO, Organizer>();
            CreateMap<Organizer, OrganizerReadDTO>();
        }
    }
}