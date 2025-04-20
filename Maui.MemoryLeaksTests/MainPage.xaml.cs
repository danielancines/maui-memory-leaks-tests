using Maui.MemoryLeaksTests.Infrastructure;
using Maui.MemoryLeaksTests.ViewModel;

namespace Maui.MemoryLeaksTests;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel mainPageViewModel, ICustomLogger customLogger)
    {
        InitializeComponent();

        this.BindingContext = mainPageViewModel;

        customLogger.SetInputView(this.ResultsEditor);
    }
}
