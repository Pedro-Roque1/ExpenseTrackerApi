using AutoMapper;
using ExpenseTrackerApi.Models.Dtos;

namespace ExpenseTrackerApi.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<User, UserResponseDto>();
        }
    }
}
