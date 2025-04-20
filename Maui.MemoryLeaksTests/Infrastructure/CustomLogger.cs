using Microsoft.Extensions.Logging;

namespace Maui.MemoryLeaksTests.Infrastructure;

public sealed class CustomLogger : ICustomLogger
{
    private InputView? _inputView;

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default!;

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (this._inputView == null)
            return;

        this._inputView.Text += $"{DateTime.Now.ToString("T")}: {formatter(state, exception)}\n";
    }

    public void SetInputView(InputView inputView)
    {
        this._inputView = inputView;
    }
}
