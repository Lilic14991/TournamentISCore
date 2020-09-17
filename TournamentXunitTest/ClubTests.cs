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
    public class ClubTests
    {
        private Mock<IClubRepo> _mockRepo;
        private ClubsController _controller;

        public ClubTests()
        {
            _mockRepo = new Mock<IClubRepo>();
            _controller = new ClubsController(_mockRepo.Object);
        }

        [Fact]
        public void Idex_Club_ShouldReturnView()
        {
            var result = _controller.Index();

            Assert.IsAssignableFrom<IActionResult>(result);
        }

        // CREATE
        [Fact]
        public void Create_Club_RedirectToIndexAction()
        {
            var clubs = new Clubs()
            {
                ClubId = 3,
                ClubName = "Red Star",
                ClubAddress = "Marakana",
                ClubBudget = 140000,
                ClubDesc = "Fudbalski klub Crvena zvezda, which translates into English as simply Red Star, is a Serbian professional football club based in Belgrade commonly known elsewhere as Red Star Belgrade, the major part of the Red Star multi-sport club.",
            };


            var result = _controller.Create(clubs);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Create_Club_InvalidModel()
        {
            _controller.ModelState.AddModelError("ClubName", "Enter club name");
            // _controller.ModelState.AddModelError("ClubAddress", "Enter club address");
            //_controller.ModelState.AddModelError("ClubBudget", "Enter club's money");


            var clubs = new Clubs()
            {
                ClubAddress = "Marakana",
                ClubBudget = 140000,
                ClubDesc = "Red Star FC is Belgrade's football team. "
            };

            var result = _controller.Create(clubs);

            _mockRepo.Verify(c => c.Create(It.IsAny<Clubs>()), Times.Never);
        }

        [Fact]
        public void Create_Club_ValidModel()
        {
            Clubs clb = null;

            _mockRepo.Setup(c => c.Create(It.IsAny<Clubs>()))
                .Callback<Clubs>(c => clb = c);

            var clubs = new Clubs()
            {
                ClubId = 3,
                ClubName = "Red Star",
                ClubAddress = "Marakana",
                ClubBudget = 140000,
                ClubDesc = "Red Star Belgrade's Football team."
            };

            _controller.Create(clubs);

            _mockRepo.Verify(c => c.Create(It.IsAny<Clubs>()), Times.Once);

            Assert.Equal(clb.ClubId, clubs.ClubId);
            Assert.Equal(clb.ClubName, clubs.ClubName);
            Assert.Equal(clb.ClubAddress, clubs.ClubAddress);
            Assert.Equal(clb.ClubBudget, clubs.ClubBudget);
            Assert.Equal(clb.ClubDesc, clubs.ClubDesc);
        }

        // EDIT
        [Fact]
        public void Edit_Club_RedirectToIndexAction()
        {
            var clubs = new Clubs()
            {
                ClubId = 3,
                ClubName = "Red Star",
                ClubAddress = "Marakana",
                ClubBudget = 140000,
                ClubDesc = "Red Star Belgrade's Football team."
            };

            var result = _controller.Edit(clubs);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Edit_Club_ReturnClubID()
        {
            int? clubID = 3;

            var clubs = new Clubs()
            {
                ClubId = 3,
                ClubName = "Red Star",
                ClubAddress = "Marakana",
                ClubBudget = 140000,
                ClubDesc = "Red Star Belgrade's Football team."
            };

            _mockRepo.Setup(e => e.FindElementById(clubID)).Returns(clubs);

            var expectedModel = new Clubs()
            {
                ClubId = clubs.ClubId,
                ClubName = clubs.ClubName,
                ClubAddress = clubs.ClubAddress,
                ClubBudget = clubs.ClubBudget,
                ClubDesc = clubs.ClubDesc
            };

            var actual = _controller.Edit(clubID);

            var actualModel = actual.Model as Clubs;

            Assert.Equal(expectedModel.ClubId, actualModel.ClubId);
            Assert.Equal(expectedModel.ClubName, actualModel.ClubName);
            Assert.Equal(expectedModel.ClubAddress, actualModel.ClubAddress);
            Assert.Equal(expectedModel.ClubBudget, actualModel.ClubBudget);
            Assert.Equal(expectedModel.ClubDesc, actualModel.ClubDesc);
        }

        [Fact]
        public void Edit_Club_InvalidModel()
        {
            // _controller.ModelState.AddModelError("ClubName", "Enter club name");
            _controller.ModelState.AddModelError("ClubAddress", "Enter club address");
            //  _controller.ModelState.AddModelError("ClubBudget", "Enter club's money");

            var clubs = new Clubs()
            {
                ClubId = 3,
                ClubName = "Red Star",
                ClubBudget = 140000,
                ClubDesc = "Red Star Belgrade's Football Club"
            };

            _controller.Edit(clubs);

            _mockRepo.Verify(e => e.Edit(It.IsAny<Clubs>()), Times.Never);
        }

        [Fact]
        public void Edit_Club_ValidModel()
        {
            Clubs clb = null;

            _mockRepo.Setup(e => e.Edit(It.IsAny<Clubs>()))
                .Callback<Clubs>(c => clb = c);

            var clubs = new Clubs()
            {
                ClubId = 3,
                ClubName = "Red Star",
                ClubAddress = "Marakana",
                ClubBudget = 140000,
                ClubDesc = "Red Star is Belgrade's Football Club"
            };

            _controller.Edit(clubs);

            _mockRepo.Verify(e => e.Edit(It.IsAny<Clubs>()), Times.Once);

            Assert.Equal(clb.ClubId, clubs.ClubId);
            Assert.Equal(clb.ClubName, clubs.ClubName);
            Assert.Equal(clb.ClubAddress, clubs.ClubAddress);
            Assert.Equal(clb.ClubBudget, clubs.ClubBudget);
            Assert.Equal(clb.ClubDesc, clubs.ClubDesc);
        }

        // DELETE
        [Fact]
        public void Delete_Club_ReturnClubID()
        {
            int? clubID = 3;

            var clubs = new Clubs()
            {
                ClubId = 3,
                ClubName = "Red Star",
                ClubAddress = "Marakana",
                ClubBudget = 140000,
                ClubDesc = "Fudbalski klub Crvena zvezda, which translates into English as simply Red Star, is a Serbian professional football club based in Belgrade commonly known elsewhere as Red Star Belgrade, the major part of the Red Star multi-sport club."

            };

            _mockRepo.Setup(d => d.Delete(clubID)).Returns(clubs);

            var expectedModel = new Clubs()
            {
                ClubId = clubs.ClubId,
                ClubName = clubs.ClubName,
                ClubAddress = clubs.ClubAddress,
                ClubBudget = clubs.ClubBudget,
                ClubDesc = clubs.ClubDesc
            };

            var actual = _controller.Delete(clubID);

            var actualModel = actual.Model as Clubs;

            Assert.Equal(expectedModel.ClubId, actualModel.ClubId);
            Assert.Equal(expectedModel.ClubName, actualModel.ClubName);
            Assert.Equal(expectedModel.ClubAddress, actualModel.ClubAddress);
            Assert.Equal(expectedModel.ClubBudget, actualModel.ClubBudget);
            Assert.Equal(expectedModel.ClubDesc, actualModel.ClubDesc);
        }


        [Fact]
        public void DeleteConfirmed_Club_Should_Delete()
        {
            var clubs = new Clubs()
            {
                ClubId = 3,
                ClubName = "Red Star",
                ClubAddress = "Maraka",
                ClubBudget = 140000,
                ClubDesc = "Fudbalski klub Crvena zvezda, which translates into English as simply Red Star, is a Serbian professional football club based in Belgrade commonly known elsewhere as Red Star Belgrade, the major part of the Red Star multi-sport club."
            };

            _mockRepo.Setup(d => d.DeleteConfirmed(clubs));

            _controller.DeleteConfirmed(clubs);

            _mockRepo.Verify(d => d.DeleteConfirmed(It.IsAny<Clubs>()), Times.Once);
        }


        [Fact]
        public void DeleteConfirmed_Club_ReturnToIndexAction()
        {
            var clubs = new Clubs()
            {
                ClubId = 3,
                ClubName = "Red Star",
                ClubAddress = "Maraka",
                ClubBudget = 140000,
                ClubDesc = "Fudbalski klub Crvena zvezda, which translates into English as simply Red Star, is a Serbian professional football club based in Belgrade commonly known elsewhere as Red Star Belgrade, the major part of the Red Star multi-sport club."
            };

            var result = _controller.DeleteConfirmed(clubs);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Details_Club_ReturnClubID()
        {
            int clubID = 3;

            var clubs = new Clubs()
            {
                ClubId = 5,
                ClubName = "Red Star",
                ClubAddress = "Marakana",
                ClubBudget = 140000,
                ClubDesc = "Fudbalski klub Crvena zvezda, which translates into English as simply Red Star, is a Serbian professional football club based in Belgrade commonly known elsewhere as Red Star Belgrade, the major part of the Red Star multi-sport club."
            };

            _mockRepo.Setup(d => d.Details(clubID)).Returns(clubs);

            var expectedModel = new Clubs()
            {
                ClubId = clubs.ClubId,
                ClubName = clubs.ClubName,
                ClubAddress = clubs.ClubAddress,
                ClubBudget = clubs.ClubBudget,
                ClubDesc = clubs.ClubDesc
            };

            var actual = _controller.Details(clubID);

            var actualModel = actual.Model as Clubs;

            Assert.Equal(expectedModel.ClubId, actualModel.ClubId);
            Assert.Equal(expectedModel.ClubName, actualModel.ClubName);
            Assert.Equal(expectedModel.ClubAddress, actualModel.ClubAddress);
            Assert.Equal(expectedModel.ClubBudget, actualModel.ClubBudget);
            Assert.Equal(expectedModel.ClubDesc, actualModel.ClubDesc);
        }


            








            






           



    }
}

                
