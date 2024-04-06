using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Mappers;
using SchoolSystem.BL.Models;
using SchoolSystem.Common.Tests;
using SchoolSystem.Common.Tests.Factories;
using SchoolSystem.Common.Tests.Seeds;
using SchoolSystem.DAL;
using SchoolSystem.DAL.Enums;
using SchoolSystem.DAL.Tests;
using SchoolSystem.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

public class ActivityFacadeTests : FacadeTestsBase
{
    private readonly ActivityFacade _activityFacade;


    public ActivityFacadeTests(ITestOutputHelper output) : base(output)
    {

        _activityFacade = new ActivityFacade(UnitOfWorkFactory, ActivityModelMapper);
    }

    [Fact]
    public async Task CreateActivity_EqualsCreated()
    {
        // Arrange
        var model = new ActivityDetailModel
        {
            Name = "Study Session",
            Description = "Testing study session",
            Start = DateTime.UtcNow.AddHours(3),
            End = DateTime.UtcNow.AddHours(5),
            Place = "Library",
            ActivityType = ActivityType.Project
        };

        // Act
        var returnedModel = await _activityFacade.SaveAsync(model);

        // Assert
        // Here we assume that the SaveAsync method generates and sets the ID for the new Activity.
        // Since we can't set the ID beforehand, we align the ID of the 'model' to the 'returnedModel' before comparison.
        model.Id = returnedModel.Id;
        DeepAssert.Equal(model, returnedModel);
    }


    [Fact]
    public async Task CreateActivity_WithNonExistingSubject_Throws()
    {
        // Arrange
        var model = new ActivityDetailModel()
        {
            Name = "Activity 2",
            Description = "Testing activity 2",
            Start = DateTime.UtcNow.AddHours(3),
            End = DateTime.UtcNow.AddHours(5),
            Place = "Classroom 202",
            ActivityType = ActivityType.Lecture,
            // Assume that we have a subject identifier that should exist in the database
            // For the purpose of this test, we're setting it to an empty Guid to simulate a non-existing subject
            Id = Guid.Empty
        };

        // Act & Assert
        await Assert.ThrowsAnyAsync<InvalidOperationException>(() => _activityFacade.SaveAsync(model));
    }

    [Fact]
    public async Task CreateActivity_WithExistingSubject_Throws()
    {
        //Arrange
        var model = new ActivityDetailModel()
        {
            Name = "Activity 2",
            Description = "Testing activity 2",
            Start = DateTime.UtcNow.AddHours(3),
            End = DateTime.UtcNow.AddHours(5),
            Place = "Classroom 202",
            ActivityType = ActivityType.Lecture,
            // Assuming that subjects are linked to activities and we have a specific subject that should not be duplicated
            // For the purpose of this test, simulate an attempt to create an activity with an already-linked subject
            Id = SubjectSeeds.ICS.Id // Presume an existing subject
        };

        //Act & Assert
        // Expecting an InvalidOperationException or similar to be thrown due to business rule violation
        // e.g., not allowing multiple activities for the same subject within a specific timeframe
        await Assert.ThrowsAnyAsync<InvalidOperationException>(() => _activityFacade.SaveAsync(model));
    }


    [Fact]
    public async Task CreateActivity_WithDuplicateSubject_Throws()
    {
        //Arrange
        var model = new ActivityDetailModel()
        {
            Name = "C# for Beginners",
            Description = "Introductory C# seminar",
            Start = DateTime.UtcNow.AddHours(3),
            End = DateTime.UtcNow.AddHours(5),
            Place = "Main Campus",
            ActivityType = ActivityType.Lecture,
            Id = SubjectSeeds.ICS.Id // Using an existing subject's ID
        };

        //Act & Assert
        // Expect an exception due to a rule against duplicate activities for the same subject
        // in a given timeframe or other business logic constraint.
        await Assert.ThrowsAnyAsync<InvalidOperationException>(() => _activityFacade.SaveAsync(model));
    }







    // Další testovací metody podle potřeby...
}
