namespace Course_API.Models
{
    public class Student : Person
    {
        public List<StudentCourse> Courses { get; set; } = new();
    }
}