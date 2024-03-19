using SchoolSystem.Common.Tests.Seeds;
using SchoolSystem.DAL.Entities;
using SchoolSystem.DAL.Enums;
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
            Id = Guid.NewGuid(),
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

    [Fact]
    public async Task AddNewStudent_WithEnrolled_Persisted()
    {
        //Arrange
        StudentEntity entity = new()
        {
            Id = Guid.NewGuid(),
            Name = "John",
            Surname = "Doe",
            ImageUrl = null,
            Enrolleds = new List<EnrolledEntity>(),
            Evaluations = new List<EvaluationEntity>()
        };
        SubjectEntity subject = new() { Id = Guid.NewGuid() };
        EnrolledEntity enrolled = new EnrolledEntity()
        {
            StudentId = entity.Id,
            Subject = subject,
            SubjectId = subject.Id,
            Student = entity,
        };
        entity.Enrolleds.Add(enrolled);
        
        //Act
        SchoolSystemDbContextSUT.Students.Add(entity);
        SchoolSystemDbContextSUT.Subjects.Add(subject);
        SchoolSystemDbContextSUT.Enrolleds.Add(enrolled);
        await SchoolSystemDbContextSUT.SaveChangesAsync();
        
         //Assert
         await using var dbx = await DbContextFactory.CreateDbContextAsync();
         var actualStudent = await dbx.Students.SingleAsync(i => i.Id == entity.Id);
         var actualEnrolled = await dbx.Enrolleds.SingleAsync(i => i.StudentId == enrolled.StudentId);
         DeepAssert.Equal(entity, actualStudent);
         Assert.True(entity.Enrolleds.Contains(enrolled));
         DeepAssert.Equal(entity.Enrolleds.First(), actualEnrolled);
         DeepAssert.Equal(actualEnrolled.Student, entity);
    }
    
    [Fact]
    public async Task AddNewStudent_WithEvaluation_Persisted()
    {
        //Arrange
        StudentEntity student = new()
        {
            Id = Guid.NewGuid(),
            Name = "John",
            Surname = "Doe",
            ImageUrl = null,
            Enrolleds = new List<EnrolledEntity>(),
            Evaluations = new List<EvaluationEntity>()
        };
        SubjectEntity subject = new() { Id = Guid.NewGuid() };
        ActivityEntity activity = new()
        {
            Id = Guid.NewGuid(), 
            Subject = subject,
            SubjectId = subject.Id,
            ActivityType = ActivityType.Other
        };
        EvaluationEntity evaluation = new EvaluationEntity()
        {
            StudentId = student.Id,
            Student = student,
            ActivityId = activity.Id,
            Activity = activity
        };
        student.Evaluations.Add(evaluation);
        
        //Act
        SchoolSystemDbContextSUT.Students.Add(student);
        SchoolSystemDbContextSUT.Subjects.Add(subject);
        SchoolSystemDbContextSUT.Activities.Add(activity);
        SchoolSystemDbContextSUT.Evaluations.Add(evaluation);
        await SchoolSystemDbContextSUT.SaveChangesAsync();
        
        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualStudent = await dbx.Students.SingleAsync(i => i.Id == student.Id);
        var actualEvaluation = await dbx.Evaluations.SingleAsync(i => i.StudentId == student.Id);
        Assert.True(student.Evaluations.Contains(evaluation));
        DeepAssert.Equal(student.Evaluations.First(), actualEvaluation);
        DeepAssert.Equal(actualEvaluation.Student, student);
    }
    
}
