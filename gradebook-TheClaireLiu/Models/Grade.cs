namespace GradeBook.Models;

public class Grade
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int AssignmentId { get; set; }
    public string Score { get; set; } = string.Empty;

    public Student? Student { get; set; }
    public Assignment? Assignment { get; set; }
}