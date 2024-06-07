using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TraineeWEB.Models;
using TraineeWEB.Services;
using TraineeWEB.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TraineeWEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly EFFilmoSearchDbContext _context;

        public ReviewController(IReviewService reviewService, EFFilmoSearchDbContext context)
        {
            _reviewService = reviewService;
            _context = context;
        }

        [HttpGet(Name = "GetAllReviews")]
        public IEnumerable<Review> Get()
        {
            return _reviewService.Get();
        }

        [HttpGet("reviews", Name = "GetAllReviewsByFilm")]
        public IActionResult GetReviewsForFilm(int filmId)
        {
            var reviews = _reviewService.GetReviewsForFilm(filmId);

            if (reviews == null || !reviews.Any())
            {
                return NotFound(); 
            }

            return new ObjectResult(reviews);
        }

        [HttpGet("id/{id}", Name = "GetReviewById")]
        public IActionResult GetReviewById(int id)
        {
            Review review = _reviewService.GetReviewById(id);
            if (review == null)
            {
                return NotFound();
            }
            return new ObjectResult(review);
        }

        [HttpGet("title/{title}", Name = "GetReviewByTitle")]
        public IActionResult GetReviewByTitle(string title)
        {
            Review review = _reviewService.GetReviewByTitle(title);
            if(review == null)
            {
                return NotFound();
            }
            return new ObjectResult(review);
        }

        [HttpGet("author/{author}", Name = "GetReviewByAuthor")]
        public IActionResult GetReviewByAuthor(string author)
        {
            Review review = _reviewService.GetReviewByAuthor(author);
            if (review == null)
            {
                return NotFound();
            }
            return new ObjectResult(review);
        }

        [HttpGet("stars/{stars}", Name = "GetReviewByStars")]
        public IActionResult GetReviewByStars(int stars)
        {
            Review review = _reviewService.GetReviewByStars(stars);
            if (review == null)
            {
                return NotFound();
            }
            return new ObjectResult(review);
        }

        [HttpPost]
        public IActionResult CreateReview(ReviewCreateDTO reviewDTO)
        {
            var film = _context.Films.Find(reviewDTO.filmId);

            if (film == null)
            {
                return NotFound(); 
            }

            var review = new Review
            {
                Title = reviewDTO.title,
                Author = reviewDTO.author,
                Description = reviewDTO.description,
                Stars = reviewDTO.stars,
                Film = film
            };

            _reviewService.Create(review);

            return CreatedAtAction(nameof(GetReviewById), new { id = review.Id }, review);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Review review)
        {
            Review updatedReview = _reviewService.Update(id, review);
            if (updatedReview == null)
            {
                return NotFound();
            }
            return Ok(updatedReview);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Review deletedReview = _reviewService.Delete(id);
            if (deletedReview == null)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
