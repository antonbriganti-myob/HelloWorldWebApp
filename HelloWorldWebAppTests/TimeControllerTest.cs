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
        public void GETRequest_ReturnsNameAndTime()
        {
            // given
            var mockPeopleContext = new Mock<PeopleContext>();
            //todo: discuss how to mock DbSet
            //mockPeopleContext.SetupGet(context => context.People).Returns(???);

            var dateTimeMock = DateTimeHelper.CreateMockDateTime();

            var TestController = new TimeController(mockPeopleContext.Object, dateTimeMock.Object);

            //"Hello Anton - the time on the server is 10:48pm on 14 March 2018"

            // when 
            ActionResult<string> ActualResult = TestController.GetCurrentTime();
            var ExpectedResult = "Hello Anton - the time on the server is 10:48pm on 14 March 2018";

            // then

            Assert.Equal(ExpectedResult, ActualResult.Value);
        }

    }
}
