using GradeBook.Models;
using Microsoft.EntityFrameworkCore;

namespace GradeBook.Data;

public class GradeBookContext : DbContext
{
    public GradeBookContext(DbContextOptions<GradeBookContext> options) : base(options) { }

    public DbSet<Student> Students => Set<Student>();
    public DbSet<Assignment> Assignments => Set<Assignment>();
    public DbSet<Grade> Grades => Set<Grade>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Grade>()
            .HasOne(g => g.Student)
            .WithMany(s => s.Grades)
            .HasForeignKey(g => g.StudentId);

        modelBuilder.Entity<Grade>()
            .HasOne(g => g.Assignment)
            .WithMany(a => a.Grades)
            .HasForeignKey(g => g.AssignmentId);
    }
}