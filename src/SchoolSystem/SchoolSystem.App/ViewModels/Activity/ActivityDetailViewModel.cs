using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Models;

namespace SchoolSystem.App.ViewModels.Activity;

public partial class ActivityDetailViewModel(
    IActivityFacade activityFacade,
    INavigationService navigationService,
    IMessengerService messengerService,
    Guid subjectId)
    : ViewModelBase(messengerService), IRecipient<ActivityEditMessage>
{
    public Guid Id { get; set; }
    private ActivityDetailModel? Activity { get; set; }
    public Guid SubjectId { get; set; } = subjectId;
    
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Activity = await activityFacade.GetAsync(Id);
    }
    
    [RelayCommand]
    private async Task GoToEditAsync()
    {
        if (Activity is not null)
        {
            await navigationService.GoToAsync("/edit",
                new Dictionary<string, object?> { [nameof(ActivityEditViewModel.Activity)] = Activity with { } });
        }
    }
    
    public async void Receive(ActivityEditMessage message)
    {
        if (message.ActivityId == Activity?.Id)
        {
            await LoadDataAsync();
        }
    }
}
