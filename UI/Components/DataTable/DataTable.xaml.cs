using System;
using System.Collections;
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

namespace ShopAIDesktop.UI.Components.DataTable;

/// <summary>
/// Lógica de interacción para DataTable.xaml
/// </summary>
public partial class DataTable : UserControl
{
    public DataTable()
    {
        InitializeComponent();
    }


    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register(
            nameof(ItemsSource),
            typeof(IEnumerable),
            typeof(DataTable),
            new PropertyMetadata(null)
        );

    public IEnumerable? ItemsSource
    {
        get => (IEnumerable?)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }
}
