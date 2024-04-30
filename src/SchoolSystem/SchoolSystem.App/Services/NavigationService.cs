using SchoolSystem.App.Models;
using SchoolSystem.App.ViewModels;
using SchoolSystem.App.ViewModels.Activity;
using SchoolSystem.App.Views.Student;
// using SchoolSystem.App.Views.Subject;
// using SchoolSystem.App.Views.Activity;
// using SchoolSystem.App.Views.Enrolled;
// using SchoolSystem.App.Views.Evaluation;

namespace SchoolSystem.App.Services;

public class NavigationService : INavigationService
{
    public IEnumerable<RouteModel> Routes { get; } = new List<RouteModel>
    {
        // Home? would be before everything (//home/...)
        //new("//home", typeof(HomeView), typeof(HomeViewModel)),
        
        // new("//students", typeof(StudentListView), typeof(StudentListViewModel)),
        // new("//students/detail", typeof(StudentDetailView), typeof(StudentDetailViewModel)),
        
        new("//students/edit", typeof(StudentEditView), typeof(StudentEditViewModel)),
        new("//students/detail/edit", typeof(StudentEditView), typeof(StudentEditViewModel)),
        
        // new("//subjects", typeof(SubjectListView), typeof(SubjectListViewModel)),
        // new("//subjects/edit", typeof(SubjectEditView), typeof(SubjectEditViewModel)),
        
        // new("//subjects/activities", typeof(ActivityListView), typeof(ActivityListViewModel)),
        // new("//subjects/activities/edit", typeof(ActivityEditView), typeof(ActivityEditViewModel)),
        
        // new("//subjects/activities/detail", typeof(ActivityDetailView), typeof(ActivityDetailViewModel)),
        // new("//subjects/activities/detail/edit", typeof(ActivityEditView), typeof(ActivityEditViewModel)),
        
    };

    public async Task GoToAsync<TViewModel>()
        where TViewModel : IViewModel
    {
        var route = GetRouteByViewModel<TViewModel>();
        await Shell.Current.GoToAsync(route);
    }
    public async Task GoToAsync<TViewModel>(IDictionary<string, object?> parameters)
        where TViewModel : IViewModel
    {
        var route = GetRouteByViewModel<TViewModel>();
        await Shell.Current.GoToAsync(route, parameters);
    }

    public async Task GoToAsync(string route)
        => await Shell.Current.GoToAsync(route);

    public async Task GoToAsync(string route, IDictionary<string, object?> parameters)
        => await Shell.Current.GoToAsync(route, parameters);

    public bool SendBackButtonPressed()
        => Shell.Current.SendBackButtonPressed();

    private string GetRouteByViewModel<TViewModel>()
        where TViewModel : IViewModel 
        => Routes.First(route => route.ViewModelType == typeof(TViewModel)).Route;
}