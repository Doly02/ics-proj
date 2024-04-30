using Microsoft.Extensions.Logging;
using SchoolSystem.App.Services;
using SchoolSystem.App.ViewModels;
using SchoolSystem.App.Views;
using SchoolSystem.App.Views.Student;

namespace SchoolSystem.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            // Registration of Services 
            builder.Services.AddSingleton<INavigationService, NavigationService>();
        
            // Registration of ViewModels
            builder.Services.AddTransient<StudentEditViewModel>();

            // Registration of  Views
            builder.Services.AddTransient<AppShell>();
            builder.Services.AddTransient<StudentEditView>();
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
