using Microsoft.Extensions.Logging;
using Plugin.LocalNotification;

namespace MauiBlazorLantern
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseLocalNotification()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

            LocalNotificationCenter.Current.NotificationActionTapped += async (e) =>
            {
                if (e.Request.ReturningData == "turnoff_flash")
                {
                    try
                    {
                        await Flashlight.Default.TurnOffAsync();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error turning off flashlight: {ex.Message}");
                    }
                }
            };

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
