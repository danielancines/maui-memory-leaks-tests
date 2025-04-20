using Maui.MemoryLeaksTests.DiClasses;
using Maui.MemoryLeaksTests.Infrastructure;
using Maui.MemoryLeaksTests.ViewModel;
using Microsoft.Extensions.Logging;

namespace Maui.MemoryLeaksTests
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

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainPageViewModel>();

            builder.Services.AddTransient<MyTransientClass>();
            builder.Services.AddScoped<MyScopedClass>();
            builder.Services.AddSingleton<MySingletonClass>();

            builder.Services.AddSingleton<ICustomLogger, CustomLogger>();

            return builder.Build();
        }
    }
}
