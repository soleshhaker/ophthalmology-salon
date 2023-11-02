using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Models.DTO;
using Ophthalmology.Models;

namespace OphthalmologySalon.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, RegisterUserDTO>();
            CreateMap<RegisterUserDTO, ApplicationUser>();
        }
    }
}