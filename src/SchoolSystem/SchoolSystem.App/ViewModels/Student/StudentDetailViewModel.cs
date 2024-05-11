using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Models;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services;


namespace SchoolSystem.App.ViewModels;

// Partial Class To Divide Class Into More Files
/// <summary>
/// Represents the View Model for Student Details, Managing the Presentation Logic for Student Information.
/// Class is Part of a Partial Class Strategy to Divide the Class Into Multiple Files for Better Maintainability.
/// </summary>
/// <remarks>
/// Class Uses Several Services and Student Facade to Handle Data Operations and Communications Related to Student Details.
/// Utilizes the CommunityToolkit.Mvvm Components for Messaging and Commanding.
/// </remarks>
[QueryProperty(nameof(Id), nameof(Id))] // Attributes To Receive Values Directly From Navigation Params in the Application 
public partial class StudentDetailViewModel(
    IStudentFacade studentFacade,
    INavigationService navigationService,
    IMessengerService messengerService,
    IAlertService alertService)
    : ViewModelBase(messengerService), IRecipient<StudentEditMessage>
{
    public Guid Id { get; set; }
    public StudentDetailModel? StudDetail { get; private set; }

    /// <summary>
    /// Receives a Message About a Student Edit and Reloads the Students Data if Edited Student Matches the Current Student in Detail View.
    /// </summary>
    /// <param name="message">The Student Edit Message That Contains the ID of the Edited Student.</param>
    /// <remarks>
    /// Method is Part of the <see cref="IRecipient{TMessage}"/> Interface Implementation, Where <c>TMessage</c> is <see cref="StudentEditMessage"/>.
    /// Method Checks if the Incoming Message's <c>StudentId</c> Matches the <c>Id</c> of the Student Currently Being Viewed.
    /// If They Match, it Triggers a Reload of the Student.
    /// </remarks>
    public async void Receive(StudentEditMessage message)
    {
        if (message.StudentId == StudDetail?.Id)
        {
            await LoadDataAsync();
        }
    }
    
    public async void Receive(StudentAddMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(StudentDeleteMessage message)
    {
        await LoadDataAsync();
    }
    /// <summary>
    /// Asynchronously Loads the Student Details From the Database Thru Facade.
    /// </summary>
    /// <remarks>
    /// This Method Overrides <see cref="ViewModelBase.LoadDataAsync"/> to Implement the Specific Data Loading Logic for Student Details.
    /// The Student Data is Fetched Based on the <see cref="Id"/> Property Value.
    /// </remarks>
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        StudDetail = await studentFacade.GetAsync(Id);
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (StudDetail is not null)
        {
            try
            {
                await studentFacade.DeleteAsync(StudDetail.Id);
                MessengerService.Send(new StudentDeleteMessage());
                navigationService.SendBackButtonPressed();
            }
            catch (InvalidOperationException)
            {
                await alertService.DisplayAsync("Operation Failed",
                    "Removal of the Student Failed.");
            }
        }
    }

    [RelayCommand]
    private async Task GoToEditAsync()
    {
        await navigationService.GoToAsync("/edit",
            new Dictionary<string, object?>
                { [nameof(StudentEditViewModel.StudDetail)] = StudDetail });
    }

    [RelayCommand]
    private async Task GoToEnrolledAsync(Guid id)
    {
        await navigationService.GoToAsync("/enrolledSubjects", new Dictionary<string, object?>
        { [nameof(EnrolledListViewModel.StudentId)] = id });
    }



    [RelayCommand]
    private async Task GoToList()
    {
        await navigationService.GoToAsync("//students");
    }


}
