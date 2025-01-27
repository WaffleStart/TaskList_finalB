

using System.ComponentModel.DataAnnotations;

namespace TaskList.Models
{
    public class Tag
    {
        [Key]
        public int Tag_Id { get; set; }

        public string Name { get; set; }

        public ICollection<TaskTag>? TaskTags { get; set; }
    }
}
