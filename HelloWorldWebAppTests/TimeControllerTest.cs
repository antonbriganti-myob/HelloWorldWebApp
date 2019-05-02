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
        public void AddPersonToWorld()
        {
            
            // todo: discuss best way to test this
            // based off test response? BadRequest is called when successful, context.People.Add isn't called add
            
            // given
  

            // when 
           

            // then

        }
    }
}
