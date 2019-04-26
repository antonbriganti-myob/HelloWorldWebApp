using System.Collections.Generic;
using HelloWorldWebApp.Models;

namespace HelloWorldWebApp.Services
{
    public interface IMessageBuilder
    {
        string CreateGetTimeMessage(List<Person> peopleList);
    }
}
