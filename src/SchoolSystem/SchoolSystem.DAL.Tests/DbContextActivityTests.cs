using SchoolSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;
using SchoolSystem.DAL.Enums;

namespace  SchoolSystem.DAL.Tests;

public class DbContextActivityTests(ITestOutputHelper output) : DbContextTestsBase(output)
{
    [Fact]
    public async Task AddNewActivity_Persisted()
    {
        //Arrange
        SubjectEntity subject = new() // todo seed subject maybe
        {
            Id = Guid.NewGuid()
        };
    
        ActivityEntity entity = new()
        {
            Id = Guid.Parse("C5DE45D7-64A0-4E8D-AC7F-BF5CFDFB0EFC"),
            Start = new DateTime(2023, 10, 1, 14, 0, 0),    // Set Start Date
            End = new DateTime(2023, 10, 1, 16, 0, 0),      // Set End Date
            Place = "101B", 
            ActivityType = ActivityType.Lecture, 
            Description = "Introduction to Programming",
            Subject = subject,
            SubjectId = subject.Id,
        };

        //Act
        SchoolSystemDbContextSUT.Activities.Add(entity);
        await SchoolSystemDbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntities = await dbx.Activities.SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(entity, actualEntities);
    }

    [Fact]
    public async Task UpdateActivity_ActivityUpdated()
    {
        // Arrange
        SubjectEntity subject = new() // todo seed subject maybe
        {
            Id = Guid.NewGuid()
        };

        var entity = new ActivityEntity
        {
            Id = Guid.NewGuid(), // Vytvoření nového GUID pro entitu
            Start = new DateTime(2023, 10, 1, 14, 0, 0),    // Set Start Date
            End = new DateTime(2023, 10, 1, 16, 0, 0),      // Set End Date
            Place = "101B",
            ActivityType = ActivityType.Lecture,
            Description = "Introduction to Programming",
            Subject = subject, // Vytvoření nového předmětu
            SubjectId = subject.Id
        };

        SchoolSystemDbContextSUT.Activities.Add(entity);
        await SchoolSystemDbContextSUT.SaveChangesAsync();

        // Upravit entitu
        entity.Description = "Updated Introduction to Programming";

        // Act
        SchoolSystemDbContextSUT.Activities.Update(entity);
        await SchoolSystemDbContextSUT.SaveChangesAsync();

        // Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var updatedEntity = await dbx.Activities.FindAsync(entity.Id);
        Assert.NotNull(updatedEntity); // Ensure the entity is not null
        DeepAssert.Equal(entity, updatedEntity);
    }

    [Fact]
    public async Task GetActivityById_ActivityRetrieved()
    {
        // Arrange
        SubjectEntity subject = new() // todo seed subject maybe
        {
            Id = Guid.NewGuid()
        };
        var newEntity = new ActivityEntity
        {
            Id = Guid.NewGuid(),
            Start = new DateTime(2023, 10, 2, 10, 0, 0),
            End = new DateTime(2023, 10, 2, 12, 0, 0),
            Place = "201A",
            ActivityType = ActivityType.Project,
            Description = "Advanced Programming Workshop",
            Subject = subject,
            SubjectId = subject.Id
        };

        await SchoolSystemDbContextSUT.Activities.AddAsync(newEntity);
        await SchoolSystemDbContextSUT.SaveChangesAsync();

        // Act
        var retrievedEntity = await SchoolSystemDbContextSUT.Activities.FindAsync(newEntity.Id);

        // Assert
        Assert.NotNull(retrievedEntity);
        DeepAssert.Equal(newEntity, retrievedEntity);
    }

    [Fact]
    public async Task DeleteActivity_ActivityIsDeleted()
    {
        // Arrange
        SubjectEntity subject = new() // todo seed subject maybe
        {
            Id = Guid.NewGuid()
        };
        var newEntity = new ActivityEntity
        {
            Id = Guid.NewGuid(),
            Start = new DateTime(2023, 10, 3, 14, 0, 0),
            End = new DateTime(2023, 10, 3, 16, 0, 0),
            Place = "301C",
            ActivityType = ActivityType.Lecture,
            Description = "Seminar on Database Management",
            Subject = subject,
            SubjectId = subject.Id
        };

        await SchoolSystemDbContextSUT.Activities.AddAsync(newEntity);
        await SchoolSystemDbContextSUT.SaveChangesAsync();

        // Act
        SchoolSystemDbContextSUT.Activities.Remove(newEntity);
        await SchoolSystemDbContextSUT.SaveChangesAsync();

        // Assert
        var deletedEntity = await SchoolSystemDbContextSUT.Activities.FindAsync(newEntity.Id);
        Assert.Null(deletedEntity);
    }

    [Fact]
    public async Task SubjectActivity_RelationshipPersisted()
    {
        // Arrange
        var subject = new SubjectEntity
        {
            Id = Guid.NewGuid(),
            Name = "Mathematics"
        };

        var activity = new ActivityEntity
        {
            Id = Guid.NewGuid(),
            Start = new DateTime(2023, 10, 4, 9, 0, 0),
            End = new DateTime(2023, 10, 4, 11, 0, 0),
            Place = "Room 402",
            ActivityType = ActivityType.Lecture,
            Description = "Calculus Lecture",
            Subject = subject,
            SubjectId = subject.Id
        };

        await SchoolSystemDbContextSUT.Subjects.AddAsync(subject);
        await SchoolSystemDbContextSUT.Activities.AddAsync(activity);
        await SchoolSystemDbContextSUT.SaveChangesAsync();

        // Act
        var retrievedSubject = await SchoolSystemDbContextSUT.Subjects
            .Include(s => s.Activities)
            .FirstOrDefaultAsync(s => s.Id == subject.Id);

        // Assert
        Assert.NotNull(retrievedSubject);
        Assert.Single(retrievedSubject.Activities);
        DeepAssert.Equal(activity, retrievedSubject.Activities.First());
    }

    [Fact]
    public async Task ActivityEvaluation_RelationshipPersisted()
    {
        // Arrange
        var student = new StudentEntity
        {
            Id = Guid.NewGuid(),
            Name = "John",
            Surname = "Doe"
        };
        SubjectEntity subject = new() // todo seed subject maybe
        {
            Id = Guid.NewGuid()
        };

        var activity = new ActivityEntity
        {
            Id = Guid.NewGuid(),
            Subject = subject,
            SubjectId = subject.Id,
            Start = new DateTime(2023, 10, 5, 14, 0, 0),
            End = new DateTime(2023, 10, 5, 16, 0, 0),
            Place = "Room 500",
            ActivityType = ActivityType.Other,
            Description = "Robotics Workshop"
        };

        await SchoolSystemDbContextSUT.Students.AddAsync(student);
        await SchoolSystemDbContextSUT.Activities.AddAsync(activity);
        await SchoolSystemDbContextSUT.SaveChangesAsync();

        var evaluation = new EvaluationEntity
        {
            StudentId = student.Id, // Nastavení ID studenta, místo celé entity
            ActivityId = activity.Id, // Nastavení ID aktivity, místo celé entity
            Student = student,
            Activity = activity,
            Score = 90,
            Note = "Excellent work"
        };

        await SchoolSystemDbContextSUT.Evaluations.AddAsync(evaluation);
        await SchoolSystemDbContextSUT.SaveChangesAsync();

        // Act
        var retrievedActivity = await SchoolSystemDbContextSUT.Activities
            .Include(a => a.Evaluations)
            .ThenInclude(e => e.Student)
            .FirstOrDefaultAsync(a => a.Id == activity.Id);

        // Assert
        Assert.NotNull(retrievedActivity);
        Assert.Single(retrievedActivity.Evaluations);
        var actualEvaluation = retrievedActivity.Evaluations.First();
        Assert.Equal(90, actualEvaluation.Score);
        Assert.Equal("Excellent work", actualEvaluation.Note);
        DeepAssert.Equal(student, actualEvaluation.Student);
    }

    [Fact]
    public async Task AddTwoActivitiesWithSameId_ShouldThrowException()
    {
        // Arrange
        var sharedId = Guid.NewGuid();
        SubjectEntity subject1 = new() // todo seed subject maybe
        {
            Id = Guid.NewGuid()
        };
        SubjectEntity subject2 = new() // todo seed subject maybe
        {
            Id = Guid.NewGuid()
        };
        var firstActivity = new ActivityEntity
        {
            Id = sharedId,
            Start = new DateTime(2023, 10, 6, 9, 0, 0),
            End = new DateTime(2023, 10, 6, 11, 0, 0),
            Place = "Room 101",
            ActivityType = ActivityType.Lecture,
            Description = "First Activity",
            Subject = subject1,
            SubjectId = subject1.Id
        };

        SchoolSystemDbContextSUT.Activities.Add(firstActivity);
        await SchoolSystemDbContextSUT.SaveChangesAsync();

        var secondActivity = new ActivityEntity
        {
            Id = sharedId, // Použití stejného ID jako první aktivity
            Start = new DateTime(2023, 10, 7, 9, 0, 0),
            End = new DateTime(2023, 10, 7, 11, 0, 0),
            Place = "Room 102",
            ActivityType = ActivityType.Lecture,
            Description = "Second Activity",
            Subject = subject2,
            SubjectId = subject2.Id
        };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
        {
            SchoolSystemDbContextSUT.Activities.Add(secondActivity);
            return SchoolSystemDbContextSUT.SaveChangesAsync();
        });

        Assert.NotNull(exception);
        Assert.Contains("The instance of entity type 'ActivityEntity' cannot be tracked", exception.Message);
    }

    [Fact]
    public async Task LoadTwoActivitiesSequentially_ActivitiesLoadedCorrectly()
    {
        // Arrange
        SubjectEntity subject1 = new() // todo seed subject maybe
        {
            Id = Guid.NewGuid()
        };
        SubjectEntity subject2 = new() // todo seed subject maybe
        {
            Id = Guid.NewGuid()
        };

        var firstActivity = new ActivityEntity
        {
            Id = Guid.NewGuid(),
            Start = new DateTime(2023, 10, 8, 9, 0, 0),
            End = new DateTime(2023, 10, 8, 11, 0, 0),
            Place = "Room 201",
            ActivityType = ActivityType.Lecture,
            Description = "First Activity",
            Subject = subject1,
            SubjectId = subject1.Id
        };

        var secondActivity = new ActivityEntity
        {
            Id = Guid.NewGuid(),
            Start = new DateTime(2023, 10, 9, 9, 0, 0),
            End = new DateTime(2023, 10, 9, 11, 0, 0),
            Place = "Room 202",
            ActivityType = ActivityType.Lecture,
            Description = "Second Activity",
            Subject = subject2,
            SubjectId = subject2.Id
        };

        await SchoolSystemDbContextSUT.Activities.AddRangeAsync(new[] { firstActivity, secondActivity });
        await SchoolSystemDbContextSUT.SaveChangesAsync();

        // Act
        var loadedFirstActivity = await SchoolSystemDbContextSUT.Activities.FindAsync(firstActivity.Id);
        var loadedSecondActivity = await SchoolSystemDbContextSUT.Activities.FindAsync(secondActivity.Id);

        // Assert
        Assert.NotNull(loadedFirstActivity);
        Assert.NotNull(loadedSecondActivity);
        DeepAssert.Equal(firstActivity, loadedFirstActivity);
        DeepAssert.Equal(secondActivity, loadedSecondActivity);
    }
}

