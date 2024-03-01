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
}
