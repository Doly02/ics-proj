using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Models;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services;


namespace SchoolSystem.App.ViewModels;

public partial class EnrolledListViewModel(
    IEnrolledFacade enrolledFacade,
    INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService), IRecipient<EnrolledDeleteMessage>
{

    public IEnumerable<EnrolledSubjectsListModel> EnrolledList { get; set; } = null!;

    public async void Receive(EnrolledDeleteMessage message)
    {
        await LoadDataAsync();
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        EnrolledList = await enrolledFacade.GetAsync();
    }

    [RelayCommand]
    private async Task GoToCreateAsync()
    {
        await navigationService.GoToAsync("/edit");
    }

    /*
    [RelayCommand]
    private async Task GoToDetailAsync(Guid subjectId)
    {
        //must exist ActivityDetaiolViewModel

        await navigationService.GoToAsync<ActivityDetailViewModel>(
           new Dictionary<string, object?> { [nameof(ActivityDetailViewModel.SubjectId)] = subjectId });
    }
    */
}
