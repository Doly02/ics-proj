//using SchoolSystem.Common.Tests.Seeds;
using SchoolSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;
using SchoolSystem.DAL.Enums;

namespace  SchoolSystem.DAL.Tests;

public class DbContextEnrolledTests(ITestOutputHelper output) : DbContextTestsBase(output)
{
    [Fact]
    public async Task AddNew_Enrolled_Persisted()
    {
        //Arrange
        SubjectEntity subject = new() // todo seeds
        {
            Id = Guid.NewGuid(),
            Name = "ICS"
        };
        StudentEntity student = new()
        {
            Id = Guid.NewGuid(),
            Name = "Peter",
            Surname = "Parker"
        };
    
        EnrolledEntity entity = new()
        {
            Id = Guid.NewGuid(),
            Student = student,
            Subject =  subject,
            SubjectId = subject.Id,
            StudentId = student.Id
        };

        //Act
        SchoolSystemDbContextSUT.Enrolleds.Add(entity);
        await SchoolSystemDbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntities = await dbx.Enrolleds.SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(entity, actualEntities);
    }
}
