namespace TaskAPI.Models
{
    public class User
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public required string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public required string FirstName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public required string LastName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? MiddleName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public required string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<Task>? Tasks { get; set; }
    }
}
