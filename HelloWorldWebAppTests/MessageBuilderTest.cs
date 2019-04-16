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
        public void GetPeopleInServerAsString_ReturnsSingleName_WhenGivenSinglePerson()
        {
            // Given
            List<Person> NameList = new List<Person>
            {
                new Person("Anton")
            };

            // When
            string actualMessage = _messageBuilder.GetPeopleInServerAsString(NameList);
            string expectedMessage = "Anton";


            // Then
            Assert.Equal(expectedMessage, actualMessage);
        }

        [Fact]
        public void GetPeopleInServerAsString_ReturnsTwoNamesInCorrectFormatting_WhenGivenTwoPeople()
        {
            // Given
            List<Person> NameList = new List<Person>
            {
                new Person("Anton"),
                new Person("Deb")
            };

            // When
            string actualMessage = _messageBuilder.GetPeopleInServerAsString(NameList);
            string expectedMessage = "Anton and Deb";


            // Then
            Assert.Equal(expectedMessage, actualMessage);
        }

        [Fact]
        public void GetPeopleInServerAsString_ReturnsThreeNamesInCorrectFormatting_WhenGivenThreePeople()
        {
            // Given
            List<Person> NameList = new List<Person>
            {
                new Person("Anton"),
                new Person("Deb"),
                new Person("Tim")
            };

            // When
            string actualMessage = _messageBuilder.GetPeopleInServerAsString(NameList);
            string expectedMessage = "Anton, Deb, and Tim";


            // Then
            Assert.Equal(expectedMessage, actualMessage);
        }
    }
}
