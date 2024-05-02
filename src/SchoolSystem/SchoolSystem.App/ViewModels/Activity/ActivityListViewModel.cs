using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Models;

namespace SchoolSystem.App.ViewModels.Activity;

public partial class ActivityListViewModel (

    IActivityFacade activityFacade,
    INavigationService navigationService,
    IMessengerService messengerService) 
    : ViewModelBase(messengerService),
        IRecipient<ActivityAddMessage>,
        IRecipient<ActivityDeleteMessage>,
        IRecipient<ActivitySortMessage>,
        IRecipient<ActivityFilterMessage>
{
    public IEnumerable<ActivityListModel> Activities { get; set; } = null!;
    public ObservableCollection<ActivityListModel> ObservableActivities { get; set; } = new ObservableCollection<ActivityListModel>();

   
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
    }
    
    ///////////////////// Navigates to the ActivityDetailViewModel /////////////////////////////////
    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
        => await navigationService.GoToAsync<ActivityDetailViewModel>(
            new Dictionary<string, object?> { [nameof(ActivityDetailViewModel.Id)] = id });
    
    
    [RelayCommand]
    private async Task GoToCreateAsync()
    {
        await navigationService.GoToAsync("/edit");
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
            = await activityFacade.FilterActivitiesByTimeAsync(startDateTime, endDateTime);
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
        ObservableActivities = await activityFacade.SortActivitiesAscendingAsync();
        Activities = ObservableActivities;
        OnPropertyChanged(nameof(Activities));
    }

    [RelayCommand]
    private async Task SortActivitiesDescendingAsync()
    {
        ObservableActivities = await activityFacade.SortActivitiesDescendingAsync();
        Activities = ObservableActivities;
        OnPropertyChanged(nameof(Activities));
    }
    
    ////////////////////////////////////////////////////////////////////////////////////////////////

   // Requests navigation to the "/add" page using the navigation service.
    [RelayCommand]
    private async Task GoToAddAsync()
    {
        await navigationService.GoToAsync("/add");
    }

    // Requests navigation to the "/remove" page using the navigation service.
    [RelayCommand]
    private async Task GoToRemoveAsync()
    {
        await navigationService.GoToAsync("/remove");
    }
    
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
    
    //////// These methods are expected to refresh the data, typically after some activity /////////
    
    // After ADD action
    public async void Receive(ActivityAddMessage message)
    {
        await LoadDataAsync();
    }

    // After DELETE action
    public async void Receive(ActivityDeleteMessage message)
    {
        await LoadDataAsync();
    }
    
    // After FILTER action
    public async void Receive(ActivityFilterMessage message)
    {
        await LoadDataAsync();
    }
    
    // After SORT action
    public async void Receive(ActivitySortMessage message)
    {
        await LoadDataAsync();
    }
}
