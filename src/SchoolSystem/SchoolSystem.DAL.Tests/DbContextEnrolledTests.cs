using SchoolSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;
using SchoolSystem.DAL.Enums;

namespace SchoolSystem.DAL.Tests;

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
            Student = student,
            Subject = subject,
            SubjectId = subject.Id,
            StudentId = student.Id,
            Id = Guid.NewGuid()
        };

        //Act
        SchoolSystemDbContextSUT.Enrolleds.Add(entity);
        await SchoolSystemDbContextSUT.SaveChangesAsync();

        //Assert
        await using var context = await DbContextFactory.CreateDbContextAsync();
        var actualEntities = await context.Enrolleds
            .SingleAsync(i => i.StudentId == entity.StudentId && i.SubjectId == entity.SubjectId);
        DeepAssert.Equal(entity, actualEntities);
    }

    [Fact]
    public async Task GetEnrolledById_EnrolledRetrieved()
    {

        var student = new StudentEntity { Id = Guid.NewGuid(), Name = "Peter", Surname = "Parker" };
        var subject = new SubjectEntity { Id = Guid.NewGuid(), Name = "ICS" };
        // Arrange
        var enrolled = new EnrolledEntity
        {

            Student = student,
            Subject = subject,
            StudentId = student.Id,
            SubjectId = subject.Id,
            Id = Guid.NewGuid()
        };

        SchoolSystemDbContextSUT.Enrolleds.Add(enrolled);
        await SchoolSystemDbContextSUT.SaveChangesAsync();

        // Act
        await using var context = await DbContextFactory.CreateDbContextAsync();
        var retrieved = await context.Enrolleds
            .FirstOrDefaultAsync(e => e.StudentId == enrolled.StudentId && e.SubjectId == enrolled.SubjectId);

        // Assert
        Assert.NotNull(retrieved);
        DeepAssert.Equal(enrolled, retrieved);
    }

    [Fact]
    public async Task UpdateEnrolled_EnrolledUpdated()
    {
        // Arrange
        var student = new StudentEntity { Id = Guid.NewGuid(), Name = "Peter", Surname = "Parker" };
        var oldSubject = new SubjectEntity { Id = Guid.NewGuid(), Name = "ICS" };
        var newSubject = new SubjectEntity { Id = Guid.NewGuid(), Name = "IW5" };
        var enrolled = new EnrolledEntity
        {
            Student = student,
            Subject = oldSubject,
            StudentId = student.Id,
            SubjectId = oldSubject.Id,
            Id = Guid.NewGuid()
        };

        SchoolSystemDbContextSUT.Students.Add(student);
        SchoolSystemDbContextSUT.Subjects.Add(oldSubject);
        SchoolSystemDbContextSUT.Subjects.Add(newSubject);
        SchoolSystemDbContextSUT.Enrolleds.Add(enrolled);
        await SchoolSystemDbContextSUT.SaveChangesAsync();

        // Act
        SchoolSystemDbContextSUT.Enrolleds.Remove(enrolled);
        await SchoolSystemDbContextSUT.SaveChangesAsync();

        var updatedEnrolled = new EnrolledEntity
        {
            Student = student,
            Subject = newSubject,
            StudentId = student.Id,
            SubjectId = newSubject.Id,
            Id = Guid.NewGuid()
        };
        SchoolSystemDbContextSUT.Enrolleds.Add(updatedEnrolled);
        await SchoolSystemDbContextSUT.SaveChangesAsync();

        // Assert
        await using var context = await DbContextFactory.CreateDbContextAsync();
        var updated = await context.Enrolleds
            .Include(e => e.Student)
            .Include(e => e.Subject)
            .FirstOrDefaultAsync(e => e.StudentId == updatedEnrolled.StudentId && e.SubjectId == updatedEnrolled.SubjectId);

        DeepAssert.Equal(updatedEnrolled, updated);
    }

    [Fact]
    public async Task DeleteEnrolled_EnrolledDeleted()
    {
        // Arrange
        var student = new StudentEntity { Id = Guid.NewGuid(), Name = "Peter", Surname = "Parker" };
        var subject = new SubjectEntity { Id = Guid.NewGuid(), Name = "ICS" };
        var enrolled = new EnrolledEntity
        {
            Student = student,
            Subject = subject,
            StudentId = student.Id,
            SubjectId = subject.Id,
            Id = Guid.NewGuid()
        };

        SchoolSystemDbContextSUT.Enrolleds.Add(enrolled);
        await SchoolSystemDbContextSUT.SaveChangesAsync();

        // Act
        SchoolSystemDbContextSUT.Enrolleds.Remove(enrolled);
        await SchoolSystemDbContextSUT.SaveChangesAsync();

        // Assert
        await using var context = await DbContextFactory.CreateDbContextAsync();
        var deleted = await context.Enrolleds
            .FirstOrDefaultAsync(e => e.StudentId == enrolled.StudentId && e.SubjectId == enrolled.SubjectId);
        Assert.Null(deleted);
    }

    [Fact]
    public async Task Enrolled_HasCorrectRelationships()
    {
        // Arrange
        var student = new StudentEntity { Id = Guid.NewGuid(), Name = "Bruce", Surname = "Wayne" };
        var subject = new SubjectEntity { Id = Guid.NewGuid(), Name = "Biology" };
        var enrolled = new EnrolledEntity
        {
            Student = student,
            Subject = subject,
            StudentId = student.Id,
            SubjectId = subject.Id,
            Id = Guid.NewGuid()
        };

        SchoolSystemDbContextSUT.Students.Add(student);
        SchoolSystemDbContextSUT.Subjects.Add(subject);
        SchoolSystemDbContextSUT.Enrolleds.Add(enrolled);
        await SchoolSystemDbContextSUT.SaveChangesAsync();

        // Act
        var retrievedEnrolled = await SchoolSystemDbContextSUT.Enrolleds
            .Include(e => e.Student)
            .Include(e => e.Subject)
            .FirstOrDefaultAsync(e => e.StudentId == enrolled.StudentId && e.SubjectId == enrolled.SubjectId);

        // Assert
        Assert.NotNull(retrievedEnrolled);
        DeepAssert.Equal(enrolled.Student, retrievedEnrolled.Student);
        DeepAssert.Equal(enrolled.Subject, retrievedEnrolled.Subject);
    }

    [Fact]
    public async Task AddTwoEnrolledsWithSameId_ThrowsException()
    {
        // Arrange
        var sharedId = Guid.NewGuid();
        var student = new StudentEntity { Id = Guid.NewGuid(), Name = "Clark", Surname = "Kent" };
        var subject = new SubjectEntity { Id = Guid.NewGuid(), Name = "Journalism" };

        var enrolled1 = new EnrolledEntity
        {
            Student = student,
            Subject = subject,
            StudentId = student.Id,
            SubjectId = subject.Id,
            Id = Guid.NewGuid()
        };

        var enrolled2 = new EnrolledEntity
        {
            Student = new StudentEntity
            {
                Id = Guid.NewGuid(),
                Name = "Lois",
                Surname = "Lane"
            },
            Subject = new SubjectEntity
            {
                Id = Guid.NewGuid(),
                Name = "Photography"
            },
            StudentId = student.Id,
            SubjectId = subject.Id,
            Id = Guid.NewGuid()
        };

        SchoolSystemDbContextSUT.Enrolleds.Add(enrolled1);
        await SchoolSystemDbContextSUT.SaveChangesAsync();

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
        {
            SchoolSystemDbContextSUT.Enrolleds.Add(enrolled2);
            return SchoolSystemDbContextSUT.SaveChangesAsync();
        });
    }
}
