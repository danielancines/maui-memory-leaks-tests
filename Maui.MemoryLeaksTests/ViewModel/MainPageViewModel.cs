using Maui.MemoryLeaksTests.DiClasses;
using Maui.MemoryLeaksTests.Helpers;
using Maui.MemoryLeaksTests.Infrastructure;
using Microsoft.Extensions.Logging;
using Utils.Commands;

namespace Maui.MemoryLeaksTests.ViewModel;

public sealed class MainPageViewModel : BaseViewModel
{
    #region Fields

    private readonly ICustomLogger _logger;
    private readonly IServiceProvider _hostServideProvider;
    private IServiceScope? _scope;
    private IServiceProvider? _scopeServiceProvider;

    #endregion

    #region Constructor

    public MainPageViewModel(MyTransientClass myTransientClass, ICustomLogger logger, IServiceProvider hostServideProvider)
    {
        this.InitializeCommands();
        this._logger = logger;
        this._hostServideProvider = hostServideProvider;

        this._scope = hostServideProvider.CreateScope();
        this._scopeServiceProvider = this._scope.ServiceProvider;
    }

    #endregion

    #region Properties

    public ObservableCommand? CallGarbageCollectorCommand { get; private set; }
    public ObservableCommand? CreateTransientClassesCommand { get; private set; }
    public ObservableCommand? ClearScopedCommand { get; private set; }

    #endregion

    #region private Methods
    private void InitializeCommands()
    {
        this.CallGarbageCollectorCommand = new ObservableCommand(OnCallGarbageCollector);
        this.CreateTransientClassesCommand = new ObservableCommand(OnCreateTransientClasses);
        this.ClearScopedCommand = new ObservableCommand(OnClearScope);
    }

    private void OnClearScope(object? obj)
    {
        //Optional if you want to dispose all disposable resolved objects
        //this._scope?.Dispose();

        this._scope = null;
        this._scopeServiceProvider = null;
        this._logger.LogInformation("Scope disposed");
    }

    private void OnCreateTransientClasses(object? obj)
    {
        for (int i = 0; i < 2; i++)
        {
            _ = this._hostServideProvider.GetService<MyTransientClass>();
            _ = this._scopeServiceProvider?.GetService<MyTransientClass>();
        }

        this._logger.LogInformation("Transient Classes Created");
    }

    private void OnCallGarbageCollector(object? obj)
    {
        _ = GarbageCollectorHelper.Collect();
        this._logger.LogInformation("Garbage collector called");
    }

    #endregion
}
