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
using Microsoft.VisualStudio.TestPlatform.Utilities;

public sealed class ActivityFacadeTests : FacadeTestsBase
{
    private readonly IActivityFacade _activityFacadeSUT;
    private readonly ITestOutputHelper _output;


    public ActivityFacadeTests(ITestOutputHelper output) : base(output)
    {

        _activityFacadeSUT = new ActivityFacade(UnitOfWorkFactory, ActivityModelMapper);
        _output = output;
    }

    [Fact]
    public async Task CreateActivity_WithExistingSubject_DoesNotThrow()
    {

        var subjectId = SubjectSeeds.ICS.Id;

        var model = new ActivityDetailModel
        {

            Name = "New study group",
            Description = "Prepare for math",
            Start = DateTime.UtcNow.AddDays(1),
            End = DateTime.UtcNow.AddDays(1).AddHours(2),
            Place = "Library, room 1001",
            ActivityType = ActivityType.Project
        };

        Exception exception = await Record.ExceptionAsync(() =>
            _activityFacadeSUT.SaveAsync(model, subjectId));

        Assert.Null(exception);
    }




    [Fact]
    public async Task CreateActivity_WithValidData_DoesNotThrow()
    {
        var model = new ActivityDetailModel
        {
            Id = Guid.Empty, 
            Name = "New Study Session",
            Description = "Description of the study session",
            Start = DateTime.UtcNow.AddDays(1),
            End = DateTime.UtcNow.AddDays(1).AddHours(2),
            Place = "Library Room 101",
            ActivityType = ActivityType.Project,
            SubjectName = "Mathematics" 
        };

        var exception = await Record.ExceptionAsync(() => _activityFacadeSUT.SaveAsync(model, SubjectSeeds.ICS.Id));

        Assert.Null(exception);
    }

    [Fact]
    public async Task GetAll_Single_SeededActivity()
    {
        var listModel = ActivityModelMapper.MapToDetailModel(ActivitySeeds.ActivityEntity);
        var returnedModel = await _activityFacadeSUT.GetAsync(listModel.Id);

        DeepAssert.Equal(listModel, returnedModel);
    }

    [Fact]
    public async Task GetById_SeededActivity()
    {
        var activity = await _activityFacadeSUT.GetAsync(ActivitySeeds.ActivityEntity.Id);

        DeepAssert.Equal(ActivityModelMapper.MapToDetailModel(ActivitySeeds.ActivityEntity), activity);
    }

    [Fact]
    public async Task GetById_NonExistent()
    {
        var activity = await _activityFacadeSUT.GetAsync(Guid.NewGuid()); 

        Assert.Null(activity);
    }

    [Fact]
    public async Task SeededActivity_DeleteById_Deleted()
    {
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await _activityFacadeSUT.DeleteAsync(ActivitySeeds.ActivityEntity.Id));

    }


}
