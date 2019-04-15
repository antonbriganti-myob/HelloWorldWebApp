using System;
using System.ComponentModel.DataAnnotations;

// Representation of a person in memory

namespace HelloWorldWebApp.Models
{
    public class Person
    {

        [Required]
        [Key]
        public string Name { get; set; }

        public Person()
        {

        }
    }
}
