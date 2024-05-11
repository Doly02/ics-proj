using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Models;

namespace SchoolSystem.App.ViewModels;

[QueryProperty(nameof(StudentName), nameof(StudentName))]
[QueryProperty(nameof(SubjectId), nameof(SubjectId))]
public partial class EnrolledActivityListViewModel (

    IActivityFacade activityFacade,
    INavigationService navigationService,
    IMessengerService messengerService) 
    : ViewModelBase(messengerService),
        IRecipient<ActivityEditMessage>
{
    public IEnumerable<ActivityListModel> EnrolledActivities { get; set; } = null!;    // asi treba zmenit na StudentActivityListModel
    public Guid SubjectId { get; set; }
    public string? StudentName { get; set; }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        EnrolledActivities = await activityFacade.GetActivitiesAsync(SubjectId);   // tu nebude fungovat GetActivitiesAsync ... treba asi vytvorit novu fasadu
    }
    
    ///////////////////// Navigates to the ActivityDetailViewModel /////////////////////////////////
    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
        => await navigationService.GoToAsync( "/detailActivity",
            new Dictionary<string, object?>
            {
                [nameof(ActivityDetailViewModel.Id)] = id,
                [nameof(ActivityDetailViewModel.SubjectId)] = SubjectId,
            });
    
    ///////////////////// Navigates to the EvaluationDetailViewModel /////////////////////////////////
    [RelayCommand]
    private async Task GoToEvaluationDetailAsync(Guid id)
    {
        await navigationService.GoToAsync("/evaluation",
            new Dictionary<string, object?>
            {
                [nameof(EvaluationDetailViewModel.Id)] = id,
                [nameof(ActivityDetailViewModel.SubjectId)] = SubjectId
            });
    }
    
    ///////////////////////////////////////// SORTING ///////////////////////////////////////////////
    [RelayCommand]
    private async Task SortActivitiesAscendingAsync()
    {
        EnrolledActivities = await activityFacade.SortActivitiesAscendingAsync(SubjectId);
        OnPropertyChanged(nameof(EnrolledActivities));
    }

    [RelayCommand]
    private async Task SortActivitiesDescendingAsync()
    {
        EnrolledActivities = await activityFacade.SortActivitiesDescendingAsync(SubjectId);
        OnPropertyChanged(nameof(EnrolledActivities));
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////
    
    // Requests navigation to the "/sort" page using the navigation service.
    [RelayCommand]
    private async Task GoToSortAsync()
    {
        await navigationService.GoToAsync("/sort");
    }
    
    [RelayCommand]
    private async Task BackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
    
    
    //////// These methods are expected to refresh the data, typically after some activity /////////
    
    // After ADD EVALUATION action
    public async void Receive(ActivityEditMessage message)
    {
        await LoadDataAsync();
    }
}
