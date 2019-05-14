using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorldWebApp.Data;
using HelloWorldWebApp.Models;

namespace HelloWorldWebApp.Services
{
    public class ActualPeopleRepository : IPeopleRepository
    {
        private readonly PeopleContext _context;

        public ActualPeopleRepository(PeopleContext context)
        {
            _context = context;
        }

        public List<Person> GetPeopleList()
        {
            return _context.People.ToList();
        }

        public bool CheckIfNameExistsInRepository(string name)
        {
            return GetPersonFromContext(name) != null;
        }


        public Task<int> AddPersonToRepository(Person person)
        {
            _context.People.Add(person);
            return _context.SaveChangesAsync();
        }

        public Task<int> RemovePersonFromRepository(Person person)
        {
            _context.People.Remove(GetPersonFromContext(person.Name));
            return _context.SaveChangesAsync();
        }

        public Task<int> UpdatePersonInRepository(NameChangeRequest request)
        {
            _context.People.FirstOrDefault(person => person.Name == request.OldName).Name = request.NewName;
            return _context.SaveChangesAsync();
        }

        private Person GetPersonFromContext(string name)
        {
            return _context.People.FirstOrDefault(person => person.Name == name);
        }


        public bool CheckIfOwnerName(string name)
        {
            return name == "Anton";
        }
    }
}