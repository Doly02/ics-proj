using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Models;

namespace SchoolSystem.App.ViewModels.EnrolledActivity;

[QueryProperty(nameof(ViewModels.Activity), nameof(ViewModels.Activity))]
public partial class EnrolledActivityEditViewModel(
    IActivityFacade activityFacade,
    INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService),
        IRecipient<ActivityAddEvalMessage>
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
    
    /*
    // ADD EVALUATION
    [RelayCommand]
    public async Task AddEvalAsync() 
    {
        
    }
    */
    
    private async Task ReloadDataAsync()
    {
        Activity = await activityFacade.GetAsync(Activity.Id)
                   ?? ActivityDetailModel.Empty;
    }
    
    public async void Receive(ActivityAddEvalMessage message)
    {
        await ReloadDataAsync();
    }
    
}
