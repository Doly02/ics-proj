using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Models;

namespace SchoolSystem.App.ViewModels;

[QueryProperty(nameof(NewStud), nameof(NewStud))]
public partial class StudentAddViewModel(
    IStudentFacade studentFacade,
    INavigationService navigationService,
    IMessengerService messengerService,
    IAlertService alertService)
    : ViewModelBase(messengerService), IRecipient<StudentAddMessage>, IRecipient<StudentDeleteMessage>
{
    public StudentDetailModel NewStud { get; set; } = StudentDetailModel.Empty;

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(NewStud.Name))
        {
            await alertService.DisplayAsync("Operation Failed",
                "The Name is Empty.");
        }
        else if (string.IsNullOrWhiteSpace(NewStud.Surname))
        {
            await alertService.DisplayAsync("Operation Failed",
                "The Surname is Empty.");
        }
        else
        {
            await studentFacade.SaveAsync(NewStud);
            MessengerService.Send(new StudentEditMessage { StudentId = NewStud.Id });
            //navigationService.SendBackButtonPressed();
            await navigationService.GoToAsync("//students");
        }

    }
    
    [RelayCommand]
    public async Task AddAsync()
    {
        try
        {
            // Generate for Student New ID
            NewStud.Id = Guid.NewGuid();
            // Add Thru Facade Student To Database
            await studentFacade.SaveAsync(NewStud);
            // Send Message That Student Was Added
            MessengerService.Send(new StudentAddMessage());
            // Create New Stud Detail
            NewStud = StudentDetailModel.Empty;
            // Navigate Back To Main Page
            await navigationService.GoToAsync("//students");
            await ReloadDataAsync();
        }
        catch (Exception ex)
        {
            // Propagate Exception
        }
    }

    [RelayCommand]
    private void Clear()
    {
        NewStud = StudentDetailModel.Empty;
    }
    
    public async void Receive(StudentAddMessage message)
    {
        await ReloadDataAsync();
    }

    public async void Receive(StudentDeleteMessage message)
    {
        await ReloadDataAsync();
    }

    private async Task ReloadDataAsync()
    {
        NewStud = await studentFacade.GetAsync(NewStud.Id)
                  ?? StudentDetailModel.Empty;
    }
    
    [RelayCommand]
    private async Task GoBackToList()
    {
        await navigationService.GoToAsync("//students");
    }
}
