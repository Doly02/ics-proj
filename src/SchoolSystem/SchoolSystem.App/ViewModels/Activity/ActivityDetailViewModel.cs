using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Models;

namespace SchoolSystem.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
[QueryProperty(nameof(SubjectId), nameof(SubjectId))]

public partial class ActivityDetailViewModel(
    IActivityFacade activityFacade,
    INavigationService navigationService,
    IMessengerService messengerService,
    IAlertService alertService)
    : ViewModelBase(messengerService), 
        IRecipient<ActivityEditMessage>,
        IRecipient<ActivityDeleteMessage>
{
    public Guid Id { get; set; }
    public ActivityDetailModel? Activity { get; set; }
    public Guid SubjectId { get; set; }
    
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Activity = await activityFacade.GetAsync(Id);
    }
    
    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (Activity is not null)
        {
            try
            {
                await activityFacade.DeleteAsync(Activity.Id);
                MessengerService.Send(new ActivityDeleteMessage());
                navigationService.SendBackButtonPressed();
            }
            catch (InvalidOperationException)
            {
                await alertService.DisplayAsync("Operation Failed",
                    "Removal of the Activity Failed.");
            }
        }
    }
    
    [RelayCommand]
    private async Task GoToEditAsync()
    {
        await Shell.Current.GoToAsync("/editActivity",
            new Dictionary<string, object?>
            {
                [nameof(ActivityEditViewModel.Id)] = Id,
                [nameof(ActivityEditViewModel.SubjectId)] = SubjectId
            });
    }
    
    
    [RelayCommand]
    private async Task BackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
    
    // After EDIT action
    public async void Receive(ActivityEditMessage message)
    {
        if (message.ActivityId == Activity?.Id)
        {
            await LoadDataAsync();
        }
    }
    
    // After DELETE action
    public async void Receive(ActivityDeleteMessage message)
    {
        await LoadDataAsync();
    }
}
