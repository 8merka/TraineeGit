using Xunit;
using Moq;
using TraineeWEB.Controllers;
using TraineeWEB.Services;
using TraineeWEB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MyTests
{
    public class FilmControllerTests
    {
        private Mock<IFilmService> _mockFilmService;
        private FilmController _filmController;

        public FilmControllerTests()
        {
            _mockFilmService = new Mock<IFilmService>();
            _filmController = new FilmController(_mockFilmService.Object);
        }

        [Fact]
        public void Get_ReturnsAllFilms()
        {
            // Arrange
            var films = new List<Film>
            {
                new Film { Id = 1, Title = "The Shawshank Redemption" },
                new Film { Id = 2, Title = "The Godfather" }
            };
            _mockFilmService.Setup(x => x.Get()).Returns(films);

            // Act
            var result = _filmController.Get();

            // Assert
            Assert.Equal(films, result);
        }

        [Fact]
        public void GetFilmById_ReturnsNotFound_WhenFilmDoesNotExist()
        {
            // Arrange
            int id = 1;
            _mockFilmService.Setup(x => x.GetFilmById(id)).Returns((Film)null);

            // Act
            var result = _filmController.Get(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetFilmById_ReturnsOk_WhenFilmExists()
        {
            // Arrange
            int id = 1;
            var film = new Film { Id = id, Title = "The Shawshank Redemption" };
            _mockFilmService.Setup(x => x.GetFilmById(id)).Returns(film);

            // Act
            var result = _filmController.Get(id);

            // Assert
            Assert.IsType<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.Equal(film, objectResult.Value);
        }

        [Fact]
        public void GetFilmByTitle_ReturnsNotFound_WhenFilmDoesNotExist()
        {
            // Arrange
            string title = "The Shawshank Redemption";
            _mockFilmService.Setup(x => x.GetByTitle(title)).Returns((Film)null);

            // Act
            var result = _filmController.GetByTitle(title);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetFilmByTitle_ReturnsOk_WhenFilmExists()
        {
            // Arrange
            string title = "The Shawshank Redemption";
            var film = new Film { Id = 1, Title = title };
            _mockFilmService.Setup(x => x.GetByTitle(title)).Returns(film);

            // Act
            var result = _filmController.GetByTitle(title);

            // Assert
            Assert.IsType<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.Equal(film, objectResult.Value);
        }

        [Fact]
        public void GetFilmsByActor_ReturnsAllFilms()
        {
            // Arrange
            int actorId = 1;
            var films = new List<Film>
            {
                new Film { Id = 1, Title = "The Shawshank Redemption" },
                new Film { Id = 2, Title = "The Godfather" }
            };
            _mockFilmService.Setup(x => x.GetFilmsByActor(actorId)).Returns(films);

            // Act
            var result = _filmController.GetFilmsByActor(actorId);

            // Assert
            Assert.Equal(films, result);
        }

        [Fact]
        public void Create_ReturnsBadRequest_WhenFilmIsNull()
        {
            // Arrange
            Film film = null;

            // Act
            var result = _filmController.Create(film);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void Create_ReturnsCreatedAtRoute_WhenFilmIsNotNull()
        {
            // Arrange
            var film = new Film { Id = 1, Title = "The Shawshank Redemption" };
            _mockFilmService.Setup(x => x.Create(film)).Verifiable();

            // Act
            var result = _filmController.Create(film);

            // Assert
            Assert.IsType<CreatedAtRouteResult>(result);
            var createdAtRouteResult = result as CreatedAtRouteResult;
            Assert.Equal("GetFilmById", createdAtRouteResult.RouteName);
            Assert.Equal(film.Id, createdAtRouteResult.RouteValues["id"]);
            Assert.Equal(film, createdAtRouteResult.Value);
        }

        [Fact]
        public void Update_ReturnsNotFound_WhenFilmDoesNotExist()
        {
            // Arrange
            int id = 1;
            var film = new Film { Id = id, Title = "The Shawshank Redemption" };
            _mockFilmService.Setup(x => x.Update(id, film)).Returns((Film)null);

            // Act
            var result = _filmController.Update(id, film);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Update_ReturnsOk_WhenFilmExists()
        {
            // Arrange
            int id = 1;
            var film = new Film { Id = id, Title = "The Shawshank Redemption" };
            _mockFilmService.Setup(x => x.Update(id, film)).Returns(film);

            // Act
            var result = _filmController.Update(id, film);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okObjectResult = result as OkObjectResult;
            Assert.Equal(film, okObjectResult.Value);
        }
        [Fact]
        public void Delete_ReturnsNotFound_WhenFilmDoesNotExist()
        {
            // Arrange
            int id = 1;
            _mockFilmService.Setup(x => x.Delete(id)).Returns((Film)null);

            // Act
            var result = _filmController.Delete(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_ReturnsNoContent_WhenFilmExists()
        {
            // Arrange
            int id = 1;
            var film = new Film { Id = id, Title = "The Shawshank Redemption" };
            _mockFilmService.Setup(x => x.Delete(id)).Returns(film);

            // Act
            var result = _filmController.Delete(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }



    }
}
