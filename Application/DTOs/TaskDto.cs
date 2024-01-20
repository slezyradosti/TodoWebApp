namespace Application.DTOs
{
    public class TaskDto : BaseDto
    {
        public string Title { get; set; }
        public string Details { get; set; }
        public bool IsDone { get; set; } = false;
    }
}
