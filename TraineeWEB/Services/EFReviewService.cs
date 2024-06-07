using TraineeWEB.Data;
using TraineeWEB.Models;

namespace TraineeWEB.Services
{
    public class EFReviewService : IReviewService
    {
        private readonly EFFilmoSearchDbContext _context;
        public EFReviewService(EFFilmoSearchDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Review> Get()
        {
            return _context.Reviews;
        }

        public IEnumerable<Review> GetReviewsForFilm(int filmId)
        {
            return _context.Reviews.Where(review => review.FilmId == filmId).ToList();
        }

        public Review GetReviewById(int id)
        {
            return _context.Reviews.Find(id);
        }

        public Review GetReviewByTitle(string title)
        {
            return _context.Reviews.FirstOrDefault(review => review.Title == title);
        }

        public Review GetReviewByAuthor(string author)
        {
            return _context.Reviews.FirstOrDefault(review => review.Author == author);
        }

        public Review GetReviewByStars(int stars)
        {
            return _context.Reviews.FirstOrDefault(review => review.Stars == stars);
        }

        public void Create(Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
        }

        public Review Update(int id, Review review)
        {
            Review existingReview = _context.Reviews.Find(id);
            if (existingReview != null)
            {
                existingReview.Title = review.Title;
                existingReview.Author = review.Author;
                existingReview.Description = review.Description;
                existingReview.Stars = review.Stars;

                _context.SaveChanges();
                return existingReview;
            }
            return null;
        }

        public Review Delete(int id)
        {
            Review reviewToDelete = _context.Reviews.Find(id);
            if (reviewToDelete != null)
            {
                _context.Reviews.Remove(reviewToDelete);
                _context.SaveChanges();
                return reviewToDelete;
            }
            return null;
        }

    }
}
