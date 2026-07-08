namespace TaskFlow.API.DTOs
{
    public class ProjectResponseDto
    {
        public int ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
