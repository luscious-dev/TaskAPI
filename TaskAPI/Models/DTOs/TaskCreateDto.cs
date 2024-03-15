namespace TaskAPI.Models.DTOs
{
    public class TaskCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateOnly DueDate { get; set; }
        public PriorityType Priority { get; set; }

    }
}
