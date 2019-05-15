using System;

namespace HelloWorldWebApp.Services
{
    public class SystemDateTime : IDateTime
    {
        public DateTime GetCurrentTimeAndDate()
        {
            TimeZoneInfo australianTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Australia/Melbourne");
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, australianTimeZoneInfo);
        }
    }
}