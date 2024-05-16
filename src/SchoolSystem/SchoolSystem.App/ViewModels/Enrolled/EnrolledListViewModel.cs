using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Models;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services;
using System.Collections.ObjectModel;


namespace SchoolSystem.App.ViewModels;


[QueryProperty(nameof(StudentId), nameof(StudentId))]
public partial class EnrolledListViewModel(
    IEnrolledFacade enrolledFacade,
    IStudentFacade studentFacade,
    INavigationService navigationService,
    IMessengerService messengerService,
    IAlertService alertService)
    : ViewModelBase(messengerService), IRecipient<EnrolledEditMessage>, IRecipient<EnrolledDeleteMessage>, IRecipient<EnrolledAddMessage>
{

    public IEnumerable<EnrolledSubjectsListModel> EnrolledList { get; set; } = null!;



    public ObservableCollection<EnrolledSubjectsListModel> sortedEnrolled { get; private set; } = new ObservableCollection<EnrolledSubjectsListModel>();
    public StudentDetailModel? Student { get; set; }

    public Guid StudentId { get; set; }

    public string StudentFullName => $"{Student?.Name} {Student?.Surname}";


    public async void Receive(EnrolledDeleteMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(EnrolledEditMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(EnrolledAddMessage message)
    {
        await LoadDataAsync();
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        // Načítání předmětů zapsaných k tomuto studentovi
        EnrolledList = await enrolledFacade.GetEnrolledSubjectsByStudentIdAsync(StudentId);
        Student = await studentFacade.GetAsync(StudentId);

        OnPropertyChanged(nameof(EnrolledList));
        OnPropertyChanged(nameof(Student));
        OnPropertyChanged(nameof(StudentFullName));

    }

    [RelayCommand]
    private async Task SortByNameAscAsync()
    {
        EnrolledList = await enrolledFacade.GetSortedAsync(true, true, StudentId);
        OnPropertyChanged(nameof(EnrolledList));
    }

    [RelayCommand]
    private async Task SortByNameDescAsync()
    {
        EnrolledList = await enrolledFacade.GetSortedAsync(false, true, StudentId);
        OnPropertyChanged(nameof(EnrolledList));
    }

    [RelayCommand]
    private async Task SortByAbbrAscAsync()
    {
        EnrolledList = await enrolledFacade.GetSortedAsync(true, false, StudentId);
        OnPropertyChanged(nameof(EnrolledList));
    }

    [RelayCommand]
    private async Task SortByAbbrDescAsync()
    {
        EnrolledList = await enrolledFacade.GetSortedAsync(false, false, StudentId);
        OnPropertyChanged(nameof(EnrolledList));
    }


    [RelayCommand]
    public async Task SearchAsync(string? search = null)
    {
        EnrolledList = await enrolledFacade.SearchBySubjectNameAsync(StudentId, search);
        OnPropertyChanged(nameof(EnrolledList));
    }


    [RelayCommand]
    public async Task GoToEditAsync()
    {
        // Navigate to the edit view with the constructed URI
        await navigationService.GoToAsync("//students/detail/enrolledSubjects/edit2",
        new Dictionary<string, object?>
        { [nameof(EnrolledEditViewModel.Student)] = Student });
    }


    [RelayCommand]
    private async Task RemoveEnrolledSubject(Guid id)
    {
        try
        {
            await enrolledFacade.DeleteAsync(id);
            MessengerService.Send(new EnrolledDeleteMessage());
        }
        catch (InvalidOperationException)
        {
            await alertService.DisplayAsync("Operation Failed", "Removal of the Subject Failed.");
        }
    }


    [RelayCommand]
    private async Task GoToDetailAsync(Guid Id)
    {
        await navigationService.GoToAsync("/enrolledActivities",
           new Dictionary<string, object?>
           {
               [nameof(EnrolledActivityListViewModel.SubjectId)] = Id,
               [nameof(EnrolledActivityListViewModel.StudentId)] = StudentId
           });

    }




    [RelayCommand]
    public async Task GoToDetailBackAsync(Guid Id)
    {
        await navigationService.GoToAsync<StudentDetailViewModel>(
           new Dictionary<string, object?> { [nameof(StudentDetailViewModel.Id)] = Id });
    }

}

