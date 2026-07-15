using System.ComponentModel.DataAnnotations;

namespace TaskFlow.API.DTOs
{
    public class CreateTaskDto
    {
        [Required]
        public int ProjectId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(300)]
        public string Description { get; set; } = string.Empty;

        //public int AssignedUserId { get; set; }

    }
}
