namespace TaskList.Models
{
    public class TaskTag
    {
        public int Task_Id { get; set; }
        public int Tag_Id { get; set; }

        public Task Task { get; set; }

        public Tag Tag { get; set; }
    }
}