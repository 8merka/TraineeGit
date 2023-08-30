using Microsoft.EntityFrameworkCore;
using TraineeWEB.Data;
using TraineeWEB.Models;

namespace TraineeWEB.Services
{
    public class EFActorService : IActorService
    {
        private readonly EFFilmoSearchDbContext _context;
        public EFActorService(EFFilmoSearchDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Actor> Get()
        {
            return _context.Actors;
        }

        public IEnumerable<Actor> GetActorsInFilm(int filmId)
        {
            var film = _context.Films.Include(film => film.ActorRoles).ThenInclude(actorRole => actorRole.Actor).SingleOrDefault(film => film.Id == filmId);

            return film?.ActorRoles?.Select(actorRole => actorRole.Actor);
        }

        public IEnumerable<Actor> GetActorsByIds(IEnumerable<int> actorIds)
        {
            return _context.Actors.Where(actor => actorIds.Contains(actor.Id));
        }

        public void AttachActorsToFilm(int filmId, IEnumerable<int> actorIds)
        {
            var film = _context.Films.Find(filmId);

            if (film == null)
            {
                throw new ArgumentException("Film not found");
            }

            var actors = _context.Actors.Where(actor => actorIds.Contains(actor.Id)).ToList();
            foreach (var actor in actors)
            {
                film.ActorRoles.Add(new ActorRole { Actor = actor, Film = film });
            }

            _context.SaveChanges();
        }

        public Actor GetActorById(int id)
        {
            return _context.Actors.Find(id);
        }

        public Actor GetActorByLastName(string lastName)
        {
            return _context.Actors.FirstOrDefault(Actor => Actor.LastName == lastName);
        }

        public Actor GetActorByNationality(string nationality)
        {
            return _context.Actors.FirstOrDefault(Actor => Actor.Nationality == nationality);
        }

        public Actor GetActorByAge(int age)
        {
            return _context.Actors.FirstOrDefault(Actor => Actor.Age == age);
        }

        public void Create(Actor actor)
        {
            _context.Actors.Add(actor);
            _context.SaveChanges();
        }
        public Actor Update(int id, Actor actor)
        {
            Actor existingActor = _context.Actors.Find(id);
            if (existingActor != null)
            {
                existingActor.FirstName = actor.FirstName;
                existingActor.LastName = actor.LastName;
                existingActor.Nationality = actor.Nationality;
                existingActor.Age = actor.Age;
                existingActor.Biography = actor.Biography;
                
                _context.SaveChanges();
                return existingActor;
            }
            return null;
        }
        public Actor Delete(int id)
        {
            Actor actorToDelete = _context.Actors.Find(id);
            if (actorToDelete != null)
            {
                _context.Actors.Remove(actorToDelete);
                _context.SaveChanges();
                return actorToDelete;
            }
            return null;
        }

    }
}
