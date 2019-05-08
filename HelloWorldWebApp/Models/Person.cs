using System.ComponentModel.DataAnnotations;

// Representation of a person in memory

namespace HelloWorldWebApp.Models
{
    public class Person
    {
        
        public long Id { get; set; }
        
        [Required]
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
