using SchoolSystem.DAL.Enums;
using SchoolSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SchoolSystem.DAL.Entities;

namespace SchoolSystem.DAL.Seeds;

public static class EnrolledSeeds
{
    public static readonly EnrolledEntity EnrolledJohn = new()
    {
        Id = Guid.NewGuid(),
        Student = StudentSeeds.StudentJohn,
        StudentId = StudentSeeds.StudentJohn.Id,
        Subject = SubjectSeeds.ICS,
        SubjectId = SubjectSeeds.ICS.Id
    };
    
    public static readonly EnrolledEntity EnrolledJane = new()
    {
        Id = Guid.NewGuid(),
        Student = StudentSeeds.StudentJane,
        StudentId = StudentSeeds.StudentJane.Id,
        Subject = SubjectSeeds.ICS,
        SubjectId = SubjectSeeds.ICS.Id
    };

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EnrolledEntity>().HasData(
            EnrolledJohn with {Subject = null!, Student = null!},
            EnrolledJane with {Subject = null!, Student = null!}
            );
    }
}
