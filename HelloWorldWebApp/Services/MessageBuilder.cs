using System;
using System.Collections.Generic;
using HelloWorldWebApp.Models;

namespace HelloWorldWebApp.Services
{
    public class MessageBuilder
    {
        private readonly IDateTime _dateTime;
        public MessageBuilder(IDateTime dateTime)
        {
            _dateTime = dateTime;
        }

        public String CreateGetTimeMessage(List<Person> PeopleList)
        {
            string CurrentTime = _dateTime.GetCurrentTimeAndDate()
                                          .ToString("h:mmtt on dd MMMM yyyy");
            string People = GetPeopleInServerAsString(PeopleList);
            string Message = string.Format("Hello {0} - the time on the server is {1}",
                                        People,
                                        CurrentTime);

            return Message;
        }

        public String GetPeopleInServerAsString(List<Person> PeopleList)
        {
            var Message = "";

            if (PeopleList.Count == 1)
            {
                Message = PeopleList[0].Name;
            }
            else if (PeopleList.Count == 2)
            {
                Message = string.Format("{0} and {1}",
                                        PeopleList[0].Name,
                                        PeopleList[1].Name);
            }
            else
            {
                for (int i = 0; i < PeopleList.Count - 1; i++)
                {
                    Message += PeopleList[i].Name + ", ";
                }

                Message = string.Format("{0}and {1}",
                                        Message,
                                        PeopleList[PeopleList.Count - 1].Name);
            }

            return Message;
        }
    }
}
