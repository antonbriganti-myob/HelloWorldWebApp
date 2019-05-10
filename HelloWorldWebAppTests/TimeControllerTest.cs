using Xunit;
using Moq;
using System.Collections.Generic;
using HelloWorldWebApp.Models;
using HelloWorldWebApp.Services;
using HelloWorldWebApp.Controllers;

namespace HelloWorldWebAppTests
{
    public class TimeControllerTest
    {
        
        [Fact]
        public void GetGreetingWithTime_Calls_RepoAndMessageBuilder()
        {
            
            // test if MessageBuilder.CreateGetTimeMessage has been called 
            // given
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
            

            // when 
            controller.GetGreetingWithTime();


            // then
            mockRepo.Verify(repo => repo.GetPeopleList());
            mockMessageBuilder.Verify(builder => builder.CreateGreetingWithTimeMessage(personList));
        }
        
        [Fact]
        public void GetNamesInServer_Calls_RepoAndMessageBuilder()
        {
            // given
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
            
            // when 
            controller.GetNamesInServer();

            // then
            mockRepo.Verify(repo => repo.GetPeopleList(), Times.Once);
            mockMessageBuilder.Verify(builder => builder.GetPeopleInServerAsString(personList), Times.Once);
        }
        
        
        [Fact]
        public void AddPersonToWorld_Calls_AddPersonToDataStore_WhenNameDoesNotExist()
        {
            // given
            var testPerson = new Person("Anton");
            var mockRepo = new Mock<IPeopleRepository>();
            mockRepo.Setup(repo => repo.CheckIfNameExistsInDataStore(testPerson.Name)).Returns(false);
            mockRepo.Setup(repo => repo.AddPersonToDataStore(testPerson));
    
            var controller = new TimeController(mockRepo.Object, new Mock<IMessageBuilder>().Object);
            // when 
            controller.AddPersonToWorld(testPerson);
            
            // then
            mockRepo.Verify(repo => repo.AddPersonToDataStore(testPerson));
        }
        
        [Fact]
        public void AddPersonToWorld_DoesNotCall_AddPersonToDataStore_WhenNameExists()
        {
            // given
            var testPerson = new Person("Anton");
            var mockRepo = new Mock<IPeopleRepository>();
            mockRepo.Setup(repo => repo.CheckIfNameExistsInDataStore(testPerson.Name)).Returns(true);
            mockRepo.Setup(repo => repo.AddPersonToDataStore(testPerson));
    
            var controller = new TimeController(mockRepo.Object, new Mock<IMessageBuilder>().Object);
            // when 
            controller.AddPersonToWorld(testPerson);
            
            // then

            mockRepo.Verify(repo => repo.AddPersonToDataStore(testPerson), Times.Never);
        }
        
        [Fact]
        public void RemovePersonFromWorld_Calls_RemovePersonFromDataStore_WhenNameExists()
        {
            // given
            var testPerson = new Person("John");
            var mockRepo = new Mock<IPeopleRepository>();
            mockRepo.Setup(repo => repo.CheckIfNameExistsInDataStore(testPerson.Name)).Returns(true);
            mockRepo.Setup(repo => repo.RemovePersonFromDataStore(testPerson));
    
            var controller = new TimeController(mockRepo.Object, new Mock<IMessageBuilder>().Object);
           
            // when 
            controller.RemovePersonFromWorld(testPerson);
            
            // then

            mockRepo.Verify(repo => repo.RemovePersonFromDataStore(testPerson));
        }
        
        [Fact]
        public void RemovePersonFromWorld_DoesNotCall_RemovePersonFromDataStore_WhenNameDoesNotExists()
        {
            // given
            var testPerson = new Person("John");
            var mockRepo = new Mock<IPeopleRepository>();
            mockRepo.Setup(repo => repo.CheckIfNameExistsInDataStore(testPerson.Name)).Returns(false);
            mockRepo.Setup(repo => repo.RemovePersonFromDataStore(testPerson));
    
            var controller = new TimeController(mockRepo.Object, new Mock<IMessageBuilder>().Object);
          
            // when 
            controller.RemovePersonFromWorld(testPerson);
            
            // then
            mockRepo.Verify(repo => repo.RemovePersonFromDataStore(testPerson), Times.Never);
        }
        
        [Fact]
        public void RemovePersonFromWorld_DoesNotCall_RemovePersonFromDataStore_WhenNameIsAnton()
        {
            // given
            var testPerson = new Person("Anton");
            var mockRepo = new Mock<IPeopleRepository>();
            mockRepo.Setup(repo => repo.RemovePersonFromDataStore(testPerson));

            var controller = new TimeController(mockRepo.Object, new Mock<IMessageBuilder>().Object);
            // when 
            controller.RemovePersonFromWorld(testPerson);
            
            // then
            mockRepo.Verify(repo => repo.RemovePersonFromDataStore(testPerson), Times.Never);
        }
        
        [Fact]
        public void UpdatePersonInWorld_Calls_UpdatePersonInDataStore_WhenOldNameExistsInRepo_AndNewNameDoesNot()
        {
            // given
            var testRequest = new NameChangeRequest("Greg", "John");
            var mockRepo = new Mock<IPeopleRepository>();
            
            mockRepo.Setup(repo => repo.CheckIfNameExistsInDataStore(testRequest.OldName)).Returns(true);
            mockRepo.Setup(repo => repo.CheckIfNameExistsInDataStore(testRequest.NewName)).Returns(false);
            mockRepo.Setup(repo => repo.UpdatePersonInDataStore(testRequest));
            var controller = new TimeController(mockRepo.Object, new Mock<IMessageBuilder>().Object);
            
            // when 
            controller.UpdatePersonInWorld(testRequest);
            
            // then
            mockRepo.Verify(repo => repo.UpdatePersonInDataStore(testRequest));
        }
        
        [Fact]
        public void UpdatePersonName_DoesNotCall_UpdatePersonInDataStore_WhenOldNameDoesNotExistInRepo()
        {
            // given
            var testRequest = new NameChangeRequest("Greg", "John");
            var mockRepo = new Mock<IPeopleRepository>();
            
            mockRepo.Setup(repo => repo.CheckIfNameExistsInDataStore(testRequest.OldName)).Returns(false);
            mockRepo.Setup(repo => repo.CheckIfNameExistsInDataStore(testRequest.NewName)).Returns(false);
            mockRepo.Setup(repo => repo.UpdatePersonInDataStore(testRequest));
            var controller = new TimeController(mockRepo.Object, new Mock<IMessageBuilder>().Object);
            
            // when 
            controller.UpdatePersonInWorld(testRequest);
            
            // then
            mockRepo.Verify(repo => repo.UpdatePersonInDataStore(testRequest), Times.Never);
        }
        
        [Fact]
        public void UpdatePersonName_DoesNotCall_UpdatePersonInDataStore_WhenNewNameDoesExistsInRepo()
        {
            // given
            var testRequest = new NameChangeRequest("Greg", "John");
            var mockRepo = new Mock<IPeopleRepository>();
            
            mockRepo.Setup(repo => repo.CheckIfNameExistsInDataStore(testRequest.OldName)).Returns(false);
            mockRepo.Setup(repo => repo.CheckIfNameExistsInDataStore(testRequest.NewName)).Returns(true);
            mockRepo.Setup(repo => repo.UpdatePersonInDataStore(testRequest));
            var controller = new TimeController(mockRepo.Object, new Mock<IMessageBuilder>().Object);
            
            // when 
            controller.UpdatePersonInWorld(testRequest);
            
            // then
            mockRepo.Verify(repo => repo.UpdatePersonInDataStore(testRequest), Times.Never);
        }
        
        [Fact]
        public void UpdatePersonName_DoesNotCall_UpdatePersonInDataStore_WhenOldNameIsOwner()
        {
            // given
            var testRequest = new NameChangeRequest("Anton", "John");
            var mockRepo = new Mock<IPeopleRepository>();
            
            mockRepo.Setup(repo => repo.UpdatePersonInDataStore(testRequest));
            var controller = new TimeController(mockRepo.Object, new Mock<IMessageBuilder>().Object);
            
            // when 
            controller.UpdatePersonInWorld(testRequest);
            
            // then
            mockRepo.Verify(repo => repo.UpdatePersonInDataStore(testRequest), Times.Never);
        }
    }
}
