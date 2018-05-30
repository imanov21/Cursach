using AutoMapper;
using BLL.DTO;
using DAL.Entities;
using DAL.Identity.Entities;

namespace BLL.Services
{
    public class AutoMapperConfig
    {
        public static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Vacancy, VacancyDTO>();
            cfg.CreateMap<Offer, OfferDTO>();
            cfg.CreateMap<Resume, ResumeDTO>();
            cfg.CreateMap<Experience, ExperienceDTO>();
            cfg.CreateMap<Heading, HeadingDTO>();
            cfg.CreateMap<User, UserDTO>();
            cfg.CreateMap<ApplicationUser, UserDTO>();
        }
    }
}
