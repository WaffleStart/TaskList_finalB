using Microsoft.AspNetCore.Mvc.Rendering;
using TaskList.Models;

namespace TaskList.ViewModels
{
    public class TaskViewModel
    {
        public int Task_Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DueDate { get; set; }

        public string Status { get; set; }

        public int User_Id { get; set; }

        public User? User { get; set; }

        public List<string> Tags { get; set; }
    }


}
