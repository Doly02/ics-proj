using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Models;

namespace SchoolSystem.App.ViewModels.Activity;

[QueryProperty(nameof(SubjectId), nameof(SubjectId))]
public partial class ActivityListViewModel(

    IActivityFacade activityFacade,
    INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService),
        IRecipient<ActivityEditMessage>,
        IRecipient<ActivityDeleteMessage>
{
    public IEnumerable<ActivityListModel> Activities { get; set; } = null!;
    public ObservableCollection<ActivityListModel> ObservableActivities { get; set; } = new ObservableCollection<ActivityListModel>();

    public Guid SubjectId { get; set; }
   
    
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Activities = await activityFacade.GetActivitiesAsync(SubjectId);
    }
    
    ///////////////////// Navigates to the ActivityDetailViewModel /////////////////////////////////
    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
        => await navigationService.GoToAsync( "/detail",
            new Dictionary<string, object?>
            {
                [nameof(ActivityDetailViewModel.Id)] = id,
                [nameof(ActivityDetailViewModel.SubjectId)] = SubjectId
            });
    
    
    ///////////////////// Navigates to the ActivityEditViewModel to add new activity /////////////////////////////////
    [RelayCommand]
    private async Task GoToCreateAsync()
    {
        await navigationService.GoToAsync( "/add",
            new Dictionary<string, object?>
            {
                [nameof(ActivityDetailViewModel.SubjectId)] = SubjectId
            });
    }

    
    ////////////////////////////////////// FILTERING ///////////////////////////////////////////////
    public DateTime StartDateFilter { get; set; }
    public TimeSpan StartTimeFilter { get; set; }
    public DateTime EndDateFilter { get; set; }
    public TimeSpan EndTimeFilter { get; set; }
    
    // Command for applying the filter
    [RelayCommand]
    private async Task ApplyFilterAsync()
    {
        var startDateTime = StartDateFilter.Date.Add(StartTimeFilter);
        var endDateTime = EndDateFilter.Date.Add(EndTimeFilter);
        
        var filteredActivities 
            = await activityFacade.FilterActivitiesByTimeAsync(startDateTime, endDateTime, SubjectId);
        ObservableActivities.Clear();
        foreach (var activity in filteredActivities)
        {
            ObservableActivities.Add(activity);
        }
        
        Activities = ObservableActivities;
        OnPropertyChanged(nameof(Activities));
    }
    
    
    ///////////////////////////////////////// SORTING ///////////////////////////////////////////////
    [RelayCommand]
    private async Task SortActivitiesAscendingAsync()
    {
        Activities = await activityFacade.SortActivitiesAscendingAsync(SubjectId);
        OnPropertyChanged(nameof(Activities));
    }

    [RelayCommand]
    private async Task SortActivitiesDescendingAsync()
    {
        Activities = await activityFacade.SortActivitiesDescendingAsync(SubjectId);
        OnPropertyChanged(nameof(Activities));
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////
    
    // Requests navigation to the "/filter" page using the navigation service.
    [RelayCommand]
    private async Task GoToFilterAsync()
    {
        await navigationService.GoToAsync("/filter");
    }
    
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
    
    // After EDIT action
    public async void Receive(ActivityEditMessage message)
    {
        await LoadDataAsync();
    }

    // After DELETE action
    public async void Receive(ActivityDeleteMessage message)
    {
        await LoadDataAsync();
    }
}
