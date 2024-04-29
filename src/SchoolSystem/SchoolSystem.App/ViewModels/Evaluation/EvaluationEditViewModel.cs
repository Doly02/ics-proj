using CommunityToolkit.Mvvm.Input;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Models;

namespace SchoolSystem.App.ViewModels;

public partial class EvaluationEditViewModel(
    IEvaluationFacade evaluationFacade,
    INavigationService navigationService,
    IMessengerService messengerService)
    : ViewModelBase(messengerService)
{
    public EvaluationDetailModel EvaluationDetail { get; init; } = EvaluationDetailModel.Empty;

    [RelayCommand]
    private async Task SaveAsync()
    {
        await evaluationFacade.SaveAsync(EvaluationDetail);
        MessengerService.Send(new EvaluationEditMessage { EvaluationId = EvaluationDetail.Id });

        navigationService.SendBackButtonPressed();
    }
}
