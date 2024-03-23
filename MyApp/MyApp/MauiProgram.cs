using Microsoft.Extensions.Logging;

namespace MyApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
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
            

            builder.Services.AddTransient<JSONServices>();

            return builder.Build();
        }
    }
}
