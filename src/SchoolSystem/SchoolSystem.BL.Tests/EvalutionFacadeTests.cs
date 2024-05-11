using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Mappers;
using SchoolSystem.BL.Models;
using SchoolSystem.Common.Tests;
using SchoolSystem.Common.Tests.Seeds;
using SchoolSystem.DAL.Enums;
using SchoolSystem.DAL.Tests;
using System.Collections.ObjectModel;
using Xunit;
using Xunit.Abstractions;

namespace SchoolSystem.BL.Tests;

public class EvaluationFacadeTests : FacadeTestsBase
{
    private readonly IEvaluationFacade _evaluationFacadeSUT;

    public EvaluationFacadeTests(ITestOutputHelper output) : base(output)
    {
        _evaluationFacadeSUT = new EvaluationFacade(UnitOfWorkFactory, new EvaluationModelMapper());
    }


    /*
    [Fact]
    public async Task Create_WithNonexistentActivityAndStudent_ThrowsException()
    {
        // Arrange
        var model = new EvaluationDetailModel
        {
            Id = Guid.NewGuid(), 
            ActivityId = Guid.NewGuid(), 
            StudentId = Guid.NewGuid(), 
            Score = 5, 
            Note = "Good job" 
        };

        // Act & Assert
        await Assert.ThrowsAsync<NullReferenceException>(() => _evaluationFacadeSUT.SaveAsync(model));
    }*/


    [Fact]
    public async Task GetById_FromSeeded_EqualsSeeded()
    {
        var expectedModel = new EvaluationDetailModel
        {
            Id = EvaluationSeeds.EvaluationEntity.Id,
            ActivityId = EvaluationSeeds.EvaluationEntity.ActivityId,
            StudentId = EvaluationSeeds.EvaluationEntity.StudentId,
            Score = EvaluationSeeds.EvaluationEntity.Score,
            Note = EvaluationSeeds.EvaluationEntity.Note
        };

        var returnedModel = await _evaluationFacadeSUT.GetAsync(expectedModel.Id);

        DeepAssert.Equal(expectedModel, returnedModel);
    }



    [Fact]
    public async Task Delete_SubjectUsedInActivity_Throws()
    {
        //Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () => await _evaluationFacadeSUT.DeleteAsync(EvaluationSeeds.EvaluationEntity.Id));
    }

    [Fact]
    public async Task GetEvaluation_NonExistingId_ReturnsNull()
    {
        var nonExistingId = Guid.NewGuid(); 
        var evaluation = await _evaluationFacadeSUT.GetAsync(nonExistingId);
        Assert.Null(evaluation);
    }






}
