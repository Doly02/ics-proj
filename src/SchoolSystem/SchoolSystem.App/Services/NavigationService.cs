using SchoolSystem.App.Models;
using SchoolSystem.App.ViewModels;
using SchoolSystem.App.ViewModels.Activity;
using SchoolSystem.App.Views.Activity;
using SchoolSystem.App.Views.Enrolled;
using SchoolSystem.App.Views.Student;
using SchoolSystem.App.Views.Subject;
using SchoolSystem.App.Views.Evaluation;

namespace SchoolSystem.App.Services;

public class NavigationService : INavigationService
{
    public IEnumerable<RouteModel> Routes { get; } = new List<RouteModel>
    {
        // Home? would be before everything (//home/...)
        //new("//home", typeof(HomeView), typeof(HomeViewModel)),
        
        // Student
        new("//students", typeof(StudentListView), typeof(StudentListViewModel)),
        new("//students/sort", typeof(StudentListView), typeof(StudentListViewModel)),
        new("//students/seach", typeof(StudentListView), typeof(StudentListViewModel)),
        // new("//students/detail", typeof(StudentDetailView), typeof(StudentDetailViewModel)),
        
        new("//student/edit", typeof(StudentEditView), typeof(StudentEditViewModel)),
        new("//student/detail/edit", typeof(StudentEditView), typeof(StudentEditViewModel)),
        new("//student/detail", typeof(StudentDetailView), typeof(StudentDetailViewModel)),
        new ("//student/list", typeof(StudentListView),typeof(StudentListView)),
        new("//student/add", typeof(StudentAddView), typeof(StudentAddViewModel)),
        // Enrolled of student
        // new("//students/detail/enrolledSubjects", typeof(EnrolledListView), typeof(EnrolledListViewModel)),
        new("//enrolled", typeof(EnrolledListView), typeof(EnrolledListViewModel)),
        new("//enrolled/edit", typeof(EnrolledEditView), typeof(EnrolledEditViewModel)),
        
        // Subjects
        new("//subjects", typeof(SubjectListView), typeof(SubjectListViewModel)),
        new("//subjects/edit", typeof(SubjectEditView), typeof(SubjectEditViewModel)),
        
        // Activities
        new("//subjects/activities", typeof(ActivityListView), typeof(ActivityListViewModel)),
        new("//subjects/activities/add", typeof(ActivityEditView), typeof(ActivityEditViewModel)),
        
        new("//subjects/activities/detail", typeof(ActivityDetailView), typeof(ActivityDetailViewModel)),
        new("//subjects/activities/detail/edit", typeof(ActivityEditView), typeof(ActivityEditViewModel)),
        
        // new("//students/detail/enrolledSubjects/activities", typeof(ActivityListView), typeof(ActivityListViewModel)),
        // new("//students/detail/enrolledSubjects/activities/edit", typeof(ActivityEditView), typeof(ActivityEditViewModel)),
        
        // new("//students/detail/enrolledSubjects/activities/detail", typeof(ActivityDetailView), typeof(ActivityDetailViewModel)),
        // new("//students/detail/enrolledSubjects/activities/detail/edit", typeof(ActivityEditView), typeof(ActivityEditViewModel)),
        
        // Evaluation
        new("//students/detail/enrolledSubjects/activities/detail/evaluation", typeof(EvaluationDetailView), typeof(EvaluationDetailViewModel)),
        new("//students/detail/enrolledSubjects/activities/detail/evaluation/edit", typeof(EvaluationEditView), typeof(EvaluationEditViewModel)),
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
