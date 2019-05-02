using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorldWebApp.Data;
using HelloWorldWebApp.Models;
using HelloWorldWebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;

namespace HelloWorldWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeController : ControllerBase
    { 
        private readonly IMessageBuilder _messageBuilder;
        private readonly IPeopleRepository _peopleRepository;

        public TimeController(IPeopleRepository repo, IMessageBuilder builder)
        {
            _peopleRepository = repo;
            _messageBuilder = builder;
        }

        [HttpGet]
        public ActionResult<string> GetGreetingWithTime()
        {
            var peopleList = _peopleRepository.GetPeopleList(); 
            var message = _messageBuilder.CreateGreetingWithTimeMessage(peopleList);
            return message;
        }
        
        [HttpGet("people")]
        public ActionResult<string> GetNamesInServer()
        {
            var peopleList = _peopleRepository.GetPeopleList();
            var message = _messageBuilder.GetPeopleInServerAsString(peopleList);
            return message;
        }


        [HttpPost]
        public async Task<IActionResult> AddPersonToWorld(Person person)
        {
            if (!(_peopleRepository.CheckIfNameExistsInWorld(person.Name)))
            {
                await _peopleRepository.AddPersonToContext(person);
                return CreatedAtAction("AddPersonToWorld", person, person.Name + " has been added to the world");
            }
            else
            {
                return BadRequest("Person with that name already exists in the world");
            }

        }

        [HttpDelete]
        public async Task<IActionResult> RemovePersonFromWorld(Person person)
        {
            if (!(_peopleRepository.CheckIfOwnerName(person.Name)))
            {
                if (_peopleRepository.CheckIfNameExistsInWorld(person.Name))
                {
                    await _peopleRepository.RemovePersonFromWorld(person);
                    return new OkObjectResult(person.Name + " has been removed from the world");
                }
                else
                {
                    return NotFound("No such person with that name exists in the world");
                }
            }
            else
            {
                return BadRequest("Anton can't be removed from the world");
            }
        }
    }
}
