using AutoMapper;
using WebApiTest.Controllers.DTOs.Cinema;
using WebApiTest.Models;

namespace WebApiTest.Controllers.Profiles
{
    public class CinemaProfile : Profile
    {
        public CinemaProfile()
        {
            CreateMap<CreateCinemaDto, Cinema>();
            CreateMap<Cinema, ReadCinemaDto>();
            CreateMap<UpdateCinemaDto, Cinema>();
        }
    }
}
