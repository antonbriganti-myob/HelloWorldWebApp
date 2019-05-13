using System.Collections.Generic;
using HelloWorldWebApp.Models;
using HelloWorldWebApp.Services;
using HelloWorldWebAppTests.Helpers;
using Xunit;

namespace HelloWorldWebAppTests
{
    public class MessageBuilderTest
    {
        public MessageBuilderTest()
        {
            var dateTimeMock = DateTimeHelper.CreateMockDateTime();
            _messageBuilder = new ActualMessageBuilder(dateTimeMock.Object);
        }

        private readonly ActualMessageBuilder _messageBuilder;

        [Fact]
        public void CreateFormattedMessageOfPeopleInServer_ReturnsSingleName_WhenGivenSinglePerson()
        {
            var nameList = new List<Person>
            {
                new Person("Anton")
            };
            var expectedMessage = "Anton";

            var actualMessage = _messageBuilder.CreateGreetingMessage(nameList);

            Assert.Equal(expectedMessage, actualMessage);
        }

        [Fact]
        public void CreateFormattedMessageOfPeopleInServer_ReturnsThreeNamesInCorrectFormatting_WhenGivenThreePeople()
        {
            var nameList = new List<Person>
            {
                new Person("Anton"),
                new Person("Deb"),
                new Person("Tim")
            };
            var expectedMessage = "Anton, Deb, and Tim";

            var actualMessage = _messageBuilder.CreateGreetingMessage(nameList);

            Assert.Equal(expectedMessage, actualMessage);
        }

        [Fact]
        public void CreateFormattedMessageOfPeopleInServer_ReturnsTwoNamesInCorrectFormatting_WhenGivenTwoPeople()
        {
            // Given
            var nameList = new List<Person>
            {
                new Person("Anton"),
                new Person("Deb")
            };
            var expectedMessage = "Anton and Deb";

            // When
            var actualMessage = _messageBuilder.CreateGreetingMessage(nameList);

            Assert.Equal(expectedMessage, actualMessage);
        }

        [Fact]
        public void CreateGetTimeMessage_ReturnsCorrectlyFormattedMessage()
        {
            var nameList = new List<Person>
            {
                new Person("Anton")
            };
            var expectedMessage = "Hello Anton - the time on the server is 10:48PM on 14 March 2018";

            var actualMessage = _messageBuilder.CreateGreetingWithTimeMessage(nameList);

            Assert.Equal(expectedMessage, actualMessage);
        }
    }
}