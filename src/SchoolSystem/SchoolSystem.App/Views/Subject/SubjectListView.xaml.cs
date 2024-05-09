using SchoolSystem.App.ViewModels;
using SchoolSystem.BL.Facades;

namespace SchoolSystem.App.Views.Subject;

public partial class SubjectListView
{
    public SubjectListView(SubjectListViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
    private async void searchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        var viewModel = (SubjectListViewModel)BindingContext; // Get the ViewModel instance
        await viewModel.SearchAsync(e.NewTextValue); // Call SearchAsync with the new text value
        
    }
}
