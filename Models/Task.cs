using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Task : BaseModel
    {
        [StringLength(75)]
        public string Title { get; set; }
        public string Details { get; set; }
        public bool IsDone { get; set; } = false;
    }
}
