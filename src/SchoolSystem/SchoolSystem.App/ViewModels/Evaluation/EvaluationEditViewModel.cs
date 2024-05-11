using CommunityToolkit.Mvvm.Input;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Models;

namespace SchoolSystem.App.ViewModels;

[QueryProperty(nameof(studentId), nameof(studentId))] 
[QueryProperty(nameof(activityId), nameof(activityId))] 
public partial class EvaluationEditViewModel(
    IEvaluationFacade evaluationFacade,
    INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService)
{
    private static Guid studentId { get; set; }
    private static Guid activityId { get; set; }
    public EvaluationDetailModel EvaluationDetail { get; init; } = EvaluationDetailModel.Empty with
    {
        StudentId = studentId,
        ActivityId = activityId
    };

    [RelayCommand]
    private async Task SaveAsync()
    {
        await evaluationFacade.SaveAsync(EvaluationDetail);
        MessengerService.Send(new EvaluationEditMessage { EvaluationId = EvaluationDetail.Id });

        navigationService.SendBackButtonPressed();
    }
    
    [RelayCommand]
    private async Task GoToDetailAsync()
    {
        await navigationService.GoToAsync("//students/detail/enrolledSubjects/enrolledActivities/evaluation",
            new Dictionary<string, object?>
            {
                [nameof(studentId)] = studentId,
                [nameof(activityId)] = activityId
            });
    }
}
