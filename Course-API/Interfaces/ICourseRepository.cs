using Course_API.ViewModels.CourseViewModels;

namespace Course_API.Interfaces
{
    public interface ICourseRepository
    {
        public Task<List<CourseOverviewViewModel>> ListCoursesAsync();
        public Task<List<CourseOverviewViewModel>> ListCoursesAsync(string courseName);
        public Task<CourseViewModel?> GetCourseByNumberAsync(int courseNo);
        public Task AddCourseAsync(PostCourseViewModel course);
        public Task UpdateCourseAsync(int courseNo, PostCourseViewModel course);
        public Task DeleteCourseAsync(int courseNo);
        public Task<bool> SaveChangesAsync();
    }
}