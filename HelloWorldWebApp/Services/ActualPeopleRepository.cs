using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorldWebApp.Data;
using HelloWorldWebApp.Models;
using Microsoft.EntityFrameworkCore;

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
        
        public bool CheckIfNameExistsInDataStore(string name)
        {
            return GetPersonFromContext(name) != null;
        }

        private Person GetPersonFromContext(string name)
        {
            return _context.People.FirstOrDefault(person => person.Name == name);
        }


        public Task<int> AddPersonToDataStore(Person person)
        {
            _context.People.Add(person);
            return _context.SaveChangesAsync();
        }

        public Task<int> RemovePersonFromDataStore(Person person)
        {
            _context.People.Remove(GetPersonFromContext(person.Name));
            return _context.SaveChangesAsync();
        }

        public Task<int> UpdatePersonInDataStore(NameChangeRequest request)
        {
            _context.People.FirstOrDefault(person => person.Name == request.OldName).Name = request.NewName;
            return _context.SaveChangesAsync();
        }


        public bool CheckIfOwnerName(string name)
        {
            return name == "Anton";
        }
    }
}