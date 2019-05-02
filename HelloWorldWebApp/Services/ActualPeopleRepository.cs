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
        
        public bool CheckIfNameExistsInWorld(string name)
        {
            return (_context.People.Find(name) != null);
        }


        public Task<int> AddPersonToContext(Person person)
        {
            _context.People.Add(person);
            return _context.SaveChangesAsync();
        }

        public Task<int> RemovePersonFromWorld(Person person)
        {
            _context.People.Remove(_context.People.Find(person.Name));
            return _context.SaveChangesAsync();
        }
        
        

        public bool CheckIfOwnerName(string name)
        {
            return (name == "Anton");
        }
    }
}