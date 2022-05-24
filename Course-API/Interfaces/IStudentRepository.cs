using Course_API.ViewModels.StudentViewModels;

namespace Course_API.Interfaces
{
    public interface IStudentRepository
    {
        public Task<List<StudentViewModel>> ListStudentsAsync();
        public Task<StudentViewModel?> GetStudentByIdAsync(int id);
        public Task<StudentViewModel?> GetStudentByEmailAsync(string email);
        public Task AddStudentAsync(PostStudentViewModel student);
        public Task UpdateStudentAsync(int id, PostStudentViewModel student);
        public Task DeleteStudentByIdAsync(int id);
        public Task DeleteStudentByEmailAsync(string email);
        public Task<bool> SaveChangesAsync();
    }
}