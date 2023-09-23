using AutoMapper;
using Microsoft.CodeAnalysis;
using Models.DTO;
using Ophthalmology.Models;

namespace OphthalmologySalon.Profiles
{
    public class VisitProfile : Profile
    {
        public VisitProfile()
        {
            CreateMap<Visit, VisitReadDTO>();
            CreateMap<VisitCreateDTO, Visit>();
        }
    }
}
