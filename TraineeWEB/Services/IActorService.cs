using System.Collections.Generic;
using TraineeWEB.Models;

namespace TraineeWEB.Services
{
    public interface IActorService
    {
        IEnumerable<Actor> Get();
        IEnumerable<Actor> GetActorsInFilm(int filmId);
        IEnumerable<Actor> GetActorsByIds(IEnumerable<int> actorIds);
        void AttachActorsToFilm(int filmId, IEnumerable<int> actorIds);
        Actor GetActorById(int id);
        Actor GetActorByLastName(string name);
        Actor GetActorByNationality(string nationality);
        Actor GetActorByAge(int age);
        void Create(Actor actor);
        Actor Update(int id, Actor actor);
        Actor Delete(int id);
    }
}