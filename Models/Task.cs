namespace Models
{
    public class Task : BaseModel
    {
        public string Details { get; set; }
        public bool IsDone { get; set; } = false;
    }
}
