using System.Collections.Generic;
using System.Linq;
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


        public void AddPersonToRepository(Person person)
        {
            _context.People.Add(person);
            _context.SaveChangesAsync();
        }

        public void RemovePersonFromRepository(Person person)
        {
            _context.People.Remove(GetPersonFromContext(person.Name));
            _context.SaveChangesAsync();
        }

        public void UpdatePersonInRepository(NameChangeRequest request)
        {
            _context.People.FirstOrDefault(person => person.Name == request.OldName).Name = request.NewName;
            _context.SaveChangesAsync();
        }

        private Person GetPersonFromContext(string name)
        {
            return _context.People.FirstOrDefault(person => person.Name == name);
        }
    }
}