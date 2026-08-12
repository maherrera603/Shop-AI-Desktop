using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShopAIDesktop.UI.Components.WindowTitleBar;

/// <summary>
/// Lógica de interacción para WindowTitleBar.xaml
/// </summary>
public partial class WindowTitleBar : UserControl
{
    public string Title { 
        get => (string)GetValue(TitleProperty); 
        set => SetValue(TitleProperty, value); 
    }

    public bool IsMinimize
    {
        get => (bool)GetValue(IsMinimizeProperty);
        set => SetValue(IsMinimizeProperty, value);
    }

    public bool IsMaximize
    {
        get => (bool)GetValue(IsMaximizeProperty);
        set => SetValue(IsMaximizeProperty, value);
    }

    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
        nameof(Title),
        typeof(string),
        typeof(WindowTitleBar),
        new PropertyMetadata(string.Empty)
    );

    public static readonly DependencyProperty IsMinimizeProperty =
    DependencyProperty.Register(
        nameof(IsMinimize),
        typeof(bool),
        typeof(WindowTitleBar),
        new PropertyMetadata(true)
    );

    public static readonly DependencyProperty IsMaximizeProperty =
    DependencyProperty.Register(
        nameof(IsMaximize),
        typeof(bool),
        typeof(WindowTitleBar),
        new PropertyMetadata(true)
    );

    

    public WindowTitleBar()
    {
        InitializeComponent();
    }


    private void HandleDrag(object sender, MouseButtonEventArgs e)
    {
        if(e.LeftButton == MouseButtonState.Pressed)
            GetWindow()?.DragMove();
    }

    private void HandleMinimize(object sender, RoutedEventArgs e)
    {
        GetWindow().WindowState = WindowState.Minimized;
    }

    private void HandleMaximize(object sender, RoutedEventArgs e)
    {
        GetWindow().WindowState = (GetWindow().WindowState == WindowState.Normal)
            ? WindowState.Maximized
            : WindowState.Normal;

    }

    private void HandleClose(object sender, RoutedEventArgs e)
    {
        GetWindow().Close();
    }

    private Window GetWindow()
    {
        return Window.GetWindow(this);
    }
}
