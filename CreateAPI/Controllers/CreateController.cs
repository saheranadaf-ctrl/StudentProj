using CreateAPI.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CreateAPI.Controllers
{

    
    [ApiController]
    [Route("api/[controller]")]
    public class CreateController : ControllerBase
    {

        private readonly StudentDataContext _dbContext;
        private readonly ILogger<CreateController> _logger;

        public CreateController(StudentDataContext dbContext, ILogger<CreateController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

// ------------------------ CREATE METHOD -------------------------------

        [HttpPost("Create")]
        public async Task<ActionResult<Student>> CreateStudent(Student student)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                
                // Log API request
                _logger.LogInformation($"Request received to create a new student: {JsonConvert.SerializeObject(student)}");

    
                
                // Add any additional data validation logic here

                _dbContext.Students.Add(student);
                 await _dbContext.SaveChangesAsync();

                // Log API response
                _logger.LogInformation($"Student created successfully: {JsonConvert.SerializeObject(student)}");


                return CreatedAtAction("GetStudentById", new { id = student.StudentId }, student);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex);
                //give the valid message with status code
                _logger.LogError(ex, "An error occurred while creating a student.");

                return StatusCode(500, "Internal server error");
            }
        }



        [HttpGet("{id}")]
        public IActionResult GetStudentById(string id)
        {
            try
            {
                 // Log API request
                _logger.LogInformation($"Request received to get student with ID: {id}");


                var student = _dbContext.Students.FirstOrDefault(o=>o.StudentId == id);

                if (student == null)
                {
                    throw new StudentNotFoundException($"Student with ID {id} not found.");
                }

                // Log API response
                _logger.LogInformation($"Returned student data: {JsonConvert.SerializeObject(student)}");


                return Ok(student); // Return 200 OK with the student data if found
            }

            catch (StudentNotFoundException ex)
            {
                _logger.LogWarning(ex, $"Student not found with ID: {id}");
                return NotFound("Student not found");
            }

            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex);
                _logger.LogError(ex, $"An error occurred while retrieving student with ID: {id}");

                return StatusCode(500, "Internal server error");
            }
        }


    }
}
