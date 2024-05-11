using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using SchoolSystem.BL.Facades;
using SchoolSystem.BL.Models;
using SchoolSystem.App.Messages;
using SchoolSystem.App.Services;
using SchoolSystem.DAL.UnitOfWork;


namespace SchoolSystem.App.ViewModels;


[QueryProperty(nameof(studentId), nameof(studentId))] 
[QueryProperty(nameof(activityId), nameof(activityId))] 
public partial class EvaluationDetailViewModel(
    IEvaluationFacade evaluationFacade,
    INavigationService navigationService,
    IMessengerService messengerService,
    IAlertService alertService)
    : ViewModelBase(messengerService), IRecipient<EvaluationEditMessage>
{
    public Guid Id = Guid.Empty; //Guid.Parse("c0a5c2d1-8a95-4e09-bba2-67c3d133e20e");
    public Guid studentId { get; set; } /*Guid.Parse(input: "0d4fa150-ad80-4d46-a511-4c666166ec5e");*/
    public Guid activityId { get; set; } /*Guid.Parse(input: "8e615f4a-7a3b-4f86-b199-d7d48c4652e8");*/
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
    private async Task BackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task GoToEditAsync()
    {
        await navigationService.GoToAsync("/evaluationEdit",
            new Dictionary<string, object?>
            {
                [nameof(EvaluationDetailViewModel.studentId)] = studentId,
                [nameof(EvaluationDetailViewModel.activityId)] = activityId
                //[nameof(EvaluationDetailViewModel)] = EvaluationDetail
            });
    }
    
}
