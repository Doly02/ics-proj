using SchoolSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;
using SchoolSystem.DAL.Tests;
using Xunit.Abstractions;
using SchoolSystem.DAL.Enums;

namespace SchoolSystem.DAL
{
    public class DbContextEvaluationTests : DbContextTestsBase
    {
        public DbContextEvaluationTests(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public async Task AddNew_Evaluation_Persisted()
        {
            // Arrange
            var studentId = Guid.NewGuid();
            var activityId = Guid.NewGuid();
            var subjectId = Guid.NewGuid();
            var evaluation = new EvaluationEntity
            {
                Score = 95,
                Note = "Excellent work",
                StudentId = studentId,
                ActivityId = activityId,
                // Directly using the initialized Student and Activity entities based on their updated structures
                Student = new StudentEntity { Id = studentId, Name = "John", Surname = "Doe", ImageUrl = null }, // Assuming ImageUrl can be null if not provided
                Activity = new ActivityEntity
                {
                    Id = activityId,
                    Start = DateTime.Now,
                    End = DateTime.Now.AddHours(1), // For example, 1 hour later
                    Place = "101 Classroom",
                    ActivityType = ActivityType.Lecture, // Assuming ActivityType.Lecture is a valid enum option
                    Description = "Introduction to Mathematics",
                    // Direct initialization of SubjectEntity based on its structure
                    Subject = new SubjectEntity
                    {
                        Id = subjectId,
                        Name = "Mathematics",
                      
                        // Assuming these are the required properties based on the SubjectEntity definition
                    }
                }
            };

            // Act
            SchoolSystemDbContextSUT.Evaluations.Add(evaluation);
            await SchoolSystemDbContextSUT.SaveChangesAsync();

            // Assert
            await using var dbx = await DbContextFactory.CreateDbContextAsync();
            var actualEntity = await dbx.Evaluations
                .Include(e => e.Student)
                .ThenInclude(s => s.Evaluations)
                .Include(e => e.Student)
                .ThenInclude(s => s.Enrolleds)
                .Include(e => e.Activity)
                .ThenInclude(a => a.Subject)
                .SingleAsync(e => e.StudentId == studentId && e.ActivityId == activityId);
            DeepAssert.Equal(evaluation, actualEntity);
        }

        // Additional tests go here
    }
}
