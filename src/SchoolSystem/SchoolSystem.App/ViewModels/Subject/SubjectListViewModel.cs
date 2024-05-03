using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Models;

namespace SchoolSystem.App.ViewModels;

public partial class SubjectListViewModel(
    ISubjectFacade subjectFacade,
    INavigationService navigationService,
    IMessengerService messengerService,
    IAlertService alertService)
    : ViewModelBase(messengerService), IRecipient<SubjectEditMessage>, IRecipient<SubjectDeleteMessage>
{
    public IEnumerable<SubjectListModel> Subjects { get; set; } = null!;

    

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        Subjects = await subjectFacade.GetAsync();
    }

    [RelayCommand]
    private async Task SortByNameAscAsync()
    {
        Subjects = await subjectFacade.GetSortedByNameAscAsync();
        OnPropertyChanged(nameof(Subjects));
    }


    [RelayCommand]
    private async Task SortByNameDescAsync()
    {
        Subjects = await subjectFacade.GetSortedByNameDescAsync();
        OnPropertyChanged(nameof(Subjects));
    }

    [RelayCommand]
    private async Task SearchAsync(string? search = null)
    {
        Subjects = await subjectFacade.SearchAsync(search);
        OnPropertyChanged(nameof(Subjects));
    }

    [RelayCommand]
    private async Task RemoveIngredientAsync(Guid id)
    {
        try
        {
            await subjectFacade.DeleteAsync(id);
            MessengerService.Send(new SubjectDeleteMessage());
            navigationService.SendBackButtonPressed();
        }
        catch (InvalidOperationException)
        {
            await alertService.DisplayAsync("Operation Failed", "Removal of the Student Failed.");
        }
    }

    [RelayCommand]
    private async Task GoToCreateAsync()
    {
        await navigationService.GoToAsync("/edit");
    }

    [RelayCommand]
    public async Task GoToEditAsync(Guid id)
    {
            // Navigate to the edit view with the constructed URI
            await navigationService.GoToAsync("/edit",
            new Dictionary<string, object?>
            { [nameof(SubjectEditViewModel.Id)] = id});
    }

    /*
    [RelayCommand]
    private async Task GoToActivityAsync(Guid subjectId)
    {
        //must exist ActivityListViewModel

        await navigationService.GoToAsync<ActivityListViewModel>(
            new Dictionary<string, object?> { [nameof(SubjectDetailViewModel.Id)] = subjectId });
    }
    */

    public async void Receive(SubjectEditMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(SubjectDeleteMessage message)
    {
        await LoadDataAsync();
    }
}
