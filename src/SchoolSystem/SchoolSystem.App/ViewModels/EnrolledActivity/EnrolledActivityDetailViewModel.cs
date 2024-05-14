using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Models;

namespace SchoolSystem.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
[QueryProperty(nameof(SubjectId), nameof(SubjectId))]

public partial class EnrolledActivityDetailViewModel(
    IActivityFacade activityFacade,
    INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService)
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
    private async Task BackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }
}