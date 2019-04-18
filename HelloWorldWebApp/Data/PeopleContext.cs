using Microsoft.EntityFrameworkCore;
using HelloWorldWebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace HelloWorldWebApp.Data
{
    public class PeopleContext : DbContext
    {
        public PeopleContext(DbContextOptions<PeopleContext> options)
            : base(options)
        {
        }

        public DbSet<Person> People { get; set; }

    }
}