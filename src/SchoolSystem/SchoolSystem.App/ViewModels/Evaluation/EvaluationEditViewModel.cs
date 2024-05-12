using CommunityToolkit.Mvvm.Input;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Models;

namespace SchoolSystem.App.ViewModels;

[QueryProperty(nameof(studentId), nameof(studentId))] 
[QueryProperty(nameof(subjectId), nameof(subjectId))] 
[QueryProperty(nameof(activityId), nameof(activityId))] 
public partial class EvaluationEditViewModel(
    IEvaluationFacade evaluationFacade,
    INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService)
{
    public Guid studentId { get; set; }
    public Guid activityId { get; set; }
    public Guid subjectId { get; set; }
    public EvaluationDetailModel? EvaluationDetail { get; private set; }

    [RelayCommand]
    private async Task SaveAsync()
    {
        await evaluationFacade.SaveAsync(EvaluationDetail);
        
        MessengerService.Send(new EvaluationEditMessage {EvaluationId = EvaluationDetail.Id});
        MessengerService.Send(new ActivityEditMessage {ActivityId = activityId});

        navigationService.SendBackButtonPressed();
    }
    
    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();

        EvaluationDetail = await evaluationFacade.GetAsync(studentId, activityId);
        
        //EvaluationDetail = await evaluationFacade.GetEmptyModel(activityId, studentId);
        
            
    }
    
    [RelayCommand]
    private async Task GoToDetailAsync()
    {
        await navigationService.GoToAsync("//students/detail/enrolledSubjects/enrolledActivities/evaluation",
            new Dictionary<string, object?>
            {
                [nameof(EvaluationDetailViewModel.activityId)] = activityId,
                [nameof(EvaluationDetailViewModel.subjectId)] = subjectId, 
                [nameof(EvaluationDetailViewModel.studentId)] = studentId
            });
    }
}
