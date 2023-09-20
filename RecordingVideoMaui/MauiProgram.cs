using Camera.MAUI;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace RecordingVideoMaui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseMauiCommunityToolkitMediaElement()
                .RegisterPages()
                .UseMauiCameraView()
                .RegisterViewModel()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static MauiAppBuilder RegisterViewModel(this MauiAppBuilder appBuilder)
        {
            appBuilder.Services.AddTransient<CamViewModel>();

            return appBuilder;

        }
        
        private static MauiAppBuilder RegisterPages(this MauiAppBuilder appBuilder)
        {
            appBuilder.Services.AddTransient<CamPage>();

            return appBuilder;

        }
    }
}
