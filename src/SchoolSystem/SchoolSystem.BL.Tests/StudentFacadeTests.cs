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

public class StudentFacadeTests : FacadeTestsBase
{
    private readonly IStudentFacade _facadeSUT;

    public StudentFacadeTests(ITestOutputHelper output) : base(output)
    {
        _facadeSUT = new StudentFacade(UnitOfWorkFactory, StudentModelMapper);
    }

    [Fact]
    public async Task Create_WithoutEnrolledSubjects_EqualsCreated()
    {
        //Arrange
        var model = new StudentDetailModel()
        {
            Name = "Marco",
            Surname = "Polo",
            EnrolledSubjects = new ObservableCollection<EnrolledSubjectsListModel>()
        };

        //Act
        var returnedModel = await _facadeSUT.SaveAsync(model);

        //Assert
        FixIds(model, returnedModel);
        DeepAssert.Equal(model, returnedModel);
    }

    [Fact]
    public async Task Create_WithNonExistingEnrolledSubject_Throws()
    {
        //Arrange
        var model = new StudentDetailModel()
        {
            Name = "Petr",
            Surname = "Petr",
            EnrolledSubjects = new ObservableCollection<EnrolledSubjectsListModel>()
            {
                new()
                {
                    Name = "Diskrétní matematika",
                    Abbreviation = "IDM",
                    StudentFullName = "Petr",
                    Activities = new ObservableCollection<ActivityListModel>()
                }
            }
        };

        //Act & Assert
        await Assert.ThrowsAnyAsync<InvalidOperationException>(() => _facadeSUT.SaveAsync(model));
    }

    [Fact]
    public async Task Create_WithExistingAndNotExistingEnrolledSubject_Throws()
    {
        var enrolledModelMapper = new EnrolledModelMapper();

        //Arrange
        var model = new StudentDetailModel
        {
            Name = "Petr",
            Surname = "Petr",
            EnrolledSubjects =
            [
                new EnrolledSubjectsListModel
                {
                    Name = "Diskrétní matematika",
                    Abbreviation = "IDM",
                    StudentFullName = "Petr Petr",
                    Activities = new ObservableCollection<ActivityListModel>()
                },
                
                enrolledModelMapper.MapToListModel(EnrolledSeeds.EnrolledEntity)
            ]
        };

        //Act & Assert
        await Assert.ThrowsAnyAsync<InvalidOperationException>(() => _facadeSUT.SaveAsync(model));
    }

    [Fact]
    public async Task GetById_FromSeeded_EqualsSeeded()
    {
        //Arrange
        var detailModel = StudentModelMapper.MapToDetailModel(StudentSeeds.StudentEntity1);

        //Act
        var returnedModel = await _facadeSUT.GetAsync(detailModel.Id);

        //Assert
        DeepAssert.Equal(detailModel, returnedModel);
    }

    [Fact]
    public async Task GetAll_FromSeeded_ContainsSeeded()
    {
        //Arrange
        var listModel = StudentModelMapper.MapToListModel(StudentSeeds.StudentEntity1);

        //Act
        var returnedModel = await _facadeSUT.GetAsync();

        //Assert
        Assert.Contains(listModel, returnedModel);
    }

    [Fact]
    public async Task Update_Name_FromSeeded_Updated()
    {
        //Arrange
        var detailModel = StudentModelMapper.MapToDetailModel(StudentSeeds.StudentEntity1);
        detailModel.Name = "Hannibal";

        //Act
        await _facadeSUT.SaveAsync(detailModel);

        //Assert
        var returnedModel = await _facadeSUT.GetAsync(detailModel.Id);
        DeepAssert.Equal(detailModel, returnedModel);
    }

    [Fact]
    public async Task DeleteById_FromSeeded_DoesNotThrow()
    {
        //Arrange & Act & Assert
        await _facadeSUT.DeleteAsync(StudentSeeds.StudentEntity1.Id);
    }

    // Helper method to synchronize Ids between expected and actual models.
    private static void FixIds(StudentDetailModel expectedModel, StudentDetailModel returnedModel)
    {
        returnedModel.Id = expectedModel.Id;

        foreach (var enrolledSubjectModel in returnedModel.EnrolledSubjects)
        {
            var matchingEnrolledSubject = expectedModel.EnrolledSubjects.FirstOrDefault(es =>
                es.Name == enrolledSubjectModel.Name &&
                es.Abbreviation == enrolledSubjectModel.Abbreviation &&
                es.StudentFullName == enrolledSubjectModel.StudentFullName);

            if (matchingEnrolledSubject != null)
            {
                enrolledSubjectModel.Id = matchingEnrolledSubject.Id;

                foreach (var activityModel in enrolledSubjectModel.Activities)
                {
                    var matchingActivity = matchingEnrolledSubject.Activities.FirstOrDefault(a =>
                        a.ActivityName == activityModel.ActivityName &&
                        a.SubjectName == activityModel.SubjectName);

                    if (matchingActivity != null)
                    {
                        activityModel.Id = matchingActivity.Id;
                    }
                }
            }
        }
    }
}
