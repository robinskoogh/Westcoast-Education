using AutoMapper;
using AutoMapper.QueryableExtensions;
using Course_API.Data;
using Course_API.Interfaces;
using Course_API.Models;
using Course_API.ViewModels.CourseViewModels;
using Microsoft.EntityFrameworkCore;

namespace Course_API.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly CourseContext _context;
        private readonly IMapper _mapper;
        public CourseRepository(CourseContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public Task<List<CourseOverviewViewModel>> ListCoursesAsync() =>
            _context.Courses.OrderBy(c => c.CourseNo)
                .ProjectTo<CourseOverviewViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

        public Task<List<CourseOverviewViewModel>> ListCoursesAsync(string categoryName) =>
            _context.Courses.Include(c => c.Category)
                .Where(c => c.Category!.Name!.ToLower() == categoryName.ToLower())
                .OrderBy(c => c.CourseNo)
                .ProjectTo<CourseOverviewViewModel>(_mapper.ConfigurationProvider)
                    .ToListAsync();

        public async Task<CourseViewModel?> GetCourseByNumberAsync(int courseNo)
        {
            var course = await _context.Courses
                .Include(c => c.Students)
                .Include(c => c.Category)
                    .Where(c => c.CourseNo == courseNo)
                        .SingleOrDefaultAsync();

            if (course is null) return null;

            var courseViewModel = _mapper.Map<CourseViewModel>(course);

            courseViewModel.Category = course.Category!.Name;

            return courseViewModel;
        }

        public async Task AddCourseAsync(PostCourseViewModel course)
        {
            var courseToAdd = _mapper.Map<Course>(course);

            var category = await _context.Categories.SingleOrDefaultAsync(c => c.Name!.ToLower() == course.Category!.ToLower());

            if (category is null)
                throw new Exception("There is no category with that name. Please add it to the database before adding the course");

            courseToAdd.Category = category;

            await _context.Courses.AddAsync(courseToAdd);
        }

        public async Task UpdateCourseAsync(int courseNo, PostCourseViewModel course)
        {
            var courseToUpdate = await GetCourseByCourseNo(courseNo);

            if (courseToUpdate is null)
                throw new Exception($"There is no course with a CourseNo of {courseNo} in the database");

            if (courseNo != course.CourseNo)
                throw new Exception($"The specified CourseNo: {courseNo} does not match that of the provided Course object ({course.CourseNo})");

            _mapper.Map(course, courseToUpdate);

            var category = await _context.Categories.SingleOrDefaultAsync(c => c.Name!.ToLower() == course.Category!.ToLower());

            if (category is null)
                throw new Exception($"There is no Category named {course.Category}. Please add it to the database before editing the course.");

            courseToUpdate.Category = category;

            _context.Update(courseToUpdate);
        }

        public async Task DeleteCourseAsync(int courseNo)
        {
            var courseToDelete = await GetCourseByCourseNo(courseNo);

            if (courseToDelete is null)
                throw new Exception($"There is no course with the specified CourseNo: {courseNo}");

            _context.Remove(courseToDelete);
        }

        private async Task<Course?> GetCourseByCourseNo(int courseNo) => await _context.Courses.Where(c => c.CourseNo == courseNo).SingleOrDefaultAsync();

        public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0 ? true : false;

    }
}