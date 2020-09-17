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
    public class GroupTest
    {
        private Mock<IGroupRepo> _mockRepo;
        private GroupsController _controller;

        public GroupTest()
        {
            _mockRepo = new Mock<IGroupRepo>();
            _controller = new GroupsController(_mockRepo.Object);
        }


        // Should return to Index View
        [Fact]
        public void Index_Group_ShouldReturnView()
        {
            var result = _controller.Index();

            Assert.IsAssignableFrom<IActionResult>(result);
        }
        // CREATE Tests
        //Should return Create View
        [Fact]
        public void Create_Group_ShouldReturnView()
        {
            var result = _controller.Create();

            Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void Create_Group_RedirectToIndexAction()
        {
            var groups = new Groups()
            {
                GroupId = 3,
                GroupName = "C",
                GroupLength = 6
            };

            var result = _controller.Create(groups);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Create_Group_InvalidModel_ReturnView()
        {
            _controller.ModelState.AddModelError("GroupName", "Enter group name");
            //_controller.ModelState.AddModelError("GropLength", "Enter group length");

            var groups = new Groups()
            {
                GroupName = "C"
            };

            var result = _controller.Create(groups);

            _mockRepo.Verify(c => c.Create(It.IsAny<Groups>()), Times.Never);

        }
        [Fact]
        public void Create_Group_ValidModel_ReturnView()
        {
            Groups grp = null;

            _mockRepo.Setup(c => c.Create(It.IsAny<Groups>()))
            .Callback<Groups>(g => grp = g);

            var groups = new Groups()
            {
                GroupId = 3,
                GroupName = "C",
                GroupLength = 6,

            };

            _controller.Create(groups);

            _mockRepo.Verify(c => c.Create(It.IsAny<Groups>()), Times.Once);

            Assert.Equal(grp.GroupId, groups.GroupId);
            Assert.Equal(grp.GroupName, groups.GroupName);
            Assert.Equal(grp.GroupLength, groups.GroupLength);
        }


        // EDIT Tests
        [Fact]
        public void Edit_Group_RedirectToIndexAction()
        {
            var groups = new Groups()
            {
                GroupName = "D"
            };

            var result = _controller.Edit(groups);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Edit_Group_ReturnGroupID()
        {
            int? groupID = 3;
            

            var groups = new Groups()
            {
                GroupId = 5,
                GroupName = "D",
                GroupLength = 5
            };

            _mockRepo.Setup(e => e.FindElementById(groupID)).Returns(groups);

            var expectedModel = new Groups()
            {
                GroupId = groups.GroupId,
                GroupName = groups.GroupName,
                GroupLength = groups.GroupLength
            };


            var actual = _controller.Edit(groupID);

            var actualModel = actual.Model as Groups;

            Assert.Equal(expectedModel.GroupId, actualModel.GroupId);
            Assert.Equal(expectedModel.GroupName, actualModel.GroupName);
            Assert.Equal(expectedModel.GroupLength, actualModel.GroupLength);
        }


        [Fact]
        public void Edit_Group_InvalidModel()
        {
            _controller.ModelState.AddModelError("GroupName", "Enter group name");

            var groups = new Groups()
            {
                GroupLength = 8
            };

            _controller.Edit(groups);

            _mockRepo.Verify(e => e.Edit(It.IsAny<Groups>()), Times.Never);
        }

        [Fact]
        public void Edit_Group_ValidModel()
        {
            Groups grp = null;

            _mockRepo.Setup(e => e.Edit(It.IsAny<Groups>()))
                .Callback<Groups>(g => grp = g);

            var groups = new Groups()
            {
                GroupId = 5,
                GroupName = "C",
                GroupLength = 6
            };

            _controller.Edit(groups);
            _mockRepo.Verify(e => e.Edit(It.IsAny<Groups>()), Times.Once);

            Assert.Equal(grp.GroupId, groups.GroupId);
            Assert.Equal(grp.GroupName, groups.GroupName);
            Assert.Equal(grp.GroupLength, groups.GroupLength);
        }
        // Delete
        [Fact]
        public void Delete_Group_ReturnGroupID()
        {
            int? groupID = 5;

            var groups = new Groups()
            {
                GroupId = 5,
                GroupName = "C",
                GroupLength = 6
            };

            _mockRepo.Setup(d => d.Delete(groupID)).Returns(groups);

            var expectedModel = new Groups()
            {
                GroupId = groups.GroupId,
                GroupName = groups.GroupName,
                GroupLength = groups.GroupLength
            };

            var actual = _controller.Delete(groupID);

            var actualModel = actual.Model as Groups;

            Assert.Equal(expectedModel.GroupId, actualModel.GroupId);
            Assert.Equal(expectedModel.GroupName, actualModel.GroupName);
            Assert.Equal(expectedModel.GroupLength, actualModel.GroupLength);
        }

        [Fact]
        public void DeleteConfirmed_Group_Should_Delete()
        {
            var groups = new Groups()
            {
                GroupId = 3,
                GroupName = "C",
                GroupLength = 6
            };


            _mockRepo.Setup(d => d.DeleteConfirmed(groups));

            _controller.DeleteConfirmed(groups);

            _mockRepo.Verify(d => d.DeleteConfirmed(It.IsAny<Groups>()), Times.Once);
        }

        [Fact]
        public void DeleteConfirmed_Group_RedirectToIndexAction()
        {
            var groups = new Groups()
            {
                GroupId = 3,
                GroupName = "C",
                GroupLength = 6
            };

            var result = _controller.DeleteConfirmed(groups);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void Details_Group_ReturnGroupID()
        {
            int? groupID = 5;

            var groups = new Groups()
            {
                GroupId = 3,
                GroupName = "C",
                GroupLength = 6
            };

            _mockRepo.Setup(d => d.Details(groupID)).Returns(groups);

            var expectedModel = new Groups()
            {
                GroupId = groups.GroupId,
                GroupName = groups.GroupName,
                GroupLength = groups.GroupLength
            };

            var actual = _controller.Details(groupID);

            var actualModel = actual.Model as Groups;

            Assert.Equal(expectedModel.GroupId, actualModel.GroupId);
            Assert.Equal(expectedModel.GroupName, actualModel.GroupName);
            Assert.Equal(expectedModel.GroupLength, actualModel.GroupLength);
        }











    }
}
