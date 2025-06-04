using GradeBook.Data;
using GradeBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GradeBook.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GradesController : ControllerBase
{
    private readonly GradeBookContext _context;

    public GradesController(GradeBookContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Grade>>> GetAllGrades()
    {
        return await _context.Grades.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Grade>> GetGrade(int id)
    {
        var grade = await _context.Grades.FindAsync(id);
        return grade == null ? NotFound() : Ok(grade);
    }

    [HttpPost]
    public async Task<ActionResult<Grade>> CreateGrade(Grade grade)
    {
        _context.Grades.Add(grade);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetGrade), new { id = grade.Id }, grade);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGrade(int id, Grade grade)
    {
        if (id != grade.Id) return BadRequest();

        _context.Entry(grade).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGrade(int id)
    {
        var grade = await _context.Grades.FindAsync(id);
        if (grade == null) return NotFound();

        _context.Grades.Remove(grade);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}