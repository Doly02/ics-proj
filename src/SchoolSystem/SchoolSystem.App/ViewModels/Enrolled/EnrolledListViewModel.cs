using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Models;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services;
using System.Collections.ObjectModel;


namespace SchoolSystem.App.ViewModels;


[QueryProperty(nameof(Student), nameof(Student))]
public partial class EnrolledListViewModel(
    IEnrolledFacade enrolledFacade,
    IStudentFacade studentFacade,
    INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService), IRecipient<EnrolledEditMessage>, IRecipient<EnrolledDeleteMessage>
{

    public IEnumerable<EnrolledSubjectsListModel> EnrolledList { get; set; } = null!;

    public ObservableCollection<EnrolledSubjectsListModel> sortedEnrolled { get; private set; } = new ObservableCollection<EnrolledSubjectsListModel>();
    public EnrolledSubjectsListModel? Student { get; set; } = EnrolledSubjectsListModel.Empty;

    public async void Receive(EnrolledDeleteMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(EnrolledEditMessage message)
    {
        await LoadDataAsync();
    }

    protected override async Task LoadDataAsync()
    {
        if (Student != null) // Kontrola, zda 'Student' není null
        {
            Student = await enrolledFacade.GetByIdAsync(Student.Id);
        }
        else
        {
            // Možné další ošetření, pokud je 'Student' null
        }
    }

    [RelayCommand]
    private async Task SortBySubjectAscAsync()
    {
        EnrolledList = await enrolledFacade.SortEnrolledSubjectsAscAsync();
        OnPropertyChanged(nameof(EnrolledList));
    }


    [RelayCommand]
    private async Task SearchAsync(string? search = null)
    {
        EnrolledList = await enrolledFacade.SearchBySubjectNameAsync(search);
        OnPropertyChanged(nameof(EnrolledList));
    }




    [RelayCommand]
    private async Task GoToCreateAsync()
    {
        await navigationService.GoToAsync("/edit", new Dictionary<string, object?> { [nameof(EnrolledEditViewModel.Student)] = Student});
    }


    [RelayCommand]
    public async Task GoToEditAsync()
    {
        // Navigate to the edit view with the constructed URI
        await navigationService.GoToAsync("/edit",
        new Dictionary<string, object?>
        { [nameof(EnrolledEditViewModel.Student)] = Student });
    }


    [RelayCommand]
    private async Task GoToDetailAsync(Guid Id)
    {

        await navigationService.GoToAsync<Activity.ActivityDetailViewModel>(
           new Dictionary<string, object?> { [nameof(Activity.ActivityDetailViewModel.Id)] = Id });
    }
    
}
