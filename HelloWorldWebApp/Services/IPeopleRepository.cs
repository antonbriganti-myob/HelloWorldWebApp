using System.Collections.Generic;
using System.Threading.Tasks;
using HelloWorldWebApp.Models;

namespace HelloWorldWebApp.Services
{
    public interface IPeopleRepository
    {
        List<Person> GetPeopleList();
        bool CheckIfNameExistsInDataStore(string name);
        Task<int> AddPersonToDataStore(Person person);
        Task<int> RemovePersonFromDataStore(Person person);
        Task<int> UpdatePersonInDataStore(NameChangeRequest request);
    }
}