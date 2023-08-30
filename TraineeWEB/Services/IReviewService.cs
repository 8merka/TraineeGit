using System;
using System.Collections.Generic;
using TraineeWEB.Models;

namespace TraineeWEB.Services
{
    public interface IReviewService
    {
        IEnumerable<Review> Get();
        IEnumerable<Review> GetReviewsForFilm(int filmId);
        Review GetReviewById(int id);
        Review GetReviewByTitle(string title);
        Review GetReviewByAuthor(string author);
        Review GetReviewByStars(int stars);
        void Create(Review review);
        Review Update(int id, Review review);
        Review Delete(int id);
    }
}