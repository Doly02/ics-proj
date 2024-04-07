using SchoolSystem.DAL.Enums;
using SchoolSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SchoolSystem.DAL.Entities;

namespace SchoolSystem.Common.Tests.Seeds;

public static class EnrolledSeeds
{
    public static readonly EnrolledEntity EmptyEnrolledEntity = new()
    {
        Id = default!,
        Student = default!,
        StudentId = default,
        Subject = default!,
        SubjectId = default
    };

    public static readonly EnrolledEntity EnrolledEntity = new()
    {
        Id = Guid.NewGuid(),
        Student = StudentSeeds.StudentEntity1,
        StudentId = StudentSeeds.StudentEntity1.Id,
        Subject = SubjectSeeds.ICS,
        SubjectId = SubjectSeeds.ICS.Id
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EnrolledEntity>().HasData(
            EnrolledEntity with {Subject = null!, Student = null!}
            );
    }
}
