using SchoolSystem.App.ViewModels;

namespace SchoolSystem.App.Views.Student;

public partial class StudentListView
{
    public StudentListView(StudentListViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
