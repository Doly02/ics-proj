using SchoolSystem.App.ViewModels;

namespace SchoolSystem.App.Views.Enrolled;

public partial class EnrolledListView
{
    public EnrolledListView(EnrolledListViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
