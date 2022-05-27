using Microsoft.AspNetCore.Mvc;
using WebApiTest.Models;
using WebApiTest.Data;
using AutoMapper;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using WebApiTest.Controllers.Helpers;
using WebApiTest.Controllers.DTOs;

namespace WebApiTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : Controller
    {
        private readonly FilmesContext _context;
        private readonly IMapper _mapper;

        public FilmeController(FilmesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet(Name = "GetAll")]
        public IEnumerable<ReadFilmeDto> GetAll()
        {
            var filmes = _context.Filmes.AsParallel().Select(f => _mapper.Map<ReadFilmeDto>(f));
            //var myJson = CustomJsonSerializer.ToJson(filmes);
            return filmes;
        }

        
        [HttpGet(template: "{id}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            Filme? filme = _context.Filmes.FirstOrDefault((filme) => filme.Id == id);
            return (filme is null) ? NotFound() : Ok(filme);
        }

        
        [HttpPost(Name = "CreateFilme")]
        public IActionResult Post([FromBody] CreateFilmeDto filmeDto)
        {

            Filme filme = _mapper.Map<Filme>(filmeDto);

            _context.Filmes.Add(filme);

            int stateChanges = _context.SaveChanges();
            if (stateChanges > 0)
            {
                return CreatedAtAction
                    (
                    actionName: nameof(GetById),
                    routeValues: new { Id = filme.Id },
                    value: filme
                    );
            }
            return BadRequest(error: new { Erro = "Erro ao cadastrar filme!" });
        }

        
        [HttpPut(template: "{id}", Name = "UpdateById")]
        public IActionResult UpdateById(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            Filme? filme = _context.Filmes.FirstOrDefault((filme) => filme.Id == id);

            if (filme is null)
                return NotFound();

            filme.Titulo = filmeDto.Titulo;
            filme.Genero = filmeDto.Genero;
            filme.Duracao = filmeDto.Duracao;
            filme.Diretor = filmeDto.Diretor;

            int stateChanges = _context.SaveChanges();
            if (stateChanges > 0)
                return NoContent();

            return BadRequest(error: new { Erro = $"Erro ao atualizar o Filme de id {id}" });
        }


        [HttpDelete(template:"{id}", Name ="DeleteById")]
        public IActionResult DeleteById(int id)
        {
            Filme? filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if (filme is null) return NotFound();

            _context.Filmes.Remove(filme);
            int stateChanges = _context.SaveChanges();

            if (stateChanges > 0)
                return NoContent();

            return BadRequest(error: "Não foi possível executar a deleção do elemento");
        }
    }
}
