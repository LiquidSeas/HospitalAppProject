using System.ComponentModel.DataAnnotations;

namespace HospitalApp.Models
{
    public class Announcement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? TargetGroup { get; set; }
    }

}
