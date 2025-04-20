using System.ComponentModel;
using Utils.Commands;

namespace Maui.MemoryLeaksTests.ViewModel;

public abstract class BaseViewModel : INotifyPropertyChanged
{
    #region Constructor

    protected BaseViewModel()
    {
        this.InitializeCommands();
    }

    #endregion

    #region Events

    public event PropertyChangedEventHandler? PropertyChanged;

    #endregion

    #region Properties
    public ObservableCommand? ViewLoadedCommand { get; private set; }

    #endregion

    #region Public Methods

    public virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public virtual void OnViewLoaded(object? obj)
    {

    }

    #endregion

    #region Private Methods
    private void InitializeCommands()
    {
        this.ViewLoadedCommand = new ObservableCommand(OnViewLoaded);
    }

    #endregion
}
