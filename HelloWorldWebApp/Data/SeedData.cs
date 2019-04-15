using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HelloWorldWebApp.Models;

namespace HelloWorldWebApp.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new PeopleContext(serviceProvider
                .GetRequiredService<DbContextOptions<PeopleContext>>()))
            {
                if (!context.People.Any())
                {
                    context.People.AddRange(
                        new Person { Name = "Anton"}
                    );

                    context.SaveChanges();
                }
            }
        }
    }
}
