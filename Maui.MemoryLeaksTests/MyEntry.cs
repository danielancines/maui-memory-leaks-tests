using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using System.Diagnostics;

namespace Maui.MemoryLeaksTests;

public class MyEntry : Entry
{
    public MyEntry()
    {
        this.Loaded += MyEntry_Loaded;
    }

    private void MyEntry_Loaded(object? sender, EventArgs e)
    {
        var textBox = this.Handler?.PlatformView as TextBox;
        textBox.AddHandler(TextBox.KeyDownEvent, new KeyEventHandler(TextBox_KeyDown), true);

    }

    private void TextBox_KeyDown(object sender, KeyRoutedEventArgs keyRoutedEventArgs)
    {
        Debug.WriteLine($"Key pressed: {keyRoutedEventArgs.Key}");
    }
}
