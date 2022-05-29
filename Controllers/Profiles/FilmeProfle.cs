using AutoMapper;
using WebApiTest.Controllers.DTOs.Filme;
using WebApiTest.Models;

namespace WebApiTest.Controllers.Profiles
{
    public class FilmeProfle : Profile
    {
        public FilmeProfle()
        {
            CreateMap<CreateFilmeDto, Filme>();
            CreateMap<Filme, ReadFilmeDto>();
            CreateMap<UpdateFilmeDto, Filme>();
        }
    }
}
