using AutoMapper;
using KMCEventAPI.Model;
using KMCEventAPI.DTO;

namespace KMCEventAPI.Profiles
{
    public class RegistrationProfile : Profile
    {
        public RegistrationProfile()
        {
            CreateMap<Registration, RegistrationReadDTO>();
        }
    }
}