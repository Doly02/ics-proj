using SchoolSystem.DAL.Enums;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.DAL.Entities;

namespace SchoolSystem.DAL.Seeds;

public static class ActivitySeeds
{
    public static readonly ActivityEntity Project = new()
    {
        Id = Guid.Parse("8e615f4a-7a3b-4f86-b199-d7d48c4652e8"),
        Description = "",
        Start = new DateTime(2024, 2, 1, 1, 1, 1),
        End = new DateTime(2024, 6, 1, 1, 1, 1),
        Place = "D105",
        Subject = SubjectSeeds.ICS,
        SubjectId = SubjectSeeds.ICS.Id,
        ActivityType = ActivityType.Project,
        Name = "School System - Project"
    };

    public static readonly ActivityEntity Lecture = new()
    {
        Id = Guid.Parse("5e8e4902-ecdc-448d-b1e9-2c2b513f561e"),
        Description = "Introduction to Data Structures",
        Start = new DateTime(2024, 2, 2, 9, 0, 0),
        End = new DateTime(2024, 2, 2, 11, 0, 0),
        Place = "A101",
        Subject = SubjectSeeds.ICS,
        SubjectId = SubjectSeeds.ICS.Id,
        ActivityType = ActivityType.Lecture,
        Name = "Lecture on Data Structures"
    };

    public static readonly ActivityEntity Exam = new()
    {
        Id = Guid.Parse("a4e7bce2-7bfd-4f30-b8f8-319d3f39e655"),
        Description = "Final exam in ...",
        Start = new DateTime(2024, 6, 20, 13, 0, 0),
        End = new DateTime(2024, 6, 20, 16, 0, 0),
        Place = "C303",
        Subject = SubjectSeeds.ICS,
        SubjectId = SubjectSeeds.ICS.Id,
        ActivityType = ActivityType.Exam,
        Name = "Final Exam in ..."
    };

    public static readonly ActivityEntity Workshop = new()
    {
        Id = Guid.Parse("9fad0e7d-9e67-4b6e-a7c3-6bbde72c0194"),
        Description = "Workshop on Artificial Intelligence and Machine Learning",
        Start = new DateTime(2024, 3, 15, 10, 0, 0),
        End = new DateTime(2024, 3, 15, 15, 0, 0),
        Place = "Lab 4",
        Subject = SubjectSeeds.ICS,
        SubjectId = SubjectSeeds.ICS.Id,
        ActivityType = ActivityType.Other,
        Name = "AI Workshop"
    };

    
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityEntity>().HasData(
            Project with { Subject = null!, Evaluations = Array.Empty<EvaluationEntity>() },
            Lecture with { Subject = null!, Evaluations = Array.Empty<EvaluationEntity>() },
            Exam with { Subject = null!, Evaluations = Array.Empty<EvaluationEntity>() },
            Workshop with { Subject = null!, Evaluations = Array.Empty<EvaluationEntity>() }
        );
    }
}
