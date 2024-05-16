using SchoolSystem.App.ViewModels;

namespace SchoolSystem.App.Views.Student;

public partial class StudentListView
{
    public StudentListView(StudentListViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
    private async void searchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        var viewModel = (StudentListViewModel)BindingContext; // Get the ViewModel instance
        await viewModel.SearchAsync(e.NewTextValue); // Call SearchAsync with the new text value

    }
}
