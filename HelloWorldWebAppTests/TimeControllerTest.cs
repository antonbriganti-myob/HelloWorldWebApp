using System;
using Xunit;
using Moq;
using HelloWorldWebApp.Data;
using System.Collections.Generic;
using HelloWorldWebApp.Models;
using HelloWorldWebApp.Services;
using HelloWorldWebApp.Controllers;
using HelloWorldWebAppTests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public void AddPersonToWorld_Calls_AddPersonToContext_WhenNameDoesntExist()
        {
            // given
            var mockMessageBuilder = new Mock<IMessageBuilder>(); 
            var testPerson = new Person("Anton");
            var mockRepo = new Mock<IPeopleRepository>();
            mockRepo.Setup(repo => repo.CheckIfNameExistsInWorld(testPerson.Name)).Returns(false);
            mockRepo.Setup(repo => repo.AddPersonToContext(testPerson));
    
            var controller = new TimeController(mockRepo.Object, mockMessageBuilder.Object);
            // when 
            controller.AddPersonToWorld(testPerson);
            
            // then
            mockRepo.Verify(repo => repo.AddPersonToContext(testPerson));
        }
        
        [Fact]
        public void AddPersonToWorld_DoesntCalls_AddPersonToContext_WhenNameExists()
        {
            // given
            var mockMessageBuilder = new Mock<IMessageBuilder>(); 
            var testPerson = new Person("Anton");
            var mockRepo = new Mock<IPeopleRepository>();
            mockRepo.Setup(repo => repo.CheckIfNameExistsInWorld(testPerson.Name)).Returns(true);
            mockRepo.Setup(repo => repo.AddPersonToContext(testPerson));
    
            var controller = new TimeController(mockRepo.Object, mockMessageBuilder.Object);
            // when 
            controller.AddPersonToWorld(testPerson);
            
            // then

            mockRepo.Verify(repo => repo.AddPersonToContext(testPerson), Times.Never);
        }
        
        [Fact]
        public void RemovePersonFromWorld_Calls_RemovePersonFromWorld_WhenNameExists()
        {
            // given
            var mockMessageBuilder = new Mock<IMessageBuilder>(); 
            var testPerson = new Person("John");
            var mockRepo = new Mock<IPeopleRepository>();
            mockRepo.Setup(repo => repo.CheckIfNameExistsInWorld(testPerson.Name)).Returns(true);
            mockRepo.Setup(repo => repo.RemovePersonFromWorld(testPerson));
    
            var controller = new TimeController(mockRepo.Object, mockMessageBuilder.Object);
            // when 
            controller.RemovePersonFromWorld(testPerson);
            
            // then

            mockRepo.Verify(repo => repo.RemovePersonFromWorld(testPerson));
        }
        
        [Fact]
        public void RemovePersonFromWorld_DoesntCall_RemovePersonFromWorld_WhenNameDoesntExists()
        {
            // given
            var mockMessageBuilder = new Mock<IMessageBuilder>(); 
            var testPerson = new Person("John");
            var mockRepo = new Mock<IPeopleRepository>();
            mockRepo.Setup(repo => repo.CheckIfNameExistsInWorld(testPerson.Name)).Returns(false);
            mockRepo.Setup(repo => repo.RemovePersonFromWorld(testPerson));
    
            var controller = new TimeController(mockRepo.Object, mockMessageBuilder.Object);
            // when 
            controller.RemovePersonFromWorld(testPerson);
            
            // then

            mockRepo.Verify(repo => repo.RemovePersonFromWorld(testPerson), Times.Never);
        }
        
        [Fact]
        public void RemovePersonFromWorld_DoesntCall_CheckIfNameExistsInWorld_WhenNameIsAnton()
        {
            // given
            var mockMessageBuilder = new Mock<IMessageBuilder>(); 
            var testPerson = new Person("Anton");
            var mockRepo = new Mock<IPeopleRepository>();
            mockRepo.Setup(repo => repo.CheckIfNameExistsInWorld(testPerson.Name)).Returns(false);

            var controller = new TimeController(mockRepo.Object, mockMessageBuilder.Object);
            // when 
            controller.RemovePersonFromWorld(testPerson);
            
            // then
            mockRepo.Verify(repo => repo.CheckIfNameExistsInWorld(testPerson.Name), Times.Never);
        }
        
    }
}
