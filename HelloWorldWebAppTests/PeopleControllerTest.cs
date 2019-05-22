using System.Collections.Generic;
using HelloWorldWebApp.Controllers;
using HelloWorldWebApp.Models;
using HelloWorldWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HelloWorldWebAppTests
{
    public class PeopleControllerTest
    {
        [Fact]
        public void GetNamesInServer_Calls_RepoAndMessageBuilder()
        {
            var personList = new List<Person>
            {
                new Person("Anton"),
                new Person("Deb")
            };
            var mockRepo = new Mock<IPeopleRepository>();
            var mockMessageBuilder = new Mock<IMessageBuilder>();
            mockRepo.Setup(repo => repo.GetPeopleList()).Returns(personList);
            mockMessageBuilder.Setup(builder => builder.GetPeopleInServerAsString(personList)).Returns("Anton, Deb");
            var controller = new PeopleController(mockRepo.Object, mockMessageBuilder.Object);

            controller.GetNamesInServer();

            mockRepo.Verify(repo => repo.GetPeopleList());
            mockMessageBuilder.Verify(builder => builder.GetPeopleInServerAsString(personList));
        }

        [Fact]
        public void AddPersonToWorld_Calls_AddPersonToRepository_WhenNameDoesNotExist()
        {
            var testPerson = new Person("Anton");
            var mockRepo = new Mock<IPeopleRepository>();
            mockRepo.Setup(repo => repo.CheckIfNameExistsInRepository(testPerson.Name)).Returns(false);
            mockRepo.Setup(repo => repo.AddPersonToRepository(testPerson));
            var controller = new PeopleController(mockRepo.Object, new Mock<IMessageBuilder>().Object);

            controller.AddPersonToWorld(testPerson);

            mockRepo.Verify(repo => repo.AddPersonToRepository(testPerson));
        }

        [Fact]
        public void AddPersonToWorld_Returns201_WhenNameDoesNotExistInRepository()
        {
            var testPerson = new Person("Anton");
            var mockRepo = new Mock<IPeopleRepository>();
            mockRepo.Setup(repo => repo.CheckIfNameExistsInRepository(testPerson.Name)).Returns(false);
            mockRepo.Setup(repo => repo.AddPersonToRepository(testPerson));
            var controller = new PeopleController(mockRepo.Object, new Mock<IMessageBuilder>().Object);

            IActionResult result = controller.AddPersonToWorld(testPerson);
            var goodRequest = result as CreatedAtActionResult;

            Assert.Equal(201, goodRequest.StatusCode);
        }

        [Fact]
        public void AddPersonToWorld_DoesNotCall_AddPersonToRepository_WhenNameExists()
        {
            var testPerson = new Person("Anton");
            var mockRepo = new Mock<IPeopleRepository>();
            mockRepo.Setup(repo => repo.CheckIfNameExistsInRepository(testPerson.Name)).Returns(true);
            mockRepo.Setup(repo => repo.AddPersonToRepository(testPerson));
            var controller = new PeopleController(mockRepo.Object, new Mock<IMessageBuilder>().Object);

            controller.AddPersonToWorld(testPerson);

            mockRepo.Verify(repo => repo.AddPersonToRepository(testPerson), Times.Never);
        }

        [Fact]
        public void AddPersonToWorld_Returns400_WhenNameExistsInRepository()
        {
            var testPerson = new Person("Anton");
            var mockRepo = new Mock<IPeopleRepository>();
            mockRepo.Setup(repo => repo.CheckIfNameExistsInRepository(testPerson.Name)).Returns(true);
            var controller = new PeopleController(mockRepo.Object, new Mock<IMessageBuilder>().Object);

            IActionResult result = controller.AddPersonToWorld(testPerson);
            var badRequest = result as BadRequestObjectResult;

            Assert.Equal(400, badRequest.StatusCode);
        }

        [Fact]
        public void AddPersonToWorld_Returns400_WhenNoPersonIsGiven()
        {
            var controller =
                new PeopleController(new Mock<IPeopleRepository>().Object, new Mock<IMessageBuilder>().Object);

            IActionResult result = controller.AddPersonToWorld(null);
            var badRequest = result as BadRequestObjectResult;

            Assert.Equal(400, badRequest.StatusCode);
        }


        [Fact]
        public void RemovePersonFromWorld_Calls_RemovePersonFromRepository_WhenNameExists()
        {
            var testPerson = new Person("John");
            var mockRepo = new Mock<IPeopleRepository>();
            mockRepo.Setup(repo => repo.CheckIfNameExistsInRepository(testPerson.Name)).Returns(true);
            mockRepo.Setup(repo => repo.RemovePersonFromRepository(testPerson));
            var controller = new PeopleController(mockRepo.Object, new Mock<IMessageBuilder>().Object);

            controller.RemovePersonFromWorld(testPerson);

            mockRepo.Verify(repo => repo.RemovePersonFromRepository(testPerson));
        }

        [Fact]
        public void RemovePersonFromWorld_Returns200_WhenNameExists()
        {
            var testPerson = new Person("John");
            var mockRepo = new Mock<IPeopleRepository>();
            mockRepo.Setup(repo => repo.CheckIfNameExistsInRepository(testPerson.Name)).Returns(true);
            mockRepo.Setup(repo => repo.RemovePersonFromRepository(testPerson));
            var controller = new PeopleController(mockRepo.Object, new Mock<IMessageBuilder>().Object);

            IActionResult result = controller.RemovePersonFromWorld(testPerson);
            var goodRequest = result as OkObjectResult;

            Assert.Equal(200, goodRequest.StatusCode);
        }

        [Fact]
        public void RemovePersonFromWorld_DoesNotCall_RemovePersonFromRepository_WhenNameDoesNotExists()
        {
            var testPerson = new Person("John");
            var mockRepo = new Mock<IPeopleRepository>();
            mockRepo.Setup(repo => repo.CheckIfNameExistsInRepository(testPerson.Name)).Returns(false);
            mockRepo.Setup(repo => repo.RemovePersonFromRepository(testPerson));
            var controller = new PeopleController(mockRepo.Object, new Mock<IMessageBuilder>().Object);

            controller.RemovePersonFromWorld(testPerson);

            mockRepo.Verify(repo => repo.RemovePersonFromRepository(testPerson), Times.Never);
        }

        [Fact]
        public void RemovePersonFromWorld_Returns404_WhenNameDoesNotExists()
        {
            var testPerson = new Person("John");
            var mockRepo = new Mock<IPeopleRepository>();
            mockRepo.Setup(repo => repo.CheckIfNameExistsInRepository(testPerson.Name)).Returns(false);
            mockRepo.Setup(repo => repo.RemovePersonFromRepository(testPerson));
            var controller = new PeopleController(mockRepo.Object, new Mock<IMessageBuilder>().Object);

            IActionResult result = controller.RemovePersonFromWorld(testPerson);
            var badRequest = result as NotFoundObjectResult;

            Assert.Equal(404, badRequest.StatusCode);
        }

        [Fact]
        public void RemovePersonFromWorld_DoesNotCall_RemovePersonFromRepository_WhenNameIsAnton()
        {
            var testPerson = new Person("Anton");
            var mockRepo = new Mock<IPeopleRepository>();
            mockRepo.Setup(repo => repo.RemovePersonFromRepository(testPerson));
            var controller = new PeopleController(mockRepo.Object, new Mock<IMessageBuilder>().Object);

            controller.RemovePersonFromWorld(testPerson);

            mockRepo.Verify(repo => repo.RemovePersonFromRepository(testPerson), Times.Never);
        }

        [Fact]
        public void RemovePersonFromWorld_Returns400_WhenNameIsAnton()
        {
            var testPerson = new Person("Anton");
            var mockRepo = new Mock<IPeopleRepository>();
            mockRepo.Setup(repo => repo.CheckIfNameExistsInRepository(testPerson.Name)).Returns(false);
            mockRepo.Setup(repo => repo.RemovePersonFromRepository(testPerson));
            var controller = new PeopleController(mockRepo.Object, new Mock<IMessageBuilder>().Object);

            IActionResult result = controller.RemovePersonFromWorld(testPerson);
            var badRequest = result as BadRequestObjectResult;

            Assert.Equal(400, badRequest.StatusCode);
        }

        [Fact]
        public void RemovePersonToWorld_ReturnsBadRequest_WhenNoPersonIsGiven()
        {
            var controller =
                new PeopleController(new Mock<IPeopleRepository>().Object, new Mock<IMessageBuilder>().Object);

            var result = controller.RemovePersonFromWorld(null);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UpdatePersonInWorld_Calls_UpdatePersonInRepository_WhenOldNameExistsInRepo_AndNewNameDoesNot()
        {
            var testRequest = new NameChangeRequest("Greg", "John");
            var mockRepo = new Mock<IPeopleRepository>();
            mockRepo.Setup(repo => repo.CheckIfNameExistsInRepository(testRequest.OldName)).Returns(true);
            mockRepo.Setup(repo => repo.CheckIfNameExistsInRepository(testRequest.NewName)).Returns(false);
            mockRepo.Setup(repo => repo.UpdatePersonInRepository(testRequest));
            var controller = new PeopleController(mockRepo.Object, new Mock<IMessageBuilder>().Object);

            controller.UpdatePersonInWorld(testRequest);

            mockRepo.Verify(repo => repo.UpdatePersonInRepository(testRequest));
        }

        [Fact]
        public void UpdatePersonInWorld_Returns200_WhenOldNameExistsInRepo_AndNewNameDoesNot()
        {
            var testRequest = new NameChangeRequest("Greg", "John");
            var mockRepo = new Mock<IPeopleRepository>();
            mockRepo.Setup(repo => repo.CheckIfNameExistsInRepository(testRequest.OldName)).Returns(true);
            mockRepo.Setup(repo => repo.CheckIfNameExistsInRepository(testRequest.NewName)).Returns(false);
            mockRepo.Setup(repo => repo.UpdatePersonInRepository(testRequest));
            var controller = new PeopleController(mockRepo.Object, new Mock<IMessageBuilder>().Object);

            IActionResult result = controller.UpdatePersonInWorld(testRequest);
            var goodRequest = result as OkObjectResult;

            Assert.Equal(200, goodRequest.StatusCode);
        }

        [Fact]
        public void UpdatePersonName_DoesNotCall_UpdatePersonInRepository_WhenNewNameDoesExistsInRepo()
        {
            var testRequest = new NameChangeRequest("Greg", "John");
            var mockRepo = new Mock<IPeopleRepository>();
            mockRepo.Setup(repo => repo.CheckIfNameExistsInRepository(testRequest.OldName)).Returns(false);
            mockRepo.Setup(repo => repo.CheckIfNameExistsInRepository(testRequest.NewName)).Returns(true);
            mockRepo.Setup(repo => repo.UpdatePersonInRepository(testRequest));
            var controller = new PeopleController(mockRepo.Object, new Mock<IMessageBuilder>().Object);

            controller.UpdatePersonInWorld(testRequest);

            mockRepo.Verify(repo => repo.UpdatePersonInRepository(testRequest), Times.Never);
        }

        [Fact]
        public void UpdatePersonName_Returns400_WhenNewNameDoesExistsInRepo()
        {
            var testRequest = new NameChangeRequest("Greg", "John");
            var mockRepo = new Mock<IPeopleRepository>();
            mockRepo.Setup(repo => repo.CheckIfNameExistsInRepository(testRequest.OldName)).Returns(false);
            mockRepo.Setup(repo => repo.CheckIfNameExistsInRepository(testRequest.NewName)).Returns(true);
            mockRepo.Setup(repo => repo.UpdatePersonInRepository(testRequest));
            var controller = new PeopleController(mockRepo.Object, new Mock<IMessageBuilder>().Object);

            IActionResult result = controller.UpdatePersonInWorld(testRequest);
            var badRequest = result as BadRequestObjectResult;

            Assert.Equal(400, badRequest.StatusCode);
        }

        [Fact]
        public void UpdatePersonName_DoesNotCall_UpdatePersonInRepository_WhenOldNameDoesNotExistInRepo()
        {
            var testRequest = new NameChangeRequest("Greg", "John");
            var mockRepo = new Mock<IPeopleRepository>();
            mockRepo.Setup(repo => repo.CheckIfNameExistsInRepository(testRequest.OldName)).Returns(false);
            mockRepo.Setup(repo => repo.CheckIfNameExistsInRepository(testRequest.NewName)).Returns(false);
            mockRepo.Setup(repo => repo.UpdatePersonInRepository(testRequest));
            var controller = new PeopleController(mockRepo.Object, new Mock<IMessageBuilder>().Object);

            controller.UpdatePersonInWorld(testRequest);

            mockRepo.Verify(repo => repo.UpdatePersonInRepository(testRequest), Times.Never);
        }

        [Fact]
        public void UpdatePersonName_Returns400_WhenOldNameDoesNotExistInRepo()
        {
            var testRequest = new NameChangeRequest("Greg", "John");
            var mockRepo = new Mock<IPeopleRepository>();
            mockRepo.Setup(repo => repo.CheckIfNameExistsInRepository(testRequest.OldName)).Returns(false);
            mockRepo.Setup(repo => repo.CheckIfNameExistsInRepository(testRequest.NewName)).Returns(false);
            mockRepo.Setup(repo => repo.UpdatePersonInRepository(testRequest));
            var controller = new PeopleController(mockRepo.Object, new Mock<IMessageBuilder>().Object);

            IActionResult result = controller.UpdatePersonInWorld(testRequest);
            var badRequest = result as BadRequestObjectResult;

            Assert.Equal(400, badRequest.StatusCode);
        }

        [Fact]
        public void UpdatePersonName_DoesNotCall_UpdatePersonInRepository_WhenOldNameIsOwner()
        {
            var testRequest = new NameChangeRequest("Anton", "John");
            var mockRepo = new Mock<IPeopleRepository>();
            mockRepo.Setup(repo => repo.UpdatePersonInRepository(testRequest));
            var controller = new PeopleController(mockRepo.Object, new Mock<IMessageBuilder>().Object);

            controller.UpdatePersonInWorld(testRequest);

            mockRepo.Verify(repo => repo.UpdatePersonInRepository(testRequest), Times.Never);
        }

        [Fact]
        public void UpdatePersonName_Returns400_WhenOldNameIsOwner()
        {
            var testRequest = new NameChangeRequest("Anton", "John");
            var mockRepo = new Mock<IPeopleRepository>();
            mockRepo.Setup(repo => repo.UpdatePersonInRepository(testRequest));
            var controller = new PeopleController(mockRepo.Object, new Mock<IMessageBuilder>().Object);

            controller.UpdatePersonInWorld(testRequest);

            IActionResult result = controller.UpdatePersonInWorld(testRequest);
            var badRequest = result as BadRequestObjectResult;

            Assert.Equal(400, badRequest.StatusCode);
        }

        [Fact]
        public void UpdatePersonName_ReturnsBadRequest_WhenNoRequestIsGiven()
        {
            var controller =
                new PeopleController(new Mock<IPeopleRepository>().Object, new Mock<IMessageBuilder>().Object);

            var result = controller.UpdatePersonInWorld(null);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}