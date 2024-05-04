using SchoolSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;


namespace SchoolSystem.DAL.Seeds;

public static class EvaluationSeeds
{
    public static readonly EvaluationEntity EvaluationJohn = new()
    {
        Id = Guid.Parse("c0a5c2d1-8a95-4e09-bba2-67c3d133e20e"),
        Student = StudentSeeds.StudentJohn,
        StudentId = StudentSeeds.StudentJohn.Id,
        Activity = ActivitySeeds.Project,
        ActivityId = ActivitySeeds.Project.Id,
        Note = "Well done",
        Score = 98
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EvaluationEntity>().HasData(
            EvaluationJohn with {Activity = null!, Student = null!}
        );
    }
}
