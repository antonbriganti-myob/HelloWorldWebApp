using System;
namespace HelloWorldWebApp.Services
{
    public class SystemDateTime : IDateTime
    {

        public DateTime GetCurrentTimeAndDate()
        {
            return DateTime.Now;
        }
    }
}
