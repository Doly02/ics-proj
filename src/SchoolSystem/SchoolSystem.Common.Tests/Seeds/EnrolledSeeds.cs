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
        Id = default,
        Student = default!,
        StudentId = default,
        Subject = default!,
        SubjectId = default
    };

    public static readonly EnrolledEntity EnrolledEntity1 = new()
    {
        Id = Guid.Parse(input: "3b8c8283-4c67-47a0-8e39-8f65c3c7e2b7"),
        Student = StudentSeeds.StudentEntity1,
        StudentId = StudentSeeds.StudentEntity1.Id,
        Subject = SubjectSeeds.ICS,
        SubjectId = SubjectSeeds.ICS.Id
    };

    public static readonly EnrolledEntity EnrolledEntity2 = new()
    {
        Id = Guid.Parse(input: "da59cf25-7b84-45a1-8c93-d551ae3944e5"),
        Student = StudentSeeds.StudentEntity2,
        StudentId = StudentSeeds.StudentEntity2.Id,
        Subject = SubjectSeeds.ICS,
        SubjectId = SubjectSeeds.ICS.Id
    };

    //To ensure that no tests reuse these clones for non-idempotent operations
    public static readonly EnrolledEntity EnrolledEntityUpdate = EnrolledEntity1 with { Id = Guid.Parse("a55b1b72-2c10-45ce-98d7-196f1710a3b3")};
    public static readonly EnrolledEntity EnrolledEntityDelete = EnrolledEntity1 with { Id = Guid.Parse("cc8e8e0a-30b3-4f0f-b552-58ba0cc4742d")};

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EnrolledEntity>().HasData(
            EnrolledEntity1 with { Student = null!, Subject = null! },
            EnrolledEntity2 with { Student = null!, Subject = null! },
            EnrolledEntityUpdate,
            EnrolledEntityDelete);
    }
}
