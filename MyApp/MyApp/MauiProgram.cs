using Microcharts.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyApp.View;

namespace MyApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMicrocharts()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("MuseoSansRounded-300.otf", "MuseoSansRounded300"); // police de la HELB
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainViewModel>();

            builder.Services.AddTransient<DetailsPage>();
            builder.Services.AddTransient<DetailsViewModel>();

            builder.Services.AddTransient<ExportArt>();
            builder.Services.AddTransient<ExportArtViewModel>();
            
            builder.Services.AddTransient<AddArt>();
            builder.Services.AddTransient<AddArtViewModel>();

            builder.Services.AddTransient<Statistics>();
            builder.Services.AddTransient<StatisticsViewModel>();

            //login page
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<LoginViewModel>();
            
            //register page
            builder.Services.AddTransient<RegisterPage>();
            builder.Services.AddTransient<RegisterViewModel>();

            builder.Services.AddTransient<JSONServices>();

            //db
            builder.Services.AddDbContext<DataAccesServices>(e => 
            e.UseSqlServer($"Server=(localdB)\\MSSQLLocalDB;DataBase=MyDataBase;Trusted_Connection=True"));

            return builder.Build();
        }
    }
}
