using SchoolSystem.Common.Tests.Seeds;
using SchoolSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace SchoolSystem.DAL.Tests;

public class DbContextStudentTests(ITestOutputHelper output) : DbContextTestsBase(output)
{
    [Fact]
    public async Task AddNew_Student_Persisted()
    {
        //Arrange
        StudentEntity entity = new()
        {
            Id = Guid.Parse("C5DE45D7-64A0-4E8D-AC7F-BF5CFDFB0EFC"),
            Name = "John",
            Surname = "Doe",
            ImageUrl = null,
            Enrolleds = new List<EnrolledEntity>(),
            Evaluations = new List<EvaluationEntity>()
        };

        //Act
        SchoolSystemDbContextSUT.Students.Add(entity);
        await SchoolSystemDbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntities = await dbx.Students.SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(entity, actualEntities);
    }
    
    [Fact]
    public async Task Delete_Student_Deleted()
    {
        //Arrange
        var baseEntity = StudentSeeds.StudentEntityDelete;

        //Act
        SchoolSystemDbContextSUT.Students.Remove(baseEntity);
        await SchoolSystemDbContextSUT.SaveChangesAsync();

        //Assert
        Assert.False(await SchoolSystemDbContextSUT.Students.AnyAsync(i => i.Id == baseEntity.Id));
    }
}
