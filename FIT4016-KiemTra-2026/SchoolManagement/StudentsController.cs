using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/students")]
public class StudentsController : ControllerBase
{
    private readonly SchoolDbContext _context;

    public StudentsController(SchoolDbContext context)
    {
        _context = context;
    }

    // READ (Pagination)
    [HttpGet]
    public IActionResult GetStudents(int page = 1)
    {
        var students = _context.Students
            .Include(s => s.School)
            .Skip((page - 1) * 10)
            .Take(10)
            .ToList();

        return Ok(students);
    }

    // CREATE
    [HttpPost]
    public IActionResult Create(Student student)
    {
        if (!_context.Schools.Any(s => s.Id == student.SchoolId))
            return BadRequest("School does not exist.");

        if (_context.Students.Any(s => s.StudentId == student.StudentId))
            return BadRequest("Student ID already exists.");

        if (_context.Students.Any(s => s.Email == student.Email))
            return BadRequest("Email already exists.");

        _context.Students.Add(student);
        _context.SaveChanges();

        return Ok("Student created successfully.");
    }

    // UPDATE
    [HttpPut("{id}")]
    public IActionResult Update(int id, Student input)
    {
        var student = _context.Students.Find(id);
        if (student == null)
            return NotFound("Student not found.");

        student.FullName = input.FullName;
        student.Email = input.Email;
        student.Phone = input.Phone;
        student.SchoolId = input.SchoolId;
        student.UpdatedAt = DateTime.Now;

        _context.SaveChanges();
        return Ok("Student updated successfully.");
    }

    // DELETE
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var student = _context.Students.Find(id);
        if (student == null)
            return NotFound("Student not found.");

        _context.Students.Remove(student);
        _context.SaveChanges();

        return Ok("Student deleted successfully.");
    }
}
