using SchoolSystem.DAL.Enums;
using SchoolSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SchoolSystem.DAL.Entities;

namespace SchoolSystem.Common.Tests.Seeds;

public static class EvaluationSeeds
{
    public static readonly EvaluationEntity EmptyEnrolledEntity = new()
    {
        Id = Guid.NewGuid(),
        Student = default!,
        StudentId = default,
        Activity = default!,
        ActivityId = default!
    };

    public static readonly EvaluationEntity EvaluationEntity = new()
    {
        Id = Guid.NewGuid(),
        Student = StudentSeeds.StudentEntity1,
        StudentId = StudentSeeds.StudentEntity1.Id,
        Activity = ActivitySeeds.ActivityEntity,
        ActivityId = ActivitySeeds.ActivityEntity.Id
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EnrolledEntity>().HasData(
            EvaluationEntity with {Activity = null!, Student = null!}
        );
    }
}
