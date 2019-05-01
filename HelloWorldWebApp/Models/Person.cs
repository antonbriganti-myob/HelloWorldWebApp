﻿using System.ComponentModel.DataAnnotations;

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

        public Person(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
