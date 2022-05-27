using Course_API.ViewModels.StudentViewModels;

namespace Course_API.Interfaces
{
    public interface IStudentRepository
    {
        public Task<List<StudentViewModel>> ListStudentsAsync();
        public Task<StudentViewModel?> GetStudentByIdAsync(string id);
        public Task<StudentViewModel?> GetStudentByEmailAsync(string email);
        public Task AddStudentAsync(PostStudentViewModel student);
        public Task UpdateStudentAsync(string id, PostStudentViewModel student);
        public Task DeleteStudentByIdAsync(string id);
        public Task DeleteStudentByEmailAsync(string email);
    }
}