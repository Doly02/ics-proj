using CommunityToolkit.Maui.Views;
using SchoolSystem.App.ViewModels;

namespace SchoolSystem.App.Views.Activity;

public partial class FilterRangePopup
{
    public ActivityListViewModel ViewModel { get; set; }

    public FilterRangePopup(ActivityListViewModel viewModel)
    {
        InitializeComponent();
        ViewModel = viewModel;
        BindingContext = ViewModel;
    }
    
    private void EnterClick(object sender, EventArgs e)
    {
        DateTime startDateTime = StartDatePopup.Date.Add(StartTimePopup.Time);
        DateTime endDateTime = EndDatePopup.Date.Add(EndTimePopup.Time);

        if (startDateTime > endDateTime)
        {
            if (Application.Current?.MainPage != null)
                Application.Current.MainPage.DisplayAlert("Error",
                    "Start date must be earlier than the end date.", "OK");
        }
        else
        {
            ViewModel.StartDateFilter = StartDatePopup.Date;
            ViewModel.StartTimeFilter = StartTimePopup.Time;
            ViewModel.EndDateFilter = EndDatePopup.Date;
            ViewModel.EndTimeFilter = EndTimePopup.Time;

            ViewModel.ApplyFilterCommand.Execute(null);
            Close();
        }
    }

    private void CancelClick(object sender, EventArgs e)
    {
        Close();
    }
    
    private void ResetFiltersClick(object sender, EventArgs e)
    {
        ViewModel.ResetFiltersCommand.Execute(null);
        Close();
    }
}


