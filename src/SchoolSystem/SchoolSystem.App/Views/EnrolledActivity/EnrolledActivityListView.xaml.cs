using SchoolSystem.App.ViewModels;

namespace SchoolSystem.App.Views.EnrolledActivity;

public partial class EnrolledActivityListView
{
    public EnrolledActivityListView(EnrolledActivityListViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}

