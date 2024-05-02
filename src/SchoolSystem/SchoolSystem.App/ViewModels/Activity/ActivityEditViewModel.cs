using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Models;

namespace SchoolSystem.App.ViewModels.Activity;

[QueryProperty(nameof(ViewModels.Activity), nameof(ViewModels.Activity))]
public partial class ActivityEditViewModel(
    IActivityFacade activityFacade,
    INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService),
        IRecipient<ActivityAddMessage>,
        IRecipient<ActivityEditMessage>
{
    public ActivityDetailModel Activity { get; set; } = ActivityDetailModel.Empty;

    // SAVE
    [RelayCommand]
    private async Task SaveAsync()
    {
        await activityFacade.SaveAsync(Activity);
        MessengerService.Send(new ActivityEditMessage { ActivityId = Activity.Id });
        navigationService.SendBackButtonPressed();
    }
    
    private async Task ReloadDataAsync()
    {
        Activity = await activityFacade.GetAsync(Activity.Id)
                   ?? ActivityDetailModel.Empty;
    }
    
    public async void Receive(ActivityAddMessage message)
    {
        await ReloadDataAsync();
    }
    
    public async void Receive(ActivityEditMessage message)
    {
        await ReloadDataAsync();
    }
}
