using CommunityToolkit.Mvvm.Input;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Mappers;
using SchoolSystem.BL.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace SchoolSystem.App.ViewModels;

[QueryProperty(nameof(Student), nameof(Student))]
public partial class EnrolledEditViewModel(
    ISubjectFacade subjectFacade,
    IEnrolledFacade enrolledFacade,
    EnrolledModelMapper enrolledModelMapper,
    INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService)
{
    public StudentDetailModel? Student { get; set; }


    public ObservableCollection<SubjectListModel> AvailableSubjects { get; set; } = new();

    public SubjectListModel? SelectedSubject { get; set; }
    public EnrolledSubjectsListModel? NewEnrollment { get; private set; }


    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        AvailableSubjects.Clear();
        var subjects = await subjectFacade.GetAsync();



        foreach (var subject in subjects)
        {
            AvailableSubjects.Add(subject);
            NewEnrollment = GetNewEnrollment();
        } 
    }

    [RelayCommand]
    private async Task AddSubjectToStudentAsync()
    {
        if (NewEnrollment is not null && SelectedSubject is not null && Student is not null)
        {
            enrolledModelMapper.MapToExistingListModel(NewEnrollment, SelectedSubject);

            await enrolledFacade.SaveAsync(NewEnrollment, Student.Id);

            Student.EnrolledSubjects.Add(enrolledModelMapper.MapToListModel(NewEnrollment));

            NewEnrollment = GetNewEnrollment();

            MessengerService.Send(new EnrolledAddMessage());

            await navigationService.GoToAsync("//student/detail/enrolled",
                new Dictionary<string, object?>
                 { [nameof(EnrolledListViewModel.Student)] = Student });
        }
    }

    [RelayCommand]
    public async Task GoBackToListAsync()
    {
        await navigationService.GoToAsync("//student/detail/enrolled",
        new Dictionary<string, object?>
        { [nameof(EnrolledListViewModel.Student)] = Student });
    }


    private EnrolledSubjectsListModel GetNewEnrollment()
    {
        var subjectFirst = AvailableSubjects.First();
        return new()
        {
            Id = Guid.NewGuid(),
            Name = subjectFirst.Name,
            SubjectId = subjectFirst.Id,
            Abbreviation = subjectFirst.Abbreviation,
            Activities = subjectFirst.Activities
        };
    }

}
