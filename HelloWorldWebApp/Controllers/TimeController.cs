using System;
using System.Threading.Tasks;
using HelloWorldWebApp.Models;
using HelloWorldWebApp.Services;
using Microsoft.AspNetCore.Mvc;

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
            if (!_peopleRepository.CheckIfNameExistsInDataStore(person.Name))
            {
                await _peopleRepository.AddPersonToDataStore(person);
                return CreatedAtAction("AddPersonToWorld", person, person.Name + " has been added to the world");
            }

            return BadRequest("Person with that name already exists in the world");

        }

        [HttpDelete]
        public async Task<IActionResult> RemovePersonFromWorld(Person person)
        {
            
            if (!CheckIfOwnerName(person.Name))
            {
                if (!_peopleRepository.CheckIfNameExistsInDataStore(person.Name))
                    return NotFound("No such person with that name exists in the world");
                await _peopleRepository.RemovePersonFromDataStore(person);
                return new OkObjectResult(person.Name + " has been removed from the world");

            }
            return BadRequest("Anton can't be removed from the world");
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdatePersonInWorld(Person person)
        {

            return BadRequest();
        }

        private static bool CheckIfOwnerName(string name)
        {
            return name == "Anton";
        }
    }
}
