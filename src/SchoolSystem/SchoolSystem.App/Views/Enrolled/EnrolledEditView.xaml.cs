using SchoolSystem.App.ViewModels;

namespace SchoolSystem.App.Views.Enrolled;

public partial class EnrolledEditView
{
	public EnrolledEditView(EnrolledEditViewModel viewModel) : base(viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}
