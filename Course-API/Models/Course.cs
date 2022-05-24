using System.ComponentModel.DataAnnotations.Schema;

namespace Course_API.Models
{
    public class Course
    {
        public int Id { get; set; }
        public int CourseNo { get; set; }
        public string? Name { get; set; }
        public int Length { get; set; }
        public string? LengthUnit { get; set; }
        public int CategoryId { get; set; }
        public string? Description { get; set; }
        public string? Details { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }
        public List<StudentCourse> Students { get; set; } = new();
    }
}