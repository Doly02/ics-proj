using CommunityToolkit.Mvvm.Input;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Models;

namespace SchoolSystem.App.ViewModels;

[QueryProperty(nameof(Subject), nameof(Subject))]
public partial class SubjectEditViewModel(
    ISubjectFacade SubjectFacade,
    INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService)
{
    public SubjectDetailModel Subject { get; init; } = SubjectDetailModel.Empty;

    [RelayCommand]
    private async Task SaveAsync()
    {
        await SubjectFacade.SaveAsync(Subject);

        MessengerService.Send(new SubjectEditMessage { SubjectId = Subject.Id });

        navigationService.SendBackButtonPressed();
    }
}
