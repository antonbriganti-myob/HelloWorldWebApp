using System;
using System.Collections.Generic;
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
        private readonly MessageBuilder _messagebuilder;

        public TimeController(PeopleContext context)
        {
            _context = context;
            _messagebuilder = new MessageBuilder(new ActualDateTime());
        }

        [HttpGet]
        public ActionResult<String> GetCurrentTime()
        {
            var Message = _messagebuilder.CreateGetTimeMessage(_context.People.ToList());
            return Message;
        }


        [HttpPost]
        public async void AddPersonToWorld(Person person)
        {
            if (_context.People.Find(person.Name) == null)
            {
                _context.People.Add(person);
                await _context.SaveChangesAsync();
            }

            //todo: return if the person was added (200) or if they already existed (???)
        }
    }
}
