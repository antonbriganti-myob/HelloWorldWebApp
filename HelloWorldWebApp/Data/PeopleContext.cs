using Microsoft.EntityFrameworkCore;
using HelloWorldWebApp.Models;

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