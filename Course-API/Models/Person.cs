namespace Course_API.Models
{
    public abstract class Person
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? StreetAddress { get; set; }
        public int ZipCode { get; set; }
        public string? City { get; set; }
    }
}