using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Models;

namespace SchoolSystem.App.ViewModels;

[QueryProperty(nameof(StudDetail), nameof(StudDetail))]
public partial class StudentEditViewModel(
    IStudentFacade studentFacade,
    INavigationService navigationService,
    IMessengerService messengerService,
    IAlertService alertService)
    : ViewModelBase(messengerService), IRecipient<StudentAddMessage>, IRecipient<StudentDeleteMessage>
{
    public StudentDetailModel StudDetail { get; set; } = StudentDetailModel.Empty;

    [RelayCommand]
    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(StudDetail.Name))
        {
            await alertService.DisplayAsync("Operation Failed",
                "The Name is Empty.");
        }
        else if (string.IsNullOrWhiteSpace(StudDetail.Surname))
        {
            await alertService.DisplayAsync("Operation Failed",
                "The Surname is Empty.");
        }
        else
        {
            await studentFacade.SaveAsync(StudDetail);
            MessengerService.Send(new StudentEditMessage { StudentId = StudDetail.Id });
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
            StudDetail.Id = Guid.NewGuid();
            // Add Thru Facade Student To Database
            await studentFacade.SaveAsync(StudDetail);
            // Send Message That Student Was Added
            MessengerService.Send(new StudentAddMessage());
            // Create New Stud Detail
            StudDetail = StudentDetailModel.Empty;
            // Navigate Back To Main Page}
        }
        catch (Exception ex)
        {
            // Propagate Exception
        }
    }

    [RelayCommand]
    private void Clear()
    {
        StudDetail = StudentDetailModel.Empty;
    }
    
    [RelayCommand]
    private async Task GoBackToDetail()
    {
        await navigationService.GoToAsync("//students/detail");
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
        StudDetail = await studentFacade.GetAsync(StudDetail.Id)
                 ?? StudentDetailModel.Empty;
    }
}
