dnamespace TaskFlow.API.Models
{
    public class TaskItem
    {
        public int TaskId { get; set; }
        public int ProjectId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
