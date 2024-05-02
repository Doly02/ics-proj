using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Models;
 
namespace SchoolSystem.App.ViewModels;

public partial class StudentListViewModel(
    IStudentFacade studentFacade,
    INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService), IRecipient<StudentEditMessage>, IRecipient<StudentDeleteMessage>
{
    public IEnumerable<StudentListModel> StudList { get; set; } = null!;
    public ObservableCollection<StudentListModel> sortedStudents { get; private set; } = new ObservableCollection<StudentListModel>();
    public async void Receive(StudentEditMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(StudentDeleteMessage message)                                 
    {
        await LoadDataAsync();
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        StudList = await studentFacade.GetAsync();
    }

    [RelayCommand]
    private async Task GoToCreateAsync()
    {
        await navigationService.GoToAsync("/edit");
    }

    
    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
    {
        await navigationService.GoToAsync<StudentDetailViewModel>(new Dictionary<string, object?>
            { [nameof(StudentDetailViewModel.Id)] = id });
    }
    
    [RelayCommand]
    private async Task SortActivitiesAscendingAsync()
    {
        sortedStudents = await studentFacade.GetStudentsSortedBySurnameAscendingAsync();
        OnPropertyChanged(nameof(StudList));
    }

    [RelayCommand]
    private async Task SortActivitiesDescendingAsync()
    {
        sortedStudents = await studentFacade.GetStudentsSortedBySurnameDescendingAsync();
        OnPropertyChanged(nameof(StudList));
    }

    [RelayCommand]
    private async Task GoToSortAsync()
    {
        await navigationService.GoToAsync("/students/sort");
    }
    
    [RelayCommand]
    private async Task GoToSeachAsync()
    {
        await navigationService.GoToAsync("/students/search");
    }
}
