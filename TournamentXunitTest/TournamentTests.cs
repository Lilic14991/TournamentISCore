using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TournamentsIS.Controllers;
using TournamentsIS.Models;
using TournamentsIS.Repository;
using Xunit;


namespace TournamentXunitTest
{
    public class TournamentTests
    {
        private readonly Mock<ITourRepo> _mockRepo;
        private readonly TournamentsController _controller;

        public TournamentTests()
        {
            _mockRepo = new Mock<ITourRepo>();
            _controller = new TournamentsController(_mockRepo.Object);
        }
        // Tournament Index test | 
        [Fact]
        public void Index_Tournament_ShouldReturnView()
        {
            var result = _controller.Index();

            Assert.IsAssignableFrom<IActionResult>(result);
        }

        // Tournament Create test |
        [Fact]
        public void Create_Tournament_ShouldReturnView()
        {
            var result = _controller.Create();

            Assert.IsAssignableFrom<IActionResult>(result);

        }

        // After create should redirect to Index Test |
        [Fact]
        public void Create_Tournament_RedirectToIndexAction()
        {
            var tournament = new Tournaments
            {
                TournamentName = "Testing 2020",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                TournamentLocation = "Belgrade"

            };

            var result = _controller.Create(tournament);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
        // Create return view Test InvalidModel
        [Fact]
        public void Create_Tournament_InvalidModel_ReturnView()
        {
            _controller.ModelState.AddModelError("TournamentName", "Enter tournament name");
            _controller.ModelState.AddModelError("TournamentLocation", "Enter tournament location");



            var tournament = new Tournaments
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            };

            var result = _controller.Create(tournament);

            _mockRepo.Verify(c => c.Create(It.IsAny<Tournaments>()), Times.Never);
        }
        // create validModel |
        [Fact]
        public void Create_Tournament_ValidModel_ReturnsView()
        {
            Tournaments tour = null;

            _mockRepo.Setup(c => c.Create
            (It.IsAny<Tournaments>()))
                .Callback<Tournaments>(t => tour = t);


            var tournaments = new Tournaments
            {
                TournamentName = "Testing 2020",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                TournamentLocation = "Belgrade"
            };

            _controller.Create(tournaments);

            _mockRepo.Verify(c => c.Create(It.IsAny<Tournaments>()), Times.Once);

            Assert.Equal(tour.TournamentId, tournaments.TournamentId);
            Assert.Equal(tour.TournamentName, tournaments.TournamentName);
            Assert.Equal(tour.StartDate, tournaments.StartDate); 
            Assert.Equal(tour.EndDate, tournaments.EndDate);
            Assert.Equal(tour.TournamentLocation, tournaments.TournamentLocation);
        }
        [Fact]



        // Edit Test InvalidModel |

        public void Edit_Tournament_ReturnTournamentID()
        {
            int tournamentID = 2;

            var tournaments = new Tournaments
            {
                TournamentId = 2,
                TournamentName = "Tournament 2020",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(5),
                TournamentLocation = "Belgrade"
            };

            _mockRepo.Setup(e => e.FindElementById(tournamentID)).Returns(tournaments);

            var expectedModel = new Tournaments()
            {
                TournamentId = tournaments.TournamentId,
                TournamentName = tournaments.TournamentName,
                StartDate = tournaments.StartDate,
                EndDate = tournaments.EndDate,
                TournamentLocation = tournaments.TournamentLocation
            };

            var actual = _controller.Edit(tournamentID);

            var actualModel = actual.Model as Tournaments;

            Assert.Equal(actualModel.TournamentId, tournaments.TournamentId);
            Assert.Equal(actualModel.TournamentName, tournaments.TournamentName);
            Assert.Equal(actualModel.StartDate.ToString(), tournaments.StartDate.ToString());
            Assert.Equal(actualModel.EndDate.ToString(), tournaments.EndDate.ToString());
            Assert.Equal(actualModel.TournamentLocation, tournaments.TournamentLocation);
        }


        [Fact]
        public void Edit_Tournament_InvalidModel()
        {
            _controller.ModelState.AddModelError("TournamentName", "Enter new tournament name");
            // _controller.ModelState.AddModelError("TournamentLocation", "Enter new tournament location");


            var tournament = new Tournaments
            {
                TournamentName = "Tournament 2020",
                TournamentLocation = "Smederevo"
            };

            _controller.Edit(tournament);

            _mockRepo.Verify(e => e.Edit(It.IsAny<Tournaments>()), Times.Never);
        }
        // Edit Test ValidModel |
        [Fact]
        public void Edit_Tournament_ValidModel()
        {
            Tournaments tour = null;

            _mockRepo.Setup(e => e.Edit(It.IsAny<Tournaments>()))
            .Callback<Tournaments>(t => tour = t);

            var tournaments = new Tournaments()
            {
                TournamentId = 2,
                TournamentName = "Tournament 2020",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                TournamentLocation = "Belgrade"
            };

            _controller.Edit(tournaments);

            _mockRepo.Verify(e => e.Edit(It.IsAny<Tournaments>()), Times.Once);

            Assert.Equal(tour.TournamentName, tournaments.TournamentName);
            Assert.Equal(tour.StartDate.ToString(), tournaments.StartDate.ToString());
            Assert.Equal(tour.EndDate.ToString(), tournaments.EndDate.ToString());
            Assert.Equal(tour.TournamentLocation, tournaments.TournamentLocation);
        }

        // Edit Test Redirect To Index |
        [Fact]
        public void Edit_Tournament_RedirectToIndexAction()
        {
            var tournaments = new Tournaments()
            {
                TournamentId = 2,
                TournamentName = "Tournament 2020",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                TournamentLocation = "Belgrade"
            };

            var result = _controller.Edit(tournaments);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Delete_Tournament_ReturnTournamentID()
        {
            int tournamentID = 2;

            var tournaments = new Tournaments()
            {
                TournamentId = 2,
                TournamentName = "Tournament 2020",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(10),
                TournamentLocation = "Belgrade"
            };


            _mockRepo.Setup(d => d.Delete(tournamentID)).Returns(tournaments);

            var expectedModel = new Tournaments()
            {
                TournamentId = tournaments.TournamentId,
                TournamentName = tournaments.TournamentName,
                StartDate = tournaments.StartDate,
                EndDate = tournaments.EndDate,
                TournamentLocation = tournaments.TournamentLocation
            };

            var actual = _controller.Delete(tournamentID);

            var actualModel = actual.Model as Tournaments;

            Assert.Equal(expectedModel.TournamentId, tournaments.TournamentId);
            Assert.Equal(expectedModel.TournamentName, tournaments.TournamentName);
            Assert.Equal(expectedModel.StartDate.ToString(), tournaments.StartDate.ToString());
            Assert.Equal(expectedModel.EndDate.ToString(), tournaments.EndDate.ToString());
            Assert.Equal(expectedModel.TournamentLocation, tournaments.TournamentLocation);
        }

        [Fact]
        public void DeleteConfirmed_Tournament_Should_Delete()
        {
            var tournaments = new Tournaments()
            {
                TournamentId = 2,
                TournamentName = "Tournament 2020",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(5),
                TournamentLocation = "Belgrade,"

            };

            _mockRepo.Setup(d => d.DeleteConfirmed(tournaments));

            _controller.DeleteConfirmed(tournaments);

            _mockRepo.Verify(d => d.DeleteConfirmed(It.IsAny<Tournaments>()), Times.Once);
        }
        [Fact]
        public void Delete_Tournament_RedirectToIndexAction()
        {
            var tournaments = new Tournaments
            {
                TournamentId = 2,
                TournamentName = "Tournament 2020",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(5),
                TournamentLocation = "Belgrade"
            };

            var result = _controller.DeleteConfirmed(tournaments);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Details_Tournament_ReturnTournamentID()
        {
            int tournamentID = 2;

            var tournaments = new Tournaments()
            {
                TournamentId = 2,
                TournamentName = "Tournament 2020",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(5),
                TournamentLocation = "Belgrade"
            };

            _mockRepo.Setup(d => d.Details(tournamentID)).Returns(tournaments);

            var expectedModel = new Tournaments
            {
                TournamentId = tournaments.TournamentId,
                TournamentName = tournaments.TournamentName,
                StartDate = tournaments.StartDate,
                EndDate = tournaments.EndDate
            };

            var actual = _controller.Details(tournamentID);

            var actualModel = actual.Model as Tournaments;

            Assert.Equal(actualModel.TournamentId, tournaments.TournamentId);
            Assert.Equal(actualModel.TournamentName, tournaments.TournamentName);
            Assert.Equal(actualModel.StartDate, tournaments.StartDate);
            Assert.Equal(actualModel.EndDate, tournaments.EndDate);
            Assert.Equal(actualModel.TournamentLocation, tournaments.TournamentLocation);
        }






           













    }
}
       
