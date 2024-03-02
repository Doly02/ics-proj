//using SchoolSystem.Common.Tests.Seeds;
using SchoolSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace SchoolSystem.DAL.Tests;

public class DbContextSubjectTests(ITestOutputHelper output) : DbContextTestsBase(output)
{
    [Fact]
    public async Task AddNew_Subject_Persisted()
    {
        //Arrange
        SubjectEntity entity = new()
        {
            Id = Guid.NewGuid(),
            Name = "ICS",
            Enrolleds = new List<EnrolledEntity>(),
            Activities = new List<ActivityEntity>()
        };

        //Act
        SchoolSystemDbContextSUT.Subjects.Add(entity);
        await SchoolSystemDbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntities = await dbx.Subjects.SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(entity, actualEntities);
    }
    [Fact]
    public async Task DeleteSubject_Something()
    {
        // Arrange
        var subject1 = new SubjectEntity 
        {   Id = Guid.NewGuid(), 
            Name = "Math Analysis II.",
            Abbreviation = "ima2", // Should Be UpperCase
            Enrolleds = new List<EnrolledEntity>(),
            Activities = new List<ActivityEntity>()
        };
        var subject2 = new SubjectEntity 
        {   Id = Guid.NewGuid(), 
            Name = "Statistics",
            Abbreviation = "IPT", // Should Stay UpperCase
            Enrolleds = new List<EnrolledEntity>(),
            Activities = new List<ActivityEntity>()
        };

        SchoolSystemDbContextSUT.Subjects.Add(subject1);
        await SchoolSystemDbContextSUT.SaveChangesAsync();
        SchoolSystemDbContextSUT.Subjects.Add(subject2);
        await SchoolSystemDbContextSUT.SaveChangesAsync();

        
        
        // Act
        SchoolSystemDbContextSUT.Subjects.Remove(subject2);
        await SchoolSystemDbContextSUT.SaveChangesAsync();

        // Assert
        var deletedSubject = await SchoolSystemDbContextSUT.Subjects.FindAsync(subject2.Id);
        var survivedSubject = await SchoolSystemDbContextSUT.Subjects.FindAsync(subject1.Id);
        Assert.Null(deletedSubject);
        Assert.NotNull(survivedSubject);
        Assert.Equal(survivedSubject.Id,subject1.Id);
    }
    [Fact]
    public async Task ModifySubject_Something()
    {
        // Arrange
        var subject1 = new SubjectEntity 
        {   Id = Guid.NewGuid(), 
            Name = "Math Analysis II.",
            Abbreviation = "ima2", // Should Be UpperCase
            Enrolleds = new List<EnrolledEntity>(),
            Activities = new List<ActivityEntity>()
        };
        var subject2 = new SubjectEntity 
        {   Id = Guid.NewGuid(), 
            Name = "Statistics",
            Abbreviation = "IPT", // Should Stay UpperCase
            Enrolleds = new List<EnrolledEntity>(),
            Activities = new List<ActivityEntity>()
        };

        SchoolSystemDbContextSUT.Subjects.Add(subject1);
        await SchoolSystemDbContextSUT.SaveChangesAsync();
        SchoolSystemDbContextSUT.Subjects.Add(subject2);
        await SchoolSystemDbContextSUT.SaveChangesAsync();

        
        
        // Act
        var toModify = await SchoolSystemDbContextSUT.Subjects.FindAsync(subject2.Id);
        toModify.Name = "Formal Languages And Compilers";
        await SchoolSystemDbContextSUT.SaveChangesAsync();


        // Assert
        var editedSubject = await SchoolSystemDbContextSUT.Subjects.FindAsync(subject2.Id);
        Assert.NotNull(editedSubject);
        Assert.Equal(editedSubject.Id,subject2.Id);
        Assert.Equal("Formal Languages And Compilers",subject2.Name);
    }
}
