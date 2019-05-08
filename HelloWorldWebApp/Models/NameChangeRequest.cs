using System.ComponentModel.DataAnnotations;

namespace HelloWorldWebApp.Models
{
    public class NameChangeRequest
    {
        [Required]
        public string OldName { get; set; }
        [Required]
        public string NewName { get; set; }

        public NameChangeRequest(string oldName, string newName)
        {
            OldName = oldName;
            NewName = newName;
        }
    }
}