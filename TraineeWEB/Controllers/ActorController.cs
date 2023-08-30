using Microsoft.AspNetCore.Mvc;
using TraineeWEB.Services;
using TraineeWEB.Models;
using TraineeWEB.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TraineeWEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IActorService _actorService;
        private readonly IFilmService _filmService;


        public ActorController(IActorService actorService, IFilmService filmService)
        {
            _actorService = actorService;
            _filmService = filmService;
        }

        [HttpGet(Name = "GetAllActors")]
        public IEnumerable<Actor> Get()
        {
            return _actorService.Get();
        }

        [HttpGet("{id}", Name = "GetActorById")]
        public IActionResult GetActorById(int id)
        {
            Actor actor = _actorService.GetActorById(id);
            if (actor == null)
            {
                return NotFound();
            }
            return new ObjectResult(actor);
        }

        [HttpGet("lastName/{lastName}", Name = "GetActorByLastName")]
        public IActionResult GetActorByLastName(string lastName)
        {
            Actor actor = _actorService.GetActorByLastName(lastName);
            if (actor == null)
            {
                return NotFound();
            }
            return new ObjectResult(actor);
        }

        [HttpGet("nationality/{nationality}", Name = "GetActorByNationality")]
        public IActionResult GetActorByNationality(string nationality)
        {
            Actor actor = _actorService.GetActorByNationality(nationality);
            if (actor == null)
            {
                return NotFound();
            }
            return new ObjectResult(actor);
        }

        [HttpGet("age/{age}", Name = "GetActorByAge")]
        public IActionResult GetActorByAge(int age)
        {
            Actor actor = _actorService.GetActorByAge(age);
            if (actor == null)
            {
                return NotFound();
            }
            return new ObjectResult(actor);
        }

        [HttpGet("{filmId}/actors")]
        public IActionResult GetActorsInFilm(int filmId)
        {
            var actors = _actorService.GetActorsInFilm(filmId);

            if (actors == null)
            {
                return NotFound();
            }

            return Ok(actors);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Actor actor)
        {
            if (actor == null)
            {
                return BadRequest();
            }
            _actorService.Create(actor);
            return CreatedAtRoute("GetActorById", new { id = actor.Id }, actor);
        }

        [HttpPost("{filmId}/actors")]
        public IActionResult AttachActorsToFilm(int filmId, [FromBody] AttachActorsDTO attachActorsDTO)
        {
            var film = _filmService.GetFilmById(filmId);

            if (film == null)
            {
                return NotFound(); // Фильм не найден
            }

            var actors = _actorService.GetActorsByIds(attachActorsDTO.ActorIds);

            if (actors == null || actors.Count() != attachActorsDTO.ActorIds.Count())
            {
                return NotFound(); // Некоторые актеры не найдены
            }

            foreach (var actor in actors)
            {
                film.ActorRoles.Add(new ActorRole { Actor = actor, CharacterName = attachActorsDTO.CaracterName });
            }

            _filmService.Update(filmId, film);

            return Ok();
        }

        

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Actor actor)
        {
            Actor updatedActor = _actorService.Update(id, actor);
            if (updatedActor == null)
            {
                return NotFound();
            }
            return Ok(updatedActor);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteActor(int id)
        {
            Actor deletedActor = _actorService.Delete(id);
            if (deletedActor == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
