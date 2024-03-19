using SchoolSystem.DAL.Enums;
using SchoolSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SchoolSystem.DAL.Entities;

namespace SchoolSystem.Common.Tests.Seeds;

public static class ActivitySeeds
{
    public static readonly ActivityEntity EmptyActivityEntity = new()
    {
        Id = default,
        ActivityType = default,
        Description = default,
        Start = default,
        End = default,
        Evaluations = default!,
        Place = default,
        Subject = default!,
        SubjectId = default
    };

    public static readonly ActivityEntity ActivityEntity = new()
    {
        Id = Guid.NewGuid(),
        Description = "",
        Start = new DateTime(2024, 2, 1, 1, 1, 1),
        End = new DateTime(2024, 6, 1, 1, 1, 1),
        Place = "D105",
        Subject = SubjectSeeds.ICS,
        SubjectId = SubjectSeeds.ICS.Id,
        ActivityType = ActivityType.Project
    };
    
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EnrolledEntity>().HasData(
            ActivityEntity
        );
    }
}
