using SchoolSystem.DAL.Enums;
using SchoolSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SchoolSystem.DAL.Entities;

namespace SchoolSystem.DAL.Seeds;

public static class ActivitySeeds
{
    public static readonly ActivityEntity Project = new()
    {
        Id = Guid.NewGuid(),
        Description = "",
        Start = new DateTime(2024, 2, 1, 1, 1, 1),
        End = new DateTime(2024, 6, 1, 1, 1, 1),
        Place = "D105",
        Subject = SubjectSeeds.ICS,
        SubjectId = SubjectSeeds.ICS.Id,
        ActivityType = ActivityType.Project,
    };
    
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityEntity>().HasData(
            Project with {Subject = null!, Evaluations = Array.Empty<EvaluationEntity>()}
        );
    }
}
