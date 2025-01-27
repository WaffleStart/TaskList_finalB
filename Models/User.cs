namespace TaskList.Models
{
    using Microsoft.Identity.Client;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using TaskList.Models;
    public class User
    {
        [Key]
        public int User_Id { get; set; }

        [Required]
        [DisplayName("Username")]
        public string Username { get; set;}

        [Required]
        [EmailAddress]
        public string Email { get; set;}
        
        [Required]
        public string Password { get; set;}

        public ICollection<Task>? Tasks { get; set; }

        public bool IsAdmin { get; set; } = false;
    }
}
