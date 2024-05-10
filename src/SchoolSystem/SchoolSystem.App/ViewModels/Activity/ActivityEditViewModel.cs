using System.Diagnostics;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Models;

namespace SchoolSystem.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
[QueryProperty(nameof(SubjectId), nameof(SubjectId))]

public partial class ActivityEditViewModel(
    IActivityFacade activityFacade,
    INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService)
{
    public ActivityDetailModel? Activity { get; private set; }
    public Guid SubjectId { get; set; }
    public Guid Id { get; set; }
    
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        
        if (Id != Guid.Empty)
        {
            // Load the existing activity if ID is provided
            Activity = await activityFacade.GetAsync(Id);
        }
        else
        {
            // Create a new activity if ID is not provided
            Activity = ActivityDetailModel.Empty;
        }
    }
    
    // SAVE
    [RelayCommand]
    private async Task SaveAsync()
    {
        if (Activity != null)
        {
            await activityFacade.SaveAsync(Activity, SubjectId);
            MessengerService.Send(new ActivityEditMessage
            {
                ActivityId = Activity.Id 
            });
        }

        navigationService.SendBackButtonPressed();
    }

    
    [RelayCommand]
    private async Task BackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}
