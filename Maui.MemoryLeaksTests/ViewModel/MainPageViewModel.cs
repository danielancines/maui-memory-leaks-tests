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
    private readonly IServiceProvider _hostServiceProvider;
    private IServiceScope? _scope;
    private IServiceProvider? _scopeServiceProvider;

    #endregion

    #region Constructor

    public MainPageViewModel(MyTransientClass myTransientClass, ICustomLogger logger, IServiceProvider hostServiceProvider)
    {
        this.InitializeCommands();
        this._logger = logger;
        this._hostServiceProvider = hostServiceProvider;
        this._scope = hostServiceProvider.CreateScope();
        this._scopeServiceProvider = this._scope.ServiceProvider;
    }

    #endregion

    #region Properties

    public ObservableCommand? CallGarbageCollectorCommand { get; private set; }
    public ObservableCommand? CreateTransientClassesCommand { get; private set; }
    public ObservableCommand? ClearScopedCommand { get; private set; }

    #endregion

    #region Public Methods

    public override void OnViewLoaded(object? obj)
    {
        this._logger.LogInformation($"HostServiceProvider: {this._hostServiceProvider.GetHashCode()}");
        this._logger.LogInformation($"ScopeServiceProvider: {this._scopeServiceProvider?.GetHashCode()}");

        base.OnViewLoaded(obj);
    }

    #endregion

    #region Private Methods
    private void InitializeCommands()
    {
        this.CallGarbageCollectorCommand = new ObservableCommand(OnCallGarbageCollector);
        this.CreateTransientClassesCommand = new ObservableCommand(OnCreateTransientClasses);
        this.ClearScopedCommand = new ObservableCommand(OnClearScope);
    }

    private void OnClearScope(object? obj)
    {
        //Optional if you want to dispose all disposable resolved objects
        this._scope?.Dispose();

        this._scope = null;
        this._scopeServiceProvider = null;
        this._logger.LogInformation("Scope disposed");
    }

    private void OnCreateTransientClasses(object? obj)
    {
        for (int i = 0; i < 2; i++)
        {
            _ = this._hostServiceProvider.GetService<MyTransientClass>();
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
