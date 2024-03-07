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
        Student = default!,
        StudentId = default,
        Subject = default!,
        SubjectId = default
    };

    public static readonly EnrolledEntity EnrolledEntity1 = new()
    {
        Student = StudentSeeds.StudentEntity1,
        StudentId = StudentSeeds.StudentEntity1.Id,
        Subject = SubjectSeeds.ICS,
        SubjectId = SubjectSeeds.ICS.Id
    };

    public static readonly EnrolledEntity EnrolledEntity2 = new()
    {
        Student = StudentSeeds.StudentEntity2,
        StudentId = StudentSeeds.StudentEntity2.Id,
        Subject = SubjectSeeds.ICS,
        SubjectId = SubjectSeeds.ICS.Id
    };

    //To ensure that no tests reuse these clones for non-idempotent operations
    //public static readonly EnrolledEntity EnrolledEntityUpdate = EnrolledEntity1 with { Id = Guid.Parse("a55b1b72-2c10-45ce-98d7-196f1710a3b3")};
    //public static readonly EnrolledEntity EnrolledEntityDelete = EnrolledEntity1 with { Id = Guid.Parse("cc8e8e0a-30b3-4f0f-b552-58ba0cc4742d")};

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EnrolledEntity>().HasData(
            EnrolledEntity1 with { Student = null!, Subject = null! },
            EnrolledEntity2 with { Student = null!, Subject = null! }
            //EnrolledEntityUpdate,
            //EnrolledEntityDelete
            );
    }
}
