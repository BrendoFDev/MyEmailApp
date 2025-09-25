using FluentValidation;
using Microsoft.Extensions.Logging;
using MyEmailApp.Pages;
using MyEmailApp.Services.Validators;
using System.Reflection;

namespace MyEmailApp
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

            builder.Services.AddValidatorsFromAssembly(typeof(UserSettingValidator).Assembly);

            builder.Services.AddTransient<UserSettingPage>();
            builder.Services.AddTransient<RegisterEmailPage>();
            builder.Services.AddTransient<EmailSenderPage>();
            builder.Services.AddTransient<EmailFactoryPage>();
            builder.Services.AddTransient<MainPage>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
