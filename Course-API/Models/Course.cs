using System.ComponentModel.DataAnnotations.Schema;

namespace Course_API.Models
{
    public class Course
    {
        public int Id { get; set; }
        public int CourseNo { get; set; }
        public string? Name { get; set; }
        public string? Duration { get; set; }
        public int CategoryId { get; set; }
        public string? Description { get; set; }
        public string? Details { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
        public ICollection<AppUser> Students { get; set; } = new List<AppUser>();
    }
}