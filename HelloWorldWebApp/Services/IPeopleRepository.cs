using System.Collections.Generic;
using System.Threading.Tasks;
using HelloWorldWebApp.Models;

namespace HelloWorldWebApp.Services
{
    public interface IPeopleRepository
    {
        List<Person> GetPeopleList();
        bool CheckIfNameExistsInRepository(string name);
        Task<int> AddPersonToRepository(Person person);
        Task<int> RemovePersonFromRepository(Person person);
        Task<int> UpdatePersonInRepository(NameChangeRequest request);
    }
}