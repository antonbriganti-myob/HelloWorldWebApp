using System;
using HelloWorldWebApp.Services;
using Moq;

namespace HelloWorldWebAppTests.Helpers
{
    public class DateTimeHelper
    {

        public static Mock<IDateTime> CreateMockDateTime()
        {

            var mockDateTime = new Mock<IDateTime>();
            mockDateTime.Setup(datetime => datetime.GetCurrentTimeAndDate())
                        .Returns(new DateTime(2018, 3, 14).Date.Add(new TimeSpan(22, 48, 0)));

            return mockDateTime;
        }
    }
}
