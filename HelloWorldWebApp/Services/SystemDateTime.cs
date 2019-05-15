using System;

namespace HelloWorldWebApp.Services
{
    public class SystemDateTime : IDateTime
    {
        public DateTime GetCurrentTimeAndDate()
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.Local);
        }
    }
}