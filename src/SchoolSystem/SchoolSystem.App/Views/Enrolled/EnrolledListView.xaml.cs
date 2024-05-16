using SchoolSystem.App.ViewModels;

namespace SchoolSystem.App.Views.Enrolled;

public partial class EnrolledListView
{
    public EnrolledListView(EnrolledListViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
    private async void searchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        var viewModel = (EnrolledListViewModel)BindingContext; // Get the ViewModel instance
        await viewModel.SearchAsync(e.NewTextValue); // Call SearchAsync with the new text value

    }
}
