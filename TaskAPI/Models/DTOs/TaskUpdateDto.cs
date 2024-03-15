namespace TaskAPI.Models.DTOs
{
    public class TaskUpdateDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateOnly? DueDate { get; set; }
        public PriorityType? Priority { get; set; }
        public StatusType? Status { get; set; }
    }
}
