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

namespace ShopAIDesktop.UI.Components.CustomHeader;

/// <summary>
/// Lógica de interacción para CustomHeader.xaml
/// </summary>
public partial class CustomHeader : UserControl
{
    public CustomHeader()
    {
        InitializeComponent();
    }

    #region Dependency Properties

    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register(
            nameof(Title),
            typeof(string),
            typeof(CustomHeader),
            new PropertyMetadata(string.Empty)
        );

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly DependencyProperty SubtitleProperty =
        DependencyProperty.Register(
            nameof(SubTitle),
            typeof(string),
            typeof(CustomHeader),
            new PropertyMetadata(string.Empty)
        );

    public string SubTitle
    {
        get => (string)GetValue(SubtitleProperty);
        set => SetValue(SubtitleProperty, value);
    }

    public static readonly DependencyProperty ActionButtonTextProperty =
        DependencyProperty.Register(
            nameof(ActionButtonText),
            typeof(string),
            typeof(CustomHeader),
            new PropertyMetadata(string.Empty)
        );

    public string ActionButtonText
    {
        get => (string)GetValue(ActionButtonTextProperty);
        set => SetValue (ActionButtonTextProperty, value);
    }

    #endregion

    #region Eventos Expuestos
    public event RoutedEventHandler ActionRequested;

    private void HandleActionButton_Click(object sender, RoutedEventArgs e)
    {
        ActionRequested?.Invoke(this, e);
    }
    #endregion
}
