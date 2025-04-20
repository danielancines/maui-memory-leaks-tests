using Maui.MemoryLeaksTests.Infrastructure;
using Microsoft.Extensions.Logging;

namespace Maui.MemoryLeaksTests.DiClasses;

public sealed class MyTransientClass : IDisposable, IDIClass
{
    private readonly ICustomLogger _customLogger;

    public MyTransientClass(ICustomLogger customLogger, IServiceProvider serviceProvider)
    {
        this._customLogger = customLogger;
        this._customLogger.LogInformation($"{this.Id} - Transient Class Created");
        this._customLogger.LogInformation($"MyTransientClass ServiceProvider: {serviceProvider.GetHashCode()}");
    }

    public Guid Id { get; } = Guid.NewGuid();

    public void Dispose()
    {
        this._customLogger.LogInformation($"{this.Id} - Transient Class Disposed");
    }
}
