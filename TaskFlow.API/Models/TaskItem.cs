using System.ComponentModel.DataAnnotations;

namespace TaskFlow.API.Models
{
    public class TaskItem
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        public Project? Project { get; set; }

        public int AssignedUserId { get; set; }

        public User? AssignedUser { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(300)]
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
    }
}
