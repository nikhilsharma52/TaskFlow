using System.ComponentModel.DataAnnotations;

namespace TaskFlow.API.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength (200)]
        public string Description { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        [Required]
        public int OwnerUserId { get; set; }

        public User? OwnerUser { get; set; }

        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();

      
    }
}
