using System.Linq;
using System.Threading.Tasks;
using HelloWorldWebApp.Data;
using HelloWorldWebApp.Models;
using HelloWorldWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorldWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeController : ControllerBase
    {
        private readonly PeopleContext _context;
        private readonly IMessageBuilder _messagebuilder;

        public TimeController(PeopleContext context, IMessageBuilder builder)
        {
            _context = context;
            _messagebuilder = builder;
        }

        [HttpGet]
        public ActionResult<string> GetCurrentTime()
        {
            var message = _messagebuilder.CreateGetTimeMessage(_context.People.ToList());
            return message;
        }


        [HttpPost]
        public async Task<IActionResult> AddPersonToWorld(Person person)
        {
            if (_context.People.Find(person.Name) == null)
            {
                _context.People.Add(person);
                await _context.SaveChangesAsync();
                return CreatedAtAction("AddPersonToWorld", person);
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
