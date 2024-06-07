using Microsoft.EntityFrameworkCore;
using TraineeWEB.Data;
using TraineeWEB.Models;

namespace TraineeWEB.Services
{
    public class EFFilmService : IFilmService
    {
        private readonly EFFilmoSearchDbContext _context;

        public EFFilmService(EFFilmoSearchDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Film> Get()
        {
            return _context.Films;
        }
        public IEnumerable<Film> GetFilmsByActor(int actorId)
        {
            var actor = _context.Actors.Include(a => a.ActorRoles).ThenInclude(af => af.Film).SingleOrDefault(a => a.Id == actorId);

            return actor?.ActorRoles?.Select(actorRole => actorRole.Film);
        }
        public void AttachActorsToFilm(int filmId, IEnumerable<int> actorIds)
        {
            var film = _context.Films.Include(f => f.ActorRoles).FirstOrDefault(f => f.Id == filmId);

            if (film == null)
            {
                throw new ArgumentException("Film not found");
            }

            var existingActorIds = film.ActorRoles.Select(ar => ar.ActorId).ToList();
            var newActorIds = actorIds.Except(existingActorIds).ToList();

            foreach (var actorId in newActorIds)
            {
                film.ActorRoles.Add(new ActorRole { FilmId = filmId, ActorId = actorId });
            }

            _context.SaveChanges();
        }
        public Film GetFilmById(int id)
        {
            return _context.Films.Find(id);
        }

        public Film GetByTitle(string title)
        {
            return _context.Films.FirstOrDefault(film => film.Title == title);
        }

        public void Create(Film film)
        {
            _context.Films.Add(film);
            _context.SaveChanges();
        }
        public Film Update(int id, Film film)
        {
            Film existingFilm = _context.Films.Find(id);
            if (existingFilm != null)
            {
                existingFilm.Title = film.Title;
                existingFilm.Duration = film.Duration;
                _context.SaveChanges();
                return existingFilm;
            }
            return null;
        }
        public Film Delete(int id)
        {
            Film filmToDelete = _context.Films.Find(id);
            if (filmToDelete != null)
            {
                _context.Films.Remove(filmToDelete);
                _context.SaveChanges();
                return filmToDelete;
            }
            return null;
        }

    }
}
