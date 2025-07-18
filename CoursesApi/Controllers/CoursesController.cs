using CoursesApi.Data;
using CoursesApi.Dtos;
using CoursesApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoursesApi.Controllers
{
    [ApiController]
    [Route("/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly CoursesDbContext _context;

        public CoursesController(CoursesDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _context.Courses.Include(c => c.Students).ToListAsync();

            IEnumerable<CourseDto> courseDtos = courses.Select(c => 
            new CourseDto(
                c.Id, 
                c.Name,
                c.Students.Select(s => new StudentDto(s.Id, s.FullName))
            ));

            return Ok(courseDtos);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseRequest request)
        {
            Course course = new Course { Name = request.Name };
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();

            return Ok(new { id = course.Id });
        }

        [HttpPost("{id:guid}/students")]
        public async Task<IActionResult> CreateStudent(Guid id, [FromBody] CreateStudentRequest request)
        {
            if (!_context.Courses.Any(c => c.Id == id)) return BadRequest();
            Student student = new Student {  FullName = request.FullName, CourseId = id }; 
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();

            return Ok(new { id = student.Id }); 
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);
            if(course == null) return NotFound();

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
