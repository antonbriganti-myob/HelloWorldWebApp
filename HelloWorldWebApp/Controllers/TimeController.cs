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
    }
}