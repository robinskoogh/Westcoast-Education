using Course_API.Interfaces;
using Course_API.ViewModels.CourseViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Course_API.Controllers
{
    [ApiController]
    [Route("api/v1/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRepository _courseRepo;
        public CoursesController(ICourseRepository courseRepo)
        {
            _courseRepo = courseRepo;
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<CourseOverviewViewModel>>> ListCourses() => Ok(await _courseRepo.ListCoursesAsync());

        [HttpGet("{categoryName}/list")]
        public async Task<ActionResult<List<CourseOverviewViewModel>>> ListCourses(string categoryName) => Ok(await _courseRepo.ListCoursesAsync(categoryName));

        [HttpGet("{courseNo}")]
        public async Task<ActionResult<CourseViewModel>> GetCourseByNumber(int courseNo)
        {
            var response = await _courseRepo.GetCourseByNumberAsync(courseNo);
            return response is not null ? Ok(response) : BadRequest($"There is no course with the Course No: {courseNo}");
        }

        [HttpPost]
        public async Task<ActionResult> AddCourse(PostCourseViewModel course)
        {
            try
            {
                await _courseRepo.AddCourseAsync(course);

                if (await _courseRepo.GetCourseByNumberAsync(course.CourseNo) is not null)
                    throw new Exception($"A course with CourseNo: {course.CourseNo} already exists in the database");

                return await _courseRepo.SaveChangesAsync() ?
                    StatusCode(201) :
                    StatusCode(500, "An error occurred while attempting to save the course to the database.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{courseNo}")]
        public async Task<ActionResult> UpdateCourseAsync(int courseNo, PostCourseViewModel course)
        {
            try
            {
                await _courseRepo.UpdateCourseAsync(courseNo, course);

                return await _courseRepo.SaveChangesAsync() ? NoContent() : StatusCode(500, "An error occurred while attempting to update the course");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{courseNo}")]
        public async Task<ActionResult> DeleteCourseAsync(int courseNo)
        {
            try
            {
                await _courseRepo.DeleteCourseAsync(courseNo);

                return await _courseRepo.SaveChangesAsync() ?
                    NoContent() :
                    StatusCode(500, "There was an error when attempting to delete the course from the database");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}