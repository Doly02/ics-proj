using SchoolSystem.DAL.Enums;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.DAL.Entities;

namespace SchoolSystem.DAL.Seeds;

public static class ActivitySeeds
{
    public static readonly ActivityEntity Project = new()
    {
        Id = Guid.Parse(input: "8e615f4a-7a3b-4f86-b199-d7d48c4652e8"),
        Description = "",
        Start = new DateTime(2024, 2, 1, 1, 1, 1),
        End = new DateTime(2024, 6, 1, 1, 1, 1),
        Place = "D105",
        Subject = SubjectSeeds.ICS,
        SubjectId = SubjectSeeds.ICS.Id,
        ActivityType = ActivityType.Project,
        Name = "Školský systém - projekt"
    };
    
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityEntity>().HasData(
            Project with {Subject = null!, Evaluations = Array.Empty<EvaluationEntity>()}
        );
    }
}
