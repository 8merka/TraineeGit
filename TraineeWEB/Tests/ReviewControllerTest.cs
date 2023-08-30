using Xunit;
using Moq;
using TraineeWEB.Controllers;
using TraineeWEB.Services;
using TraineeWEB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MyTests
{
    public class ActorControllerTests
    {
        private Mock<IActorService> _mockActorService;
        private Mock<IFilmService> _mockFilmService;
        private ActorController _actorController;

        public ActorControllerTests()
        {
            _mockActorService = new Mock<IActorService>();
            _mockFilmService = new Mock<IFilmService>();
            _actorController = new ActorController(_mockActorService.Object, _mockFilmService.Object);
        }



        [Fact]
        public void GetActorByLastName_ReturnsNotFound_WhenActorDoesNotExist()
        {
            // Arrange
            string lastName = "Hanks";
            _mockActorService.Setup(x => x.GetActorByLastName(lastName)).Returns((Actor)null);

            // Act
            var result = _actorController.GetActorByLastName(lastName);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetActorByLastName_ReturnsOk_WhenActorExists()
        {
            // Arrange
            string lastName = "Hanks";
            var actor = new Actor { Id = 1, FirstName = "Tom", LastName = lastName };
            _mockActorService.Setup(x => x.GetActorByLastName(lastName)).Returns(actor);

            // Act
            var result = _actorController.GetActorByLastName(lastName);

            // Assert
            Assert.IsType<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.Equal(actor, objectResult.Value);
        }

        [Fact]
        public void GetActorByNationality_ReturnsNotFound_WhenActorDoesNotExist()
        {
            // Arrange
            string nationality = "American";
            _mockActorService.Setup(x => x.GetActorByNationality(nationality)).Returns((Actor)null);

            // Act
            var result = _actorController.GetActorByNationality(nationality);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetActorByNationality_ReturnsOk_WhenActorExists()
        {
            // Arrange
            string nationality = "American";
            var actor = new Actor { Id = 1, FirstName = "Tom", LastName = "Hanks", Nationality = nationality };
            _mockActorService.Setup(x => x.GetActorByNationality(nationality)).Returns(actor);

            // Act
            var result = _actorController.GetActorByNationality(nationality);

            // Assert
            Assert.IsType<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.Equal(actor, objectResult.Value);
        }

        [Fact]
        public void GetActorByAge_ReturnsNotFound_WhenActorDoesNotExist()
        {
            // Arrange
            int age = 50;
            _mockActorService.Setup(x => x.GetActorByAge(age)).Returns((Actor)null);

            // Act
            var result = _actorController.GetActorByAge(age);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetActorByAge_ReturnsOk_WhenActorExists()
        {
            // Arrange
            int age = 50;
            var actor = new Actor { Id = 1, FirstName = "Tom", LastName = "Hanks", Age = age };
            _mockActorService.Setup(x => x.GetActorByAge(age)).Returns(actor);

            // Act
            var result = _actorController.GetActorByAge(age);

            // Assert
            Assert.IsType<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.Equal(actor, objectResult.Value);
        }

        [Fact]
        public void GetActorsInFilm_ReturnsNotFound_WhenFilmDoesNotExist()
        {
            // Arrange
            int filmId = 1;
            _mockActorService.Setup(x => x.GetActorsInFilm(filmId)).Returns((IEnumerable<Actor>)null);

            // Act
            var result = _actorController.GetActorsInFilm(filmId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetActorsInFilm_ReturnsOk_WhenFilmExists()
        {
            // Arrange
            int filmId = 1;
            var actors = new List<Actor>
    {
        new Actor { Id = 1, FirstName = "Tom", LastName = "Hanks" },
        new Actor { Id = 2, FirstName = "Meryl", LastName = "Streep" }
    };
            _mockActorService.Setup(x => x.GetActorsInFilm(filmId)).Returns(actors);

            // Act
            var result = _actorController.GetActorsInFilm(filmId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okObjectResult = result as OkObjectResult;
            Assert.Equal(actors, okObjectResult.Value);
        }

        [Fact]
        public void Create_ReturnsBadRequest_WhenActorIsNull()
        {
            // Arrange
            Actor actor = null;

            // Act
            var result = _actorController.Create(actor);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void Create_ReturnsCreatedAtRoute_WhenActorIsNotNull()
        {
            // Arrange
            var actor = new Actor { Id = 1, FirstName = "Tom", LastName = "Hanks" };
            _mockActorService.Setup(x => x.Create(actor)).Verifiable();

            // Act
            var result = _actorController.Create(actor);

            // Assert
            Assert.IsType<CreatedAtRouteResult>(result);
            var createdAtRouteResult = result as CreatedAtRouteResult;
            Assert.Equal("GetActorById", createdAtRouteResult.RouteName);
            Assert.Equal(actor.Id, createdAtRouteResult.RouteValues["id"]);
            Assert.Equal(actor, createdAtRouteResult.Value);
        }

        [Fact]
        public void Update_ReturnsNotFound_WhenActorDoesNotExist()
        {
            // Arrange
            int id = 1;
            var actor = new Actor { Id = id, FirstName = "Tom", LastName = "Hanks" };
            _mockActorService.Setup(x => x.Update(id, actor)).Returns((Actor)null);

            // Act
            var result = _actorController.Update(id, actor);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Update_ReturnsOk_WhenActorExists()
        {
            // Arrange
            int id = 1;
            var actor = new Actor { Id = id, FirstName = "Tom", LastName = "Hanks" };
            _mockActorService.Setup(x => x.Update(id, actor)).Returns(actor);

            // Act
            var result = _actorController.Update(id, actor);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okObjectResult = result as OkObjectResult;
            Assert.Equal(actor, okObjectResult.Value);
        }

        [Fact]
        public void DeleteActor_ReturnsNotFound_WhenActorDoesNotExist()
        {
            // Arrange
            int id = 1;
            _mockActorService.Setup(x => x.Delete(id)).Returns((Actor)null);

            // Act
            var result = _actorController.DeleteActor(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteActor_ReturnsNoContent_WhenActorExists()
        {
            // Arrange
            int id = 1;
            var actor = new Actor { Id = id, FirstName = "Tom", LastName = "Hanks" };
            _mockActorService.Setup(x => x.Delete(id)).Returns(actor);

            // Act
            var result = _actorController.DeleteActor(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }



    }
}
