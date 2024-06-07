using Microsoft.AspNetCore.Mvc;
using TraineeWEB.Models;
using TraineeWEB.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TraineeWEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilmController : ControllerBase
    {
        readonly IFilmService _filmService;
        
        public FilmController(IFilmService filmService)
        {
            _filmService = filmService;
        }

        [HttpGet(Name = "GetAllFilms")]
        public IEnumerable<Film> Get()
        {
            return _filmService.Get();
        }

        [HttpGet("id/{id}", Name = "GetFilmById")]
        public IActionResult Get(int id)
        {
            Film film = _filmService.GetFilmById(id);
            if (film == null)
            {
                return NotFound();
            }
            return new ObjectResult(film);
        }

        [HttpGet("title/{title}", Name = "GetFilmByTitle")]
        public IActionResult GetByTitle(string title)
        {
            Film film = _filmService.GetByTitle(title);
            if (film == null)
            {
                return NotFound();
            }
            return new ObjectResult(film);
        }

        [HttpGet("actor/{actorId}/films", Name = "GetFilmsByActor")]
        public IEnumerable<Film> GetFilmsByActor(int actorId)
        {
            return _filmService.GetFilmsByActor(actorId);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Film film)
        {
            if (film == null)
            {
                return BadRequest();
            }
            _filmService.Create(film);
            return CreatedAtRoute("GetFilmById", new {id = film.Id}, film);
        }



        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Film film)
        {
            Film updatedFilm = _filmService.Update(id, film);
            if (updatedFilm == null)
            {
                return NotFound();
            }
            return Ok(updatedFilm);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Film deletedFilm = _filmService.Delete(id);
            if (deletedFilm == null)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
