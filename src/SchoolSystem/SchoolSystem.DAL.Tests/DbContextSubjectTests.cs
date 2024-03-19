//using SchoolSystem.Common.Tests.Seeds;
using SchoolSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using SchoolSystem.DAL.Enums;
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
    
    [Fact]
    public async Task AddRelation_AssignActivitiesToSubject()
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
        toModify.Activities.Clear();
        var newActivities = new List<ActivityEntity>
        {
            new ActivityEntity
            {
                Id = Guid.NewGuid(),
                Start = new DateTime(2024, 3, 15, 9, 0, 0),
                End = new DateTime(2024, 3, 15, 10, 30, 0),
                Place = "D105",
                ActivityType = ActivityType.Exam,
                Description = "Midterm Statistics",
                Subject = toModify,
                SubjectId = toModify.Id
            },
            new ActivityEntity
            {
                Id = Guid.NewGuid(),
                Start = new DateTime(2024, 3, 22, 9, 0, 0), // March 22, 2024, 9:00 AM
                End = new DateTime(2024, 3, 22, 10, 30, 0), // March 22, 2024, 10:30 AM
                Place = "D105",
                ActivityType = ActivityType.Lecture,
                Description = "Statistics Lecture",
                Subject = toModify,
                SubjectId = toModify.Id,
                Evaluations = new List<EvaluationEntity>()
            }
        };
        foreach (var activity in newActivities)
        {
            SchoolSystemDbContextSUT.Activities.Add(activity);  // Add Activity To Context   
            // Assign Activity toModify.Activities.Add(activity);                  
        }
        await SchoolSystemDbContextSUT.SaveChangesAsync();

        
        // Assert
        var modifiedSubject = await SchoolSystemDbContextSUT.Subjects.FindAsync(subject2.Id);
        Assert.NotNull(modifiedSubject);
        Assert.NotEmpty(modifiedSubject.Activities);                    // To Verify That Activities Are Not Empty
        Assert.Equal(2, modifiedSubject.Activities.Count); // Verify The Count of Activities Is as Expected

        // Verify Details of Activities
        var activity1 = modifiedSubject.Activities.FirstOrDefault(a => a.Description == "Midterm Statistics");
        Assert.NotNull(activity1);
        Assert.Equal(new DateTime(2024, 3, 15, 9, 0, 0), activity1.Start);
        Assert.Equal("D105", activity1.Place);
        Assert.Equal(ActivityType.Exam, activity1.ActivityType);
        
        var activity2 = modifiedSubject.Activities.FirstOrDefault(a => a.Description == "Statistics Lecture");
        Assert.NotNull(activity2);
        Assert.Equal(new DateTime(2024, 3, 22, 9, 0, 0), activity2.Start);
        Assert.Equal("D105", activity2.Place);
        Assert.Equal(ActivityType.Lecture, activity2.ActivityType);
        
    }
     [Fact]
    public async Task DeleteRelation_AssignActivitiesToSubject()
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
        subject2.Activities = new List<ActivityEntity>
        {
            new ActivityEntity
            {
                Id = Guid.NewGuid(),
                Start = new DateTime(2024, 3, 15, 9, 0, 0),
                End = new DateTime(2024, 3, 15, 10, 30, 0),
                Place = "D105",
                ActivityType = ActivityType.Exam,
                Description = "Midterm Statistics",
                Subject = subject2,
                SubjectId = subject2.Id
            },
            new ActivityEntity
            {
                Id = Guid.NewGuid(),
                Start = new DateTime(2024, 3, 22, 9, 0, 0), // March 22, 2024, 9:00 AM
                End = new DateTime(2024, 3, 22, 10, 30, 0), // March 22, 2024, 10:30 AM
                Place = "D105",
                ActivityType = ActivityType.Lecture,
                Description = "Statistics Lecture",
                Subject = subject2,
                SubjectId = subject2.Id,
                Evaluations = new List<EvaluationEntity>()
            }
        };
        
        SchoolSystemDbContextSUT.Subjects.Add(subject1);
        await SchoolSystemDbContextSUT.SaveChangesAsync();
        SchoolSystemDbContextSUT.Subjects.Add(subject2);
        await SchoolSystemDbContextSUT.SaveChangesAsync();

        
        // Act
        var findedSubject = await SchoolSystemDbContextSUT.Subjects.FindAsync(subject2.Id);
        var removedActivity = findedSubject.Activities.First();
        findedSubject.Activities.Remove(removedActivity);
        SchoolSystemDbContextSUT.Activities.Remove(removedActivity);
        await SchoolSystemDbContextSUT.SaveChangesAsync();

        // Assert
        var modifiedSubject = await SchoolSystemDbContextSUT.Subjects.FindAsync(subject2.Id);
        Assert.NotNull(modifiedSubject);
        Assert.NotEmpty(modifiedSubject.Activities);                    // To Verify That Activities Are Not Empty
        Assert.Equal(1, modifiedSubject.Activities.Count); // Verify The Count of Activities Is as Expected

        // Verify Details of Activities
        var activity2 = modifiedSubject.Activities.FirstOrDefault(a => a.Description == "Statistics Lecture");
        Assert.NotNull(activity2);
        Assert.Equal(new DateTime(2024, 3, 22, 9, 0, 0), activity2.Start);
        Assert.Equal("D105", activity2.Place);
        Assert.Equal(ActivityType.Lecture, activity2.ActivityType);
        
    }

}
