using Course_API.Interfaces;
using Course_API.ViewModels.StudentViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Course_API.Controllers
{
    [ApiController]
    [Route("api/v1/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _studentRepo;
        public StudentsController(IStudentRepository studentRepo)
        {
            _studentRepo = studentRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<StudentViewModel>>> ListStudentsAsync() => Ok(await _studentRepo.ListStudentsAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentViewModel>> GetStudentByIdAsync(string id)
        {
            var response = await _studentRepo.GetStudentByIdAsync(id);

            return response is not null ? Ok(response) : BadRequest($"No student with Id: {id} was found in the database");
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<StudentViewModel>> GetStudentByEmailAsync(string email)
        {
            var response = await _studentRepo.GetStudentByEmailAsync(email);

            return response is not null ? Ok(response) : BadRequest($"No student with Email: {email} was found in the database");
        }

        [HttpPost]
        public async Task<ActionResult> AddStudentAsync(PostStudentViewModel student)
        {
            try
            {
                if (await _studentRepo.GetStudentByEmailAsync(student.Email!) is not null)
                    return BadRequest($"The email address {student.Email} is already in use");

                await _studentRepo.AddStudentAsync(student);

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStudentAsync(string id, PostStudentViewModel student)
        {
            try
            {
                await _studentRepo.UpdateStudentAsync(id, student);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudentByIdAsync(string id)
        {
            try
            {
                await _studentRepo.DeleteStudentByIdAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("email/{email}")]
        public async Task<ActionResult> DeleteStudentByEmailAsync(string email)
        {
            try
            {
                await _studentRepo.DeleteStudentByEmailAsync(email);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}