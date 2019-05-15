using System.Collections.Generic;
using HelloWorldWebApp.Controllers;
using HelloWorldWebApp.Models;
using HelloWorldWebApp.Services;
using Moq;
using Xunit;

namespace HelloWorldWebAppTests
{
    public class TimeControllerTest
    {
        [Fact]
        public void AddPersonToWorld_Calls_AddPersonToRepository_WhenNameDoesNotExist()
        {
            var testPerson = new Person("Anton");
            var mockRepo = new Mock<IPeopleRepository>();
            mockRepo.Setup(repo => repo.CheckIfNameExistsInRepository(testPerson.Name)).Returns(false);
            mockRepo.Setup(repo => repo.AddPersonToRepository(testPerson));
            var controller = new TimeController(mockRepo.Object, new Mock<IMessageBuilder>().Object);

            controller.AddPersonToWorld(testPerson);

            mockRepo.Verify(repo => repo.AddPersonToRepository(testPerson));
        }

        [Fact]
        public void AddPersonToWorld_DoesNotCall_AddPersonToRepository_WhenNameExists()
        {
            var testPerson = new Person("Anton");
            var mockRepo = new Mock<IPeopleRepository>();
            mockRepo.Setup(repo => repo.CheckIfNameExistsInRepository(testPerson.Name)).Returns(true);
            mockRepo.Setup(repo => repo.AddPersonToRepository(testPerson));
            var controller = new TimeController(mockRepo.Object, new Mock<IMessageBuilder>().Object);

            controller.AddPersonToWorld(testPerson);

            mockRepo.Verify(repo => repo.AddPersonToRepository(testPerson), Times.Never);
        }


        [Fact]
        public void GetGreetingWithTime_Calls_RepoAndMessageBuilder()
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
            var controller = new TimeController(mockRepo.Object, mockMessageBuilder.Object);

            controller.GetGreetingWithTime();

            mockRepo.Verify(repo => repo.GetPeopleList());
            mockMessageBuilder.Verify(builder => builder.CreateGreetingWithTimeMessage(personList));
        }

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
            var controller = new TimeController(mockRepo.Object, mockMessageBuilder.Object);

            controller.GetNamesInServer();

            mockRepo.Verify(repo => repo.GetPeopleList());
            mockMessageBuilder.Verify(builder => builder.GetPeopleInServerAsString(personList));
        }


        [Fact]
        public void RemovePersonFromWorld_Calls_RemovePersonFromRepository_WhenNameExists()
        {
            var testPerson = new Person("John");
            var mockRepo = new Mock<IPeopleRepository>();
            mockRepo.Setup(repo => repo.CheckIfNameExistsInRepository(testPerson.Name)).Returns(true);
            mockRepo.Setup(repo => repo.RemovePersonFromRepository(testPerson));
            var controller = new TimeController(mockRepo.Object, new Mock<IMessageBuilder>().Object);

            controller.RemovePersonFromWorld(testPerson);

            mockRepo.Verify(repo => repo.RemovePersonFromRepository(testPerson));
        }

        [Fact]
        public void RemovePersonFromWorld_DoesNotCall_RemovePersonFromRepository_WhenNameDoesNotExists()
        {
            var testPerson = new Person("John");
            var mockRepo = new Mock<IPeopleRepository>();
            mockRepo.Setup(repo => repo.CheckIfNameExistsInRepository(testPerson.Name)).Returns(false);
            mockRepo.Setup(repo => repo.RemovePersonFromRepository(testPerson));
            var controller = new TimeController(mockRepo.Object, new Mock<IMessageBuilder>().Object);

            controller.RemovePersonFromWorld(testPerson);

            mockRepo.Verify(repo => repo.RemovePersonFromRepository(testPerson), Times.Never);
        }

        [Fact]
        public void RemovePersonFromWorld_DoesNotCall_RemovePersonFromRepository_WhenNameIsAnton()
        {
            var testPerson = new Person("Anton");
            var mockRepo = new Mock<IPeopleRepository>();
            mockRepo.Setup(repo => repo.RemovePersonFromRepository(testPerson));
            var controller = new TimeController(mockRepo.Object, new Mock<IMessageBuilder>().Object);

            controller.RemovePersonFromWorld(testPerson);

            mockRepo.Verify(repo => repo.RemovePersonFromRepository(testPerson), Times.Never);
        }

        [Fact]
        public void UpdatePersonInWorld_Calls_UpdatePersonInRepository_WhenOldNameExistsInRepo_AndNewNameDoesNot()
        {
            var testRequest = new NameChangeRequest("Greg", "John");
            var mockRepo = new Mock<IPeopleRepository>();
            mockRepo.Setup(repo => repo.CheckIfNameExistsInRepository(testRequest.OldName)).Returns(true);
            mockRepo.Setup(repo => repo.CheckIfNameExistsInRepository(testRequest.NewName)).Returns(false);
            mockRepo.Setup(repo => repo.UpdatePersonInRepository(testRequest));
            var controller = new TimeController(mockRepo.Object, new Mock<IMessageBuilder>().Object);

            controller.UpdatePersonInWorld(testRequest);

            mockRepo.Verify(repo => repo.UpdatePersonInRepository(testRequest));
        }

        [Fact]
        public void UpdatePersonName_DoesNotCall_UpdatePersonInRepository_WhenNewNameDoesExistsInRepo()
        {
            var testRequest = new NameChangeRequest("Greg", "John");
            var mockRepo = new Mock<IPeopleRepository>();
            mockRepo.Setup(repo => repo.CheckIfNameExistsInRepository(testRequest.OldName)).Returns(false);
            mockRepo.Setup(repo => repo.CheckIfNameExistsInRepository(testRequest.NewName)).Returns(true);
            mockRepo.Setup(repo => repo.UpdatePersonInRepository(testRequest));
            var controller = new TimeController(mockRepo.Object, new Mock<IMessageBuilder>().Object);

            controller.UpdatePersonInWorld(testRequest);

            mockRepo.Verify(repo => repo.UpdatePersonInRepository(testRequest), Times.Never);
        }

        [Fact]
        public void UpdatePersonName_DoesNotCall_UpdatePersonInRepository_WhenOldNameDoesNotExistInRepo()
        {
            var testRequest = new NameChangeRequest("Greg", "John");
            var mockRepo = new Mock<IPeopleRepository>();
            mockRepo.Setup(repo => repo.CheckIfNameExistsInRepository(testRequest.OldName)).Returns(false);
            mockRepo.Setup(repo => repo.CheckIfNameExistsInRepository(testRequest.NewName)).Returns(false);
            mockRepo.Setup(repo => repo.UpdatePersonInRepository(testRequest));
            var controller = new TimeController(mockRepo.Object, new Mock<IMessageBuilder>().Object);

            controller.UpdatePersonInWorld(testRequest);

            mockRepo.Verify(repo => repo.UpdatePersonInRepository(testRequest), Times.Never);
        }

        [Fact]
        public void UpdatePersonName_DoesNotCall_UpdatePersonInRepository_WhenOldNameIsOwner()
        {
            var testRequest = new NameChangeRequest("Anton", "John");
            var mockRepo = new Mock<IPeopleRepository>();
            mockRepo.Setup(repo => repo.UpdatePersonInRepository(testRequest));
            var controller = new TimeController(mockRepo.Object, new Mock<IMessageBuilder>().Object);

            controller.UpdatePersonInWorld(testRequest);

            mockRepo.Verify(repo => repo.UpdatePersonInRepository(testRequest), Times.Never);
        }
    }
}