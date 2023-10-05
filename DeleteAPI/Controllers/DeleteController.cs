using DeleteAPI.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeleteAPI.Controllers
{

    
    [ApiController]
    [Route("api/[controller]")]
    public class DeleteController : ControllerBase
    {

        private readonly StudentDataContext _dbContext;

        public DeleteController(StudentDataContext dbContext)
        {
            _dbContext = dbContext;
        }

// ------------------  DELETE METHOD ------------------------------

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteStudent(string id)
        {
            try
            {
                //check if the id is present
                var existingStudent = await _dbContext.Students.FindAsync(id);

                //if the entered is not found
                if (existingStudent == null)
                {
                    return NotFound("Student not found");
                }

                //if the id entered is found
                _dbContext.Students.Remove(existingStudent);
                await _dbContext.SaveChangesAsync();

                return Ok(existingStudent);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex);
                //give the valid message with status code
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
