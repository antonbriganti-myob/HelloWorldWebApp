using System;
namespace HelloWorldWebApp.Services
{
    public class SystemDateTime : IDateTime
    {
        public SystemDateTime()
        {
        }

        public DateTime GetCurrentTimeAndDate()
        {
            return DateTime.Now;
        }
    }
}
