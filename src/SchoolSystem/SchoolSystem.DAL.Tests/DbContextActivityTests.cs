//using SchoolSystem.Common.Tests.Seeds;
using SchoolSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace  SchoolSystem.DAL.Tests;

public class DbContextActivityTests(ITestOutputHelper output) : DbContextTestsBase(output)
{
    [Fact]
    public async Task_AddNewActivity_Per()
    {
        //Arrange
        ActivityEntity entity = new()
        {
            Id = Guid.Parse("C5DE45D7-64A0-4E8D-AC7F-BF5CFDFB0EFC"),
            Start = new DateTime(2023, 10, 1, 14, 0, 0),    // Set Start Date
            End = new DateTime(2023, 10, 1, 16, 0, 0),      // Set End Date
            Place = "101B", 
            ActivityType = ActivityType.Lecture, 
            Description = "Introduction to Programming" 
        };

        //Act
        SchoolSystemDbContextSUT.Ingredients.Add(entity);
        await SchoolSystemDbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntities = await dbx.Activities.SingleAsync(i => i.Id == entity.Id);
        Assert.Equal(entity, actualEntities);
    }
}

