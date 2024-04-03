using SchoolSystem.DAL.Enums;
using SchoolSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SchoolSystem.DAL.Entities;

namespace SchoolSystem.DAL.Seeds;

public static class EvaluationSeeds
{
    public static readonly EvaluationEntity EvaluationJohn = new()
    {
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
