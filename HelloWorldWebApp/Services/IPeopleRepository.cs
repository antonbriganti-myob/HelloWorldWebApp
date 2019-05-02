using System.Collections.Generic;
using System.Threading.Tasks;
using HelloWorldWebApp.Data;
using HelloWorldWebApp.Models;

namespace HelloWorldWebApp.Services
{
    public interface IPeopleRepository
    {
        List<Person> GetPeopleList();
        bool CheckIfNameExistsInWorld(string name);
        Task<int> AddPersonToContext(Person person);
        Task<int> RemovePersonFromWorld(Person person);
        
        bool CheckIfOwnerName(string name);
    }
}