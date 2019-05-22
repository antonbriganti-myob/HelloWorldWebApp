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
            var message = "Hello Anton and Deb - the time on the server is 2:32pm on 16 May 2019";
            mockMessageBuilder.Setup(builder => builder.CreateGreetingWithTimeMessage(personList)).Returns(message);
            var controller = new TimeController(mockRepo.Object, mockMessageBuilder.Object);

            controller.GetGreetingWithTime();

            mockRepo.Verify(repo => repo.GetPeopleList());
            mockMessageBuilder.Verify(builder => builder.CreateGreetingWithTimeMessage(personList));
        }

        [Fact]
        public void GetGreetingWithTime_Returns_ExpectedMessage()
        {
            var personList = new List<Person>
            {
                new Person("Anton"),
                new Person("Deb")
            };
            var mockRepo = new Mock<IPeopleRepository>();
            var mockMessageBuilder = new Mock<IMessageBuilder>();
            mockRepo.Setup(repo => repo.GetPeopleList()).Returns(personList);
            var message = "Hello Anton and Deb - the time on the server is 2:32pm on 16 May 2019";
            mockMessageBuilder.Setup(builder => builder.CreateGreetingWithTimeMessage(personList)).Returns(message);
            var controller = new TimeController(mockRepo.Object, mockMessageBuilder.Object);

            var result = controller.GetGreetingWithTime();


            Assert.Equal(message, result.Value);
        }
    }
}