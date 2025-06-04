using GradeBook.Data;
using GradeBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GradeBook.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly GradeBookContext _context;

    public StudentsController(GradeBookContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetAllStudents()
    {
        return await _context.Students.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);
        return student == null ? NotFound() : Ok(student);
    }

    [HttpPost]
    public async Task<ActionResult<Student>> CreateStudent(Student student)
    {
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudent(int id, Student student)
    {
        if (id != student.Id) return BadRequest();

        _context.Entry(student).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null) return NotFound();

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("{id}/grades")]
    public async Task<ActionResult<IEnumerable<Grade>>> GetGradesForStudent(int id)
    {
        var student = await _context.Students
            .Include(s => s.Grades)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (student == null) return NotFound();
        return Ok(student.Grades);
    }
}
