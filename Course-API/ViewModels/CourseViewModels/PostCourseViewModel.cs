using System.ComponentModel.DataAnnotations;
using Course_API.Models;

namespace Course_API.ViewModels.CourseViewModels
{
    public class PostCourseViewModel
    {
        public int CourseNo { get; set; }
        public string? Name { get; set; }
        public string? Duration { get; set; }
        [Required]
        public string? Category { get; set; }
        public string? Description { get; set; }
        public string? Details { get; set; }
    }
}