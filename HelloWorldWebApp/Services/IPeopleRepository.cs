using System.Collections.Generic;
using HelloWorldWebApp.Models;

namespace HelloWorldWebApp.Services
{
    public interface IPeopleRepository
    {
        List<Person> GetPeopleList();
        bool CheckIfNameExistsInRepository(string name);
        void AddPersonToRepository(Person person);
        void RemovePersonFromRepository(Person person);
        void UpdatePersonInRepository(NameChangeRequest request);
    }
}