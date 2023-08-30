using TraineeWEB.Models;

namespace TraineeWEB.Services
{
    public interface IFilmService
    {
        IEnumerable<Film> Get();
        IEnumerable<Film> GetFilmsByActor(int actorId);
        void AttachActorsToFilm(int filmId, IEnumerable<int> actorIds);
        Film GetFilmById(int id);
        Film GetByTitle(string Title);
        void Create(Film film);
        Film Update(int id, Film film);
        Film Delete(int id);
    }
}
