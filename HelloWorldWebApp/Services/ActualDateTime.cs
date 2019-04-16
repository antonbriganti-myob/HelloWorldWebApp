using System;
namespace HelloWorldWebApp.Services
{
    public class ActualDateTime : IDateTime
    {
        public ActualDateTime()
        {
        }

        public string GetCurrentTimeAndDate()
        {
            return DateTime.Now.ToString("h:mm:ss tt on dd MMMM yyyy");
        }
    }
}
