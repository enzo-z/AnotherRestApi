using AutoMapper;
using WebApiTest.Controllers.DTOs;
using WebApiTest.Models;

namespace WebApiTest.Controllers.Profiles
{
    public class FilmeProfle : Profile
    {
        public FilmeProfle()
        {
            CreateMap<CreateFilmeDto, Filme>();
            CreateMap<Filme, ReadFilmeDto>();
            CreateMap<Filme, UpdateFilmeDto>();
        }
    }
}
