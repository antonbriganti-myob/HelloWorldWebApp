using System.Collections.Generic;
using HelloWorldWebApp.Models;

namespace HelloWorldWebApp.Services
{
    public interface IMessageBuilder
    {
        string CreateGreetingWithTimeMessage(List<Person> peopleList);
        string GetPeopleInServerAsString(List<Person> peopleList);
    }
}
