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
    private IEnumerable<StudentListModel> _OriginStudents { get; set; } = null!;
    
    public async void Receive(StudentAddMessage message)
    {
        await LoadDataAsync();
    }
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
        await navigationService.GoToAsync("//students/add");
    }

    
    [RelayCommand]
    private async Task GoToDetailAsync(Guid id)
    {
        await navigationService.GoToAsync("/detail", new Dictionary<string, object?>
            { [nameof(StudentDetailViewModel.Id)] = id });
    }
    
    [RelayCommand]
    private async Task SortStudentBySurnameAscendingAsync()
    {
        if (null! == _OriginStudents)
        {
            _OriginStudents = StudList;
            StudList  = await studentFacade.GetStudentsSortedBySurnameAscendingAsync(false);
            OnPropertyChanged(nameof(StudList));
        }
        else
        {
            StudList = new ObservableCollection<StudentListModel>(_OriginStudents);
            _OriginStudents = null!;
            OnPropertyChanged(nameof(StudList));
        }
    }

    [RelayCommand]
    private async Task SortStudentBySurnameDescendingAsync()
    {
        if (null! == _OriginStudents)
        {
            _OriginStudents = StudList;
            StudList  = await studentFacade.GetStudentsSortedBySurnameDescendingAsync(false);
            OnPropertyChanged(nameof(StudList));
        }
        else
        {
            StudList = new ObservableCollection<StudentListModel>(_OriginStudents);
            _OriginStudents = null!;
            OnPropertyChanged(nameof(StudList));
        }
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
    
    [RelayCommand]
    public async Task SearchAsync(string? search = null)
    {
        try
        {
            var results = await studentFacade.SearchAsync(search);
            StudList = results; 
            OnPropertyChanged(nameof(StudList));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message); 
        }
    }
}
