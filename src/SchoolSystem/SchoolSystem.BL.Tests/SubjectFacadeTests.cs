using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Mappers;
using SchoolSystem.BL.Models;
using SchoolSystem.Common.Tests;
using SchoolSystem.Common.Tests.Seeds;
using SchoolSystem.DAL.Tests;
using System.Collections.ObjectModel;
using Xunit;
using Xunit.Abstractions;

namespace SchoolSystem.BL.Tests;

public sealed class SubjectFacadeTests : FacadeTestsBase
{
    private readonly ISubjectFacade _facadeSUT;

    public SubjectFacadeTests(ITestOutputHelper output) : base(output)
    {
        _facadeSUT = new SubjectFacade(UnitOfWorkFactory, SubjectModelMapper);
    }

    [Fact]
    public async Task GetAll_FromSeeded_ContainsSeeded()
    {
        //Arrange
        var listModel = SubjectModelMapper.MapToListModel(SubjectSeeds.ICS);

        //Act
        var returnedModel = await _facadeSUT.GetAsync();

        //Assert
        Assert.Contains(listModel, returnedModel);
    }

    [Fact]
    public async Task GetAll_Single_SeededICS()
    {
        var subjects = await _facadeSUT.GetAsync();
        var subject =subjects.Single(i => i.Id == SubjectSeeds.ICS.Id);

        DeepAssert.Equal(SubjectModelMapper.MapToListModel(SubjectSeeds.ICS), subject);
    }

    [Fact]
    public async Task DeleteById_FromSeeded_DoesNotThrow()
    {
        //Arrange & Act & Assert
        await _facadeSUT.DeleteAsync(SubjectSeeds.ICS.Id);
    }

    [Fact]
    public async Task DeleteById_FromSeeded_Deleted()
    {
        // Act - Delete the subject
        await _facadeSUT.DeleteAsync(SubjectSeeds.ICS.Id);

        // Assert - Verify the subject is no longer in the list
        var postDeleteSubjects = await _facadeSUT.GetAsync();
        Assert.DoesNotContain(postDeleteSubjects, s => s.Id == SubjectSeeds.ICS.Id);
    }
}
