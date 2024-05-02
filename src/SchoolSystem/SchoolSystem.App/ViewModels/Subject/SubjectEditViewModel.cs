using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Models;

namespace SchoolSystem.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class SubjectEditViewModel(
    ISubjectFacade SubjectFacade,
    INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService)
{
    public Guid Id { get; set; }
    public SubjectDetailModel? Subject { get; private set; }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        
        if (Id != Guid.Empty)
        {
            // Load the existing subject if ID is provided
            Subject = await SubjectFacade.GetAsync(Id);
        }
        else
        {
            // Create a new subject if ID is not provided
            Subject = SubjectDetailModel.Empty;
        }
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        await SubjectFacade.SaveAsync(Subject);

        MessengerService.Send(new SubjectEditMessage { SubjectId = Subject.Id });

        navigationService.SendBackButtonPressed();
    }
}
