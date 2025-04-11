namespace Maui.MemoryLeaksTests;

static class TestHelpers
{
    public static async Task Collect()
    {
        await Task.Yield();
        GC.Collect();
        GC.WaitForPendingFinalizers();
    }
}

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }
    Window window;
    private async void OnCounterClicked(object sender, EventArgs e)
    {
        var application = new Application();

        WeakReference CreateReference()
        {
            var window = new Window { Page = new ContentPage() };
            var firstTitleBar = new TitleBar();
            var secondTitleBar = new TitleBar();
            var reference = new WeakReference(firstTitleBar);

            window.TitleBar = firstTitleBar;

            application.OpenWindow(window);

            window.TitleBar = secondTitleBar;

            ((IWindow)window).Destroying();
            return reference;
        }

        var reference = CreateReference();

        // GC
        await TestHelpers.Collect();
        this.ResultsEditor.Text += $"{DateTime.Now.TimeOfDay} - Control must be false! - {reference.IsAlive}\n";

        GC.KeepAlive(application);
    }

    private async void TestWindowButton_Clicked(object sender, EventArgs e)
    {
        var application = new Application();

        WeakReference CreateReference()
        {
            var page = new MyContentPage(this);
            page.Loaded += Page_Loaded;
            var window = new Window { Page = page };
            window.Destroying += Window_Destroying;
            var reference = new WeakReference(window);
            application.OpenWindow(window);
            ((IWindow)window).Destroying();
            return reference;
        }

        var reference = CreateReference();

        // GC
        await TestHelpers.Collect();

        this.ResultsEditor.Text += $"{DateTime.Now.TimeOfDay} - Window must be false! - {reference.IsAlive}\n";

        GC.KeepAlive(application);
    }

    private void Window_Destroying(object? sender, EventArgs e)
    {
        if (sender is Window window)
        {
            window.Destroying -= Window_Destroying;
            if (window.Page is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }

    private void Page_Loaded(object? sender, EventArgs e)
    {
    }
}

public class MyContentPage : ContentPage, IDisposable
{
    ContentPage _page;

    public MyContentPage(ContentPage page)
    {
        _page = page;
        _page.Loaded += Page_Loaded;
        var button = new Button { Text = "Click Me" };
        button.Clicked += OnButtonClicked;
        Content = button;
    }

    private void Page_Loaded(object? sender, EventArgs e)
    {
    }

    private void OnButtonClicked(object sender, EventArgs e)
    {
        // Handle button click
    }

    public void Dispose()
    {
        this._page.Loaded -= Page_Loaded;
    }
}
