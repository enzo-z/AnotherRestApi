using Microsoft.AspNetCore.Mvc;
using WebApiTest.Models;
using WebApiTest.Data;
using AutoMapper;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using WebApiTest.Controllers.Helpers;
using WebApiTest.Controllers.DTOs.Filme;

namespace WebApiTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private readonly MyAppDbContext _dbContext;
        private readonly IMapper _mapper;

        public FilmeController(MyAppDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetAll")]
        public IEnumerable<ReadFilmeDto> GetAll()
        {
            var filmes = _dbContext.Filmes.AsParallel().Select(f => _mapper.Map<ReadFilmeDto>(f));
            //var myJson = CustomJsonSerializer.ToJson(filmes);
            return filmes;
        }

        [HttpGet(template: "{id}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            if (!existsInDatabase(id)) return NotFound();

            Filme filme = _dbContext.Filmes.First((filme) => filme.Id == id);
            var filmeDto = _mapper.Map<ReadFilmeDto>(filme); 
            return Ok(filmeDto);
        }

        [HttpPost(Name = "CreateFilme")]
        public IActionResult Post([FromBody] CreateFilmeDto filmeDto)
        {
            var filme = _mapper.Map<Filme>(filmeDto);
            _dbContext.Filmes.Add(filme);

            if(changesInDatabaseOccurred(_dbContext.SaveChanges()))
            {
                return CreatedAtAction
                    (
                    actionName: nameof(GetById),
                    routeValues: new { Id = filme.Id },
                    value: filme
                    );
            }
            return BadRequest(error: new { Erro = ErrorMessage.CreationError});
        }
        
        [HttpPut(template: "{id}", Name = "UpdateById")]
        public IActionResult UpdateById(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            if(!existsInDatabase(id)) return NotFound();
            
            Filme filme = _dbContext.Filmes.First((filme) => filme.Id == id);
            _mapper.Map(filmeDto, filme);

            if (changesInDatabaseOccurred(_dbContext.SaveChanges())) 
                return NoContent();

            return BadRequest(error: new { ElementId = id, Erro = ErrorMessage.UpdateError });
        }

        [HttpDelete(template:"{id}", Name ="DeleteById")]
        public IActionResult DeleteById(int id)
        {
            if (!existsInDatabase(id)) 
                return NotFound();

            var filme = _dbContext.Filmes.First(filme => filme.Id == id);
            _dbContext.Filmes.Remove(filme);

            if (changesInDatabaseOccurred(_dbContext.SaveChanges())) 
                return NoContent();

            return BadRequest(error: ErrorMessage.DeletionError); ;
        }

        private bool changesInDatabaseOccurred(int changes) => (changes > 0); 
        private bool existsInDatabase(int id) => _dbContext.Filmes.Any(filme => filme.Id == id);
    }
}
