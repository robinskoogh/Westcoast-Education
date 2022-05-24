using Course_API.Models;

namespace Course_API.ViewModels.CourseViewModels
{
    public class CourseViewModel
    {
        public int CourseNo { get; set; }
        public string? Name { get; set; }
        public string? Length { get; set; }
        public string? Category { get; set; }
        public string? Description { get; set; }
        public string? Details { get; set; }
    }
}