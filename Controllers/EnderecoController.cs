using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiTest.Controllers.DTOs.Endereco;
using WebApiTest.Controllers.Helpers;
using WebApiTest.Data;
using WebApiTest.Models;

namespace WebApiTest.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {
        private IMapper _mapper;
        private MyAppDbContext _dbContext;
        public EnderecoController(IMapper mapper, MyAppDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<ReadEnderecoDto>? GetAll()
        {
            if (!_dbContext.Enderecos.Any()) return null;

            var enderecosDto = _dbContext.Enderecos.AsParallel().Select(c => _mapper.Map<ReadEnderecoDto>(c));
            return enderecosDto;
        }

        [HttpGet(template:"{id}")]
        public IActionResult GetById(int id)
        {
            if (!existsInDatabase(id)) return NotFound();

            var endereco = _dbContext.Enderecos.First(e => e.Id == id);
            var enderecoDto = _mapper.Map<ReadEnderecoDto>(endereco);
            return Ok(enderecoDto);
        }


        [HttpPost]
        public IActionResult Post([FromBody] CreateEnderecoDto enderecoDto)
        {
            var endereco = _mapper.Map<Endereco>(enderecoDto);
            _dbContext.Add(endereco);

            if (changesInDatabaseOccurred(_dbContext.SaveChanges()))
            {
                return CreatedAtAction(
                        actionName: nameof(GetById),
                        routeValues: new { id = endereco.Id },
                        value: endereco
                    );
            }
            return BadRequest(error: ErrorMessage.CreationError);
        }


        [HttpPut(template:"{id}")]
        public IActionResult UpdateById(int id, [FromBody] UpdateEnderecoDto enderecoDto)
        {
            if (!existsInDatabase(id)) return NotFound();

            var endereco = _dbContext.Enderecos.First(e => e.Id == id);

            _mapper.Map(enderecoDto, endereco);

            if (changesInDatabaseOccurred(_dbContext.SaveChanges()))
                return NoContent();

            return BadRequest(error: new {ElementId = id, Erro = ErrorMessage.UpdateError});
        }

        [HttpDelete(template:"{id}")]
        public IActionResult DeleteById(int id)
        {
            if (!existsInDatabase(id)) return NotFound();

            var endereco = _dbContext.Enderecos.First(e => e.Id == id);
            _dbContext.Enderecos.Remove(endereco);

            if (changesInDatabaseOccurred(_dbContext.SaveChanges()))
                return NoContent();

            return BadRequest(error: ErrorMessage.DeletionError);
        }


        private bool changesInDatabaseOccurred(int changes) => changes > 0;
        private bool existsInDatabase(int id) => _dbContext.Enderecos.Any(filme => filme.Id == id);
    }
}
