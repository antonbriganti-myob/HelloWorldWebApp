using Microsoft.EntityFrameworkCore;
using HelloWorldWebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace HelloWorldWebApp.Data
{
    public class PeopleContext : DbContext
    {
        // todo: figure out how to inject options with in memory database
        public PeopleContext(DbContextOptions<PeopleContext> options)
            : base(options)
        {
        }

        public DbSet<Person> People { get; set; }

    }
}