using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services;
using SchoolSystem.App.ViewModels.Activity;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Models;

namespace SchoolSystem.App.ViewModels.EnrolledActivity;

public partial class EnrolledActivityListViewModel (

    IActivityFacade activityFacade,
    INavigationService navigationService,
    IMessengerService messengerService, 
    Guid subjectId) 
    : ViewModelBase(messengerService),
        IRecipient<ActivityAddEvalMessage>,
        IRecipient<ActivitySortMessage>
{
    public IEnumerable<ActivityListModel> Activities { get; set; } = null!;
    public ObservableCollection<ActivityListModel> ObservableActivities { get; set; } = new ObservableCollection<ActivityListModel>();
    public Guid SubjectId { get; set; } = subjectId;
   
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
    }
    
    ///////////////////// Navigates to the ActivityDetailViewModel /////////////////////////////////
    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
        => await navigationService.GoToAsync<ActivityDetailViewModel>(
            new Dictionary<string, object?> { [nameof(ActivityDetailViewModel.Id)] = id });
    
    
    ///////////////////// Navigates to the EvaluationDetailViewModel /////////////////////////////////
    [RelayCommand]
    private async Task GoToEvaluationDetailAsync(Guid id)
        => await navigationService.GoToAsync<EvaluationDetailViewModel>(
            new Dictionary<string, object?> { [nameof(EvaluationDetailViewModel.Id)] = id });

    
    [RelayCommand]
    private async Task GoToCreateAsync()
    {
        await navigationService.GoToAsync("/edit");
    }
    
    
    ///////////////////////////////////////// SORTING ///////////////////////////////////////////////
    [RelayCommand]
    private async Task SortActivitiesAscendingAsync()
    {
        ObservableActivities = await activityFacade.SortActivitiesAscendingAsync(SubjectId);
        Activities = ObservableActivities;
        OnPropertyChanged(nameof(Activities));
    }

    [RelayCommand]
    private async Task SortActivitiesDescendingAsync()
    {
        ObservableActivities = await activityFacade.SortActivitiesDescendingAsync(SubjectId);
        Activities = ObservableActivities;
        OnPropertyChanged(nameof(Activities));
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////

    
    /*
    // Requests navigation to the "/addEval" page using the navigation service.
    [RelayCommand]
    private async Task GoToAddAsync()
    {
        await navigationService.GoToAsync("/add");
    }

    // Requests navigation to the "/sort" page using the navigation service.
    [RelayCommand]
    private async Task GoToSortAsync()
    {
        await navigationService.GoToAsync("/sort");
    }
    */
    
    
    //////// These methods are expected to refresh the data, typically after some activity /////////
    
    // After ADD EVALUATION action
    public async void Receive(ActivityAddEvalMessage message)
    {
        await LoadDataAsync();
    }
    
    // After SORT action
    public async void Receive(ActivitySortMessage message)
    {
        await LoadDataAsync();
    }
}