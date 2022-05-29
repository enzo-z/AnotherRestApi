using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiTest.Controllers.DTOs.Cinema;
using WebApiTest.Controllers.Helpers;
using WebApiTest.Data;
using WebApiTest.Models;

namespace WebApiTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CinemaController : ControllerBase
    {
        private MyAppDbContext _dbContext;
        private IMapper _mapper;

        public CinemaController(MyAppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<ReadCinemaDto>? GetAll()
        {
            if (!_dbContext.Cinemas.Any()) return null;

            var cinemasDto = _dbContext.Cinemas.AsParallel().Select(c => _mapper.Map<ReadCinemaDto>(c));
            
            return cinemasDto;
        }

        [HttpGet(template: "{id}")]
        public IActionResult GetById(int id)
        {
            if (!existsInDatabase(id)) return NotFound();

            var cinema = _dbContext.Cinemas.First(c => c.Id == id);
            var cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
            return Ok(cinemaDto);  
        }

        [HttpDelete(template:"{id}")]
        public IActionResult DeleteById(int id)
        {
            if (!existsInDatabase(id)) return NotFound();

            var cinema = _dbContext.Cinemas.First(c => c.Id == id);
            _dbContext.Cinemas.Remove(cinema);

            if(changesInDatabaseOcurred(_dbContext.SaveChanges())) 
                return NoContent();

            return BadRequest(error: ErrorMessage.DeletionError);
        }

        public IActionResult UpdateById(int id, [FromBody] UpdateCinemaDto cinemaDto)
        {
            if (!existsInDatabase(id)) return NotFound();
            
            var cinema = _dbContext.Cinemas.First(c => c.Id==id);
            _mapper.Map(cinemaDto, cinema);

            if (changesInDatabaseOcurred(_dbContext.SaveChanges()))
                return NoContent();

            return BadRequest(new { Id = id, Erro = ErrorMessage.UpdateError });
        }

        [HttpPost(Name = "CreateCinema")]
        public IActionResult Post([FromBody] CreateCinemaDto cinemaDto)
        {
            var cinema = _mapper.Map<Cinema>(cinemaDto);

            _dbContext.Cinemas.Add(cinema);
            if (changesInDatabaseOcurred(_dbContext.SaveChanges()))
            {
                return CreatedAtAction(
                    actionName: nameof(GetById),
                    routeValues: new { Id = cinema.Id },
                    value: cinema
                    );
            }

            return BadRequest(error: ErrorMessage.CreationError);
        }

        private bool existsInDatabase(int id) => _dbContext.Cinemas.Any((c) => c.Id == id);
        private bool changesInDatabaseOcurred(int changes) => changes > 0;
    }
}
