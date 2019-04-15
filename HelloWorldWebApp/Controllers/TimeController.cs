using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorldWebApp.Data;
using HelloWorldWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorldWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeController : ControllerBase
    {
        private readonly PeopleContext _context;

        public TimeController(PeopleContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<String> GetCurrentTime()
        {
            string CurrentTime = DateTime.Now.ToString("h:mm:ss tt on dd MMMM yyyy");
            string People = GetPeopleInServerAsString(_context.People.ToList());
            string Message = string.Format("Hello {0} - the time on the server is {1}",
                                        People,
                                        CurrentTime);
            return Message;
        }

        private String GetPeopleInServerAsString(List<Person> People)
        {
            var Message = "";
            if (People.Count == 1)
            {
                Message = People[0].Name;
            }
            else if (People.Count == 2)
            {
                Message = string.Format("{0} & {1}",
                                        People[0].Name,
                                        People[1].Name);
            }
            else
            {
                for (int i = 0; i < People.Count - 1; i++)
                {
                    Message += People[i].Name + ", ";
                }

                Message = string.Format("{0}and {1}",
                                        Message,
                                        People[People.Count - 1].Name);
            }

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
            //todo: ask about how 
            //return GetCurrentTime();
        }
    }
}
