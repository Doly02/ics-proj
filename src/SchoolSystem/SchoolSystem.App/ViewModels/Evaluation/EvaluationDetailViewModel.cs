using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Models;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services;
using SchoolSystem.DAL.UnitOfWork;


namespace SchoolSystem.App.ViewModels;


[QueryProperty(nameof(studentId), nameof(studentId))]
[QueryProperty(nameof(subjectId), nameof(subjectId))] 
[QueryProperty(nameof(activityId), nameof(activityId))] 
public partial class EvaluationDetailViewModel(
    IEvaluationFacade evaluationFacade,
    INavigationService navigationService,
    IMessengerService messengerService,
    IAlertService alertService)
    : ViewModelBase(messengerService), IRecipient<EvaluationEditMessage>
{
    public Guid studentId { get; set; } 
    public Guid activityId { get; set; } 
    public Guid subjectId { get; set; }
    public EvaluationDetailModel? EvaluationDetail { get; private set; }
    
    public async void Receive(EvaluationEditMessage message)
    {
        if (message.EvaluationId == EvaluationDetail?.Id)
        {
            await LoadDataAsync();
        }
    }
    
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        EvaluationDetail = await evaluationFacade.GetAsync(studentId, activityId);
        /*if (EvaluationDetail is null)
        {
            EvaluationDetail = await evaluationFacade.GetEmptyModel(activityId, studentId);
        }*/
            
    }
    
    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (EvaluationDetail is not null)
        {
            try
            {
                await evaluationFacade.DeleteAsync(EvaluationDetail.Id);
                MessengerService.Send(new ActivityEditMessage {ActivityId = activityId});
                navigationService.SendBackButtonPressed();
            }
            catch (InvalidOperationException)
            {
                await alertService.DisplayAsync("Operation Failed",
                    "Removal of the Evaluation Failed.");
            }
        }
    }
    
    [RelayCommand]
    private async Task BackAsync()
    {
        await navigationService.GoToAsync("//students/detail/enrolledSubjects/enrolledActivities",
            new Dictionary<string, object?>
            {
                [nameof(EnrolledActivityListViewModel.StudentId)] = studentId,
                [nameof(EnrolledActivityListViewModel.SubjectId)] = subjectId
            });
    }

    [RelayCommand]
    private async Task GoToEditAsync()
    {
        await navigationService.GoToAsync("/evaluationEdit",
            new Dictionary<string, object?>
            {
                [nameof(EvaluationEditViewModel.studentId)] = studentId,
                [nameof(EvaluationEditViewModel.subjectId)] = subjectId,
                [nameof(EvaluationEditViewModel.activityId)] = activityId
            });
    }
    
}
