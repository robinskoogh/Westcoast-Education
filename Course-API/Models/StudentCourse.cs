namespace Course_API.Models
{
    public class StudentCourse
    {
        // public DateOnly DatePurchased { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        // public bool CourseCompleted { get; set; } = false;

        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public Student? Student { get; set; }
        public Course? Course { get; set; }
    }
}