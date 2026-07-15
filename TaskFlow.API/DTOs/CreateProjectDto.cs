using System.ComponentModel.DataAnnotations;

namespace TaskFlow.API.DTOs
{
    public class CreateProjectDto
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Description { get; set; } = string.Empty;

        //[Required]
        //public int OwnerUserId { get; set; }

    }
}
