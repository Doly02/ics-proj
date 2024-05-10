using System.Diagnostics;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Models;
using SchoolSystem.DAL.Enums;

namespace SchoolSystem.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
[QueryProperty(nameof(SubjectId), nameof(SubjectId))]

public partial class ActivityEditViewModel(
    IActivityFacade activityFacade,
    INavigationService navigationService,
    IMessengerService messengerService,
    IAlertService alertService)
    : ViewModelBase(messengerService)
{
    public ActivityDetailModel? Activity { get; private set; }
    public Guid SubjectId { get; set; }
    public Guid Id { get; set; }

    public DateTime StartDate { get; set; } = DateTime.Today;
    public TimeSpan StartTime { get; set; } = TimeSpan.Zero;
    
    public DateTime EndDate { get; set; } = DateTime.Today;
    public TimeSpan EndTime { get; set; } = TimeSpan.Zero;
    
    public ActivityType SelectedActivityType { get; set; }
    public List<ActivityType> ActivityTypes { get; } = Enum.GetValues(typeof(ActivityType)).Cast<ActivityType>().ToList();

    
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        
        if (Id != Guid.Empty)
        {
            // Load the existing activity if ID is provided
            Activity = await activityFacade.GetAsync(Id);
            if (Activity != null)
            {
                StartDate = Activity.Start.Date;
                StartTime = Activity.Start.TimeOfDay;
                EndDate = Activity.End.Date;
                EndTime = Activity.End.TimeOfDay;
                SelectedActivityType = Activity.ActivityType;
            }
        }
        else
        {
            // Create a new activity if ID is not provided
            Activity = new ActivityDetailModel
            {
                Start = StartDate + StartTime,
                End = EndDate + EndTime,
                ActivityType = ActivityType.Other
            };
            SelectedActivityType = ActivityType.Other;
        }
    }
    
    // SAVE
    [RelayCommand]
    private async Task SaveAsync()
    {
        if (Activity != null)
        {
            DateTime StartDateTime = StartDate.Date + StartTime;
            DateTime EndDateTime = EndDate.Date + EndTime;
            
            if (StartDateTime <= EndDateTime)
            {
                Activity.Start = StartDateTime;
                Activity.End = EndDateTime;
                Activity.ActivityType = SelectedActivityType;
            
                await activityFacade.SaveAsync(Activity, SubjectId);
                MessengerService.Send(new ActivityEditMessage { ActivityId = Activity.Id });
                navigationService.SendBackButtonPressed();
            }
            else
            {
                await alertService.DisplayAsync("Operation Failed",
                    "Start date must be before End date.");
            }
        }
    }

    [RelayCommand]
    private async Task BackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}
