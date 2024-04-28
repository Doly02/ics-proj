using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Models;

namespace SchoolSystem.App.ViewModels;

public partial class StudentEditViewModel(
    IStudentFacade studentFacade,
    INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService)
{
    public StudentDetailModel StudDetail { get; set; } = StudentDetailModel.Empty;

    [RelayCommand]
    private async Task SaveAsync()
    {
        await studentFacade.SaveAsync(StudDetail);
        MessengerService.Send(new StudentEditMessage { StudentId = StudDetail.Id });

        navigationService.SendBackButtonPressed();
    }
    [RelayCommand]
    private async Task AddAsync()
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
}
