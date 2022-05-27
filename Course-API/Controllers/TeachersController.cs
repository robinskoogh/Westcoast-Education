using Course_API.Interfaces;
using Course_API.ViewModels.TeacherViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Course_API.Controllers
{
    [ApiController]
    [Route("api/v1/teachers")]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherRepository _teacherRepo;
        public TeachersController(ITeacherRepository teacherRepo)
        {
            _teacherRepo = teacherRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<TeacherViewModel>>> ListTeachersAsync() => await _teacherRepo.ListTeachersAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherViewModel>> GetTeacherByIdAsync(string id)
        {
            var response = await _teacherRepo.GetTeacherByIdAsync(id);

            return response is not null ? Ok(response) : BadRequest($"There is no teacher with Id: {id} in the database");
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<TeacherViewModel>> GetTeacherByEmailAsync(string email)
        {
            var response = await _teacherRepo.GetTeacherByEmailAsync(email);

            return response is not null ? Ok(response) : BadRequest($"There is no teacher with Email: {email} in the database");
        }

        [HttpPost]
        public async Task<ActionResult> AddTeacherAsync(PostTeacherViewModel teacher)
        {
            try
            {
                if (await _teacherRepo.GetTeacherByEmailAsync(teacher.Email!) is not null)
                    return BadRequest($"The email address {teacher.Email} is already in use");

                await _teacherRepo.AddTeacherAsync(teacher);

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTeacherAsync(string id, PostTeacherViewModel teacher)
        {
            try
            {
                await _teacherRepo.UpdateTeacherAsync(id, teacher);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTeacherByIdAsync(string id)
        {
            try
            {
                await _teacherRepo.DeleteTeacherByIdAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("email/{email}")]
        public async Task<ActionResult> DeleteTeacherByEmailAsync(string email)
        {
            try
            {
                await _teacherRepo.DeleteTeacherByEmailAsync(email);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}