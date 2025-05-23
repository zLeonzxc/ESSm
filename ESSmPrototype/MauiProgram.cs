﻿#if ANDROID
using ESSmPrototype.Platforms.Android;
#endif
#if IOS
using ESSmPrototype.Platforms.iOS;
#endif

using ESSmPrototype.Custom;

namespace ESSmPrototype
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureMauiHandlers(handlers => {
#if ANDROID
                    handlers.AddHandler<CViewCell, CustomViewCellHandler>();
#endif
                })
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddScoped<StartedPage>();

            // register viewmodels
            builder.Services.AddSingleton<LoginViewModel>();

            // register API services


#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
