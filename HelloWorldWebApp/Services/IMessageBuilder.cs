using System.Collections.Generic;
using HelloWorldWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorldWebApp.Services
{
    public interface IMessageBuilder
    {
        string CreateGetTimeMessage(List<Person> peopleList);
        string GetPeopleInServerAsString(List<Person> peopleList);
        string CreateFormattedMessageOfPeopleInServer(List<Person> peopleList);
    }
}
