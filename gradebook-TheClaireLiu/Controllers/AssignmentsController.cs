using GradeBook.Data;
using GradeBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GradeBook.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssignmentsController : ControllerBase
{
    private readonly GradeBookContext _context;

    public AssignmentsController(GradeBookContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Assignment>>> GetAllAssignments()
    {
        return await _context.Assignments.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Assignment>> GetAssignment(int id)
    {
        var assignment = await _context.Assignments.FindAsync(id);
        return assignment == null ? NotFound() : Ok(assignment);
    }

    [HttpPost]
    public async Task<ActionResult<Assignment>> CreateAssignment(Assignment assignment)
    {
        _context.Assignments.Add(assignment);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAssignment), new { id = assignment.Id }, assignment);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAssignment(int id, Assignment assignment)
    {
        if (id != assignment.Id) return BadRequest();

        _context.Entry(assignment).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAssignment(int id)
    {
        var assignment = await _context.Assignments.FindAsync(id);
        if (assignment == null) return NotFound();

        _context.Assignments.Remove(assignment);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpGet("{id}/grades")]
    public async Task<ActionResult<IEnumerable<Grade>>> GetGradesForAssignment(int id)
    {
        var assignment = await _context.Assignments
            .Include(a => a.Grades)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (assignment == null) return NotFound();
        return Ok(assignment.Grades);
    }
}
