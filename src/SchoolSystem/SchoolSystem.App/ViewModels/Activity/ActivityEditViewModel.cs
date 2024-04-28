using SchoolSystem.App.Services;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Models;

namespace SchoolSystem.App.ViewModels.Activity;

public partial class ActivityEditViewModel(
    IActivityFacade activityFacade,
    INavigationService navigationService,
    IMessengerService messengerService
) : ViewModelBase(messengerService)
{
    public ActivityDetailModel Activity { get; set; } = ActivityDetailModel.Empty;
}

