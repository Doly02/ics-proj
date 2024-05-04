using SchoolSystem.App.ViewModels;

namespace SchoolSystem.App.Views.Enrolled;

public partial class EnrolledEditView
{
	public EnrolledEditView(EnrolledListViewModel viewModel) : base(viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}
