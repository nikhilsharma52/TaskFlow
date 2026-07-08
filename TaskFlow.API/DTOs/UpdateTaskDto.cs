using System.ComponentModel.DataAnnotations;

namespace TaskFlow.API.DTOs
{
    public class UpdateTaskDto
    {
        [Required]
        public int ProjectId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(300)]
        public string Description { get; set; } = string.Empty;
    }
}
