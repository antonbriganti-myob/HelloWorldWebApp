using System;
using System.Collections.Generic;
using HelloWorldWebApp.Models;
using HelloWorldWebApp.Services;
using Moq;
using Xunit;

namespace HelloWorldWebAppTests
{
    public class MessageBuilderTest
    {

        private readonly MessageBuilder _messageBuilder;
        public MessageBuilderTest()
        {
            var dateTimeMock = new Mock<IDateTime>();
            dateTimeMock.Setup(dateTime => dateTime.GetCurrentTimeAndDate()).Returns("10:48pm on 14 March 2018");
            _messageBuilder = new MessageBuilder(dateTimeMock.Object);

        }

        [Fact]
        public void GetPeopleInServerAsString_ReturnsSingleName()
        {
            // Given
            List<Person> NameList = new List<Person>
            {
                new Person("Anton")
            };

            // When
            string actualMessage = _messageBuilder.CreateGetTimeMessage(NameList);
            string expectedMessage = "Hello Anton - the time on the server is 10:48pm on 14 March 2018";


            // Then
            Assert.Equal(expectedMessage, actualMessage);
        }
    }
}
