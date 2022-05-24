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
        public async Task<ActionResult<StudentViewModel>> GetStudentByIdAsync(int id)
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
            await _studentRepo.AddStudentAsync(student);

            if (await _studentRepo.GetStudentByEmailAsync(student.Email!) is not null)
                return BadRequest("The email specified is already in use");

            return await _studentRepo.SaveChangesAsync() ? StatusCode(201) : StatusCode(500, "An error occurred while attempting to save the student to the database");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStudentAsync(int id, PostStudentViewModel student)
        {
            try
            {
                await _studentRepo.UpdateStudentAsync(id, student);

                return await _studentRepo.SaveChangesAsync() ? NoContent() : StatusCode(500, "An error occurred while attempting to update the student to the database");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudentByIdAsync(int id)
        {
            try
            {
                await _studentRepo.DeleteStudentByIdAsync(id);

                return await _studentRepo.SaveChangesAsync() ? NoContent() : StatusCode(500, "An error occurred while attempting to delete the student from the database");
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

                return await _studentRepo.SaveChangesAsync() ? NoContent() : StatusCode(500, "An error occurred while attempting to delete the student from the database");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}