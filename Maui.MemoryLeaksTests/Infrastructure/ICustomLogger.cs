using Microsoft.Extensions.Logging;

namespace Maui.MemoryLeaksTests.Infrastructure;

public interface ICustomLogger : ILogger
{
    void SetInputView(InputView inputView);
}
