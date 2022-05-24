namespace Course_API.Models
{
    public class Teacher : Person
    {
        public ICollection<Category> AreasOfExpertise { get; set; } = new List<Category>();
    }
}