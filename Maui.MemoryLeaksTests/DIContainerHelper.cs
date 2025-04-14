namespace Maui.MemoryLeaksTests;

public static class DIContainerHelper
{
    public static T? GetService<T>()
    {
#if WINDOWS
        using var serviceScope = MauiWinUIApplication.Current.Services.CreateScope();
        var serviceProvider = serviceScope.ServiceProvider;

        return (T?)serviceProvider.GetService(typeof(T));
#endif
        return default;
    }
}
