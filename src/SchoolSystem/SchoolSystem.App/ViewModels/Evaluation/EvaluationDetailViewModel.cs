using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Models;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services;


namespace SchoolSystem.App.ViewModels;


[QueryProperty(nameof(Id), nameof(Id))] 
public partial class EvaluationDetailViewModel(
    IEvaluationFacade evaluationFacade,
    INavigationService navigationService,
    IMessengerService messengerService,
    IAlertService alertService)
    : ViewModelBase(messengerService), IRecipient<EvaluationEditMessage>
{
    public Guid Id { get; set; }
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

        EvaluationDetail = await evaluationFacade.GetAsync(Id);
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (EvaluationDetail is not null)
        {
            try
            {
                await evaluationFacade.DeleteAsync(EvaluationDetail.Id);
                MessengerService.Send(new EvaluationDeleteMessage());
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
    private async Task GoToEditAsync()
    {
        await navigationService.GoToAsync("/edit",
            new Dictionary<string, object?>
                { [nameof(EvaluationEditViewModel.EvaluationDetail)] = EvaluationDetail });
    }
    
}
