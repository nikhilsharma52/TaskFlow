using System.ComponentModel.DataAnnotations;

namespace TaskFlow.API.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        public string PasswordHash { get; set; } = string.Empty;

        public string Role { get; set; } = "User";

        public ICollection<Project> Projects { get; set; } = new List<Project>();

        public ICollection<TaskItem> AssignedTasks { get; set; } = new List<TaskItem>();
    
    }
}
