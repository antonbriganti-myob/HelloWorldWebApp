using HelloWorldWebApp.Models;
using HelloWorldWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorldWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : Controller
    {
        private readonly IPeopleRepository _peopleRepository;
        private readonly IMessageBuilder _messageBuilder;

        public PeopleController(IPeopleRepository peopleRepository, IMessageBuilder messageBuilder)
        {
            _peopleRepository = peopleRepository;
            _messageBuilder = messageBuilder;
        }

        [HttpGet]
        public ActionResult<string> GetNamesInServer()
        {
            var peopleList = _peopleRepository.GetPeopleList();
            var message = _messageBuilder.GetPeopleInServerAsString(peopleList);
            return message;
        }


        [HttpPost]
        public IActionResult AddPersonToWorld(Person person)
        {
            if (person == null)
            {
                return BadRequest("No person was given");
            }

            if (!_peopleRepository.CheckIfNameExistsInRepository(person.Name))
            {
                _peopleRepository.AddPersonToRepository(person);
                return CreatedAtAction("AddPersonToWorld", person, $"{person.Name} has been added to the world");
            }

            return BadRequest($"{person.Name} already exists in the world");
        }

        [HttpDelete]
        public IActionResult RemovePersonFromWorld(Person person)
        {
            if (person == null)
            {
                return BadRequest("No person was given");
            }

            if (!CheckIfOwnerName(person.Name))
            {
                if (!_peopleRepository.CheckIfNameExistsInRepository(person.Name))
                    return NotFound($"No person with the name {person.Name} exists in the world");
                _peopleRepository.RemovePersonFromRepository(person);
                return new OkObjectResult($"{person.Name} has been removed from the world");
            }

            return BadRequest("Anton can't be removed from the world");
        }

        [HttpPut]
        public IActionResult UpdatePersonInWorld(NameChangeRequest nameChangeRequest)
        {
            if (nameChangeRequest == null)
            {
                return BadRequest("No name change request was given");
            }

            if (!CheckIfOwnerName(nameChangeRequest.OldName))
            {
                if (!_peopleRepository.CheckIfNameExistsInRepository(nameChangeRequest.OldName))
                    return BadRequest($"{nameChangeRequest.OldName} does not exist in the world, and was not updated");
                if (_peopleRepository.CheckIfNameExistsInRepository(nameChangeRequest.NewName))
                    return BadRequest($"{nameChangeRequest.NewName} already exists in the world");

                _peopleRepository.UpdatePersonInRepository(nameChangeRequest);
                return new OkObjectResult(
                    $"{nameChangeRequest.OldName} has been changed to {nameChangeRequest.NewName} in the world");
            }

            return BadRequest("Can't change the owner's name");
        }

        private static bool CheckIfOwnerName(string name)
        {
            return name == "Anton";
        }
    }
}