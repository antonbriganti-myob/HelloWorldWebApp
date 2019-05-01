using System.Collections.Generic;
using HelloWorldWebApp.Models;

namespace HelloWorldWebApp.Services
{
    public class ActualMessageBuilder : IMessageBuilder
    {
        private readonly IDateTime _dateTime;
        public ActualMessageBuilder(IDateTime dateTime)
        {
            _dateTime = dateTime;
        }

        public string CreateGetTimeMessage(List<Person> peopleList)
        {
            var currentTime = _dateTime.GetCurrentTimeAndDate()
                                          .ToString("h:mmtt on dd MMMM yyyy");
            var people = CreateFormattedMessageOfPeopleInServer(peopleList);
            var message = $"Hello {people} - the time on the server is {currentTime}";

            return message;
        }

        public string GetPeopleInServerAsString(List<Person> peopleList)
        {
            return string.Join(",", peopleList);
        }

        public string CreateFormattedMessageOfPeopleInServer(List<Person> peopleList)
        {
            var message = "";

            switch (peopleList.Count)
            {
                case 1:
                    message = peopleList[0].Name;
                    break;
                case 2:
                    message = $"{peopleList[0].Name} and {peopleList[1].Name}";
                    break;
                default:
                {
                    for (int i = 0; i < peopleList.Count - 1; i++)
                    {
                        message += peopleList[i].Name + ", ";
                    }

                    message = $"{message}and {peopleList[peopleList.Count - 1].Name}";
                    break;
                }
            }

            return message;
        }
    }
}
