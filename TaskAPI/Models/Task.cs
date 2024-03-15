using System.ComponentModel.DataAnnotations.Schema;

namespace TaskAPI.Models
{
    public class Task
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public required int UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required DateOnly DueDate { get; set; }
        public PriorityType Priority { get; set; }
        public StatusType Status { get; set; } = StatusType.Pending;

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum PriorityType
    {
        Low = -1,
        Medium = 0,
        High = 1
    }

    /// <summary>
    /// 
    /// </summary>
    public enum StatusType
    {
        Pending = 1,
        InProgress = 2,
        Completed = 3
    }
}
