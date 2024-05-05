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
    IMessengerService messengerService,
    Guid subjectId)
    : ViewModelBase(messengerService),
        IRecipient<ActivityAddMessage>,
        IRecipient<ActivityDeleteMessage>
{
    public ActivityDetailModel Activity { get; set; } = ActivityDetailModel.Empty;
    public Guid SubjectId { get; set; } = subjectId;

    // SAVE
    [RelayCommand]
    private async Task SaveAsync()
    {
        await activityFacade.SaveAsync(Activity, SubjectId);
        MessengerService.Send(new ActivityEditMessage { ActivityId = Activity.Id });
        navigationService.SendBackButtonPressed();
    }
    
    // ADD
    [RelayCommand]
    public async Task AddAsync() 
    {
        try 
        {
            Activity.Id = Guid.NewGuid();
            await activityFacade.SaveAsync(Activity, SubjectId);
            MessengerService.Send(new StudentAddMessage());
            Activity = ActivityDetailModel.Empty;
        }
        
        catch (Exception exception)
        {
            // Propagate Exception
        }
    }
    
    //CLEAR
    [RelayCommand]
    private void Clear()
    {
        Activity = ActivityDetailModel.Empty;
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
    
    public async void Receive(ActivityDeleteMessage message)
    {
        await ReloadDataAsync();
    }
}
