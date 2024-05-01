using CommunityToolkit.Mvvm.Input;
using SchoolSystem.App.Services;
using SchoolSystem.App.ViewModels;
using SchoolSystem.BL.Models;


namespace SchoolSystem.App.Shells
{
    public partial class AppShell
    {
        private readonly INavigationService _navigationService;

        public AppShell(INavigationService navigationService)
        {
            _navigationService = navigationService;

            InitializeComponent();
        }

        [RelayCommand]
        private async Task GoToStudentsAsync()
            => await _navigationService.GoToAsync<StudentListViewModel>();

        [RelayCommand]
        private async Task GoToSubjectsAsync()
            => await _navigationService.GoToAsync<SubjectListViewModel>();
    
        [RelayCommand]
        private async Task GoToEditStudentAsync()
            => await _navigationService.GoToAsync<StudentEditViewModel>();
    }
}
