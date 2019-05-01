using System;
using System.Collections.Generic;
using HelloWorldWebApp.Models;
using HelloWorldWebApp.Services;
using HelloWorldWebAppTests.Helpers;
using Moq;
using Xunit;

namespace HelloWorldWebAppTests
{
    public class MessageBuilderTest
    {

        private readonly ActualMessageBuilder _messageBuilder;
        public MessageBuilderTest()
        {
            var dateTimeMock = DateTimeHelper.CreateMockDateTime();
            _messageBuilder = new ActualMessageBuilder(dateTimeMock.Object);

        }

        [Fact]
        public void CreateFormattedMessageOfPeopleInServer_ReturnsSingleName_WhenGivenSinglePerson()
        {
            // Given
            var nameList = new List<Person>
            {
                new Person("Anton")
            };

            // When
            var actualMessage = _messageBuilder.CreateFormattedMessageOfPeopleInServer(nameList);
            var expectedMessage = "Anton";


            // Then
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

            // When
            var actualMessage = _messageBuilder.CreateFormattedMessageOfPeopleInServer(nameList);
            var expectedMessage = "Anton and Deb";


            // Then
            Assert.Equal(expectedMessage, actualMessage);
        }

        [Fact]
        public void CreateFormattedMessageOfPeopleInServer_ReturnsThreeNamesInCorrectFormatting_WhenGivenThreePeople()
        {
            // Given
            var nameList = new List<Person>
            {
                new Person("Anton"),
                new Person("Deb"),
                new Person("Tim")
            };

            // When
            var actualMessage = _messageBuilder.CreateFormattedMessageOfPeopleInServer(nameList);
            var expectedMessage = "Anton, Deb, and Tim";


            // Then
            Assert.Equal(expectedMessage, actualMessage);
        }

        [Fact]
        public void CreateGetTimeMessage_ReturnsCorrectlyFormattedMessage()
        {
            var nameList = new List<Person>
            {
                new Person("Anton")
            };

            var actualMessage = _messageBuilder.CreateGetTimeMessage(nameList);
            var expectedMessage = "Hello Anton - the time on the server is 10:48PM on 14 March 2018";

            Assert.Equal(expectedMessage, actualMessage);
        }
    }
}
