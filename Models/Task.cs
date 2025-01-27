using System.ComponentModel.DataAnnotations;

namespace TaskList.Models
{
    public class Task
    {
        [Key]
        public int Task_Id { get; set; }

        public int? User_Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DueDate { get; set;}

        public string Status { get; set; }

        public User? User { get; set; } = new User();

        public ICollection<TaskTag>? TaskTags { get; set; }
    }
}
