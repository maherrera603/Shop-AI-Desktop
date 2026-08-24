using ShopAIDesktop.Src.Domain.entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using ShopAIDesktop.UI.Pages.Categories;
using SharpVectors.Dom.Events;

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

    public event EventHandler<Category>? DeleteRequest;
    public event EventHandler<Category>? OpenFormUpdate;


    public void HandleDelete_Click(object sender, RoutedEventArgs e) {
        if (sender is Button button && button.DataContext is Category category) {
            DeleteRequest?.Invoke(this, category);
        }
    }

    public void HandleFormUpdate_Click(object sender, RoutedEventArgs e)
    {
        if(sender is Button button && button.DataContext is Category category)
        {
            OpenFormUpdate?.Invoke(this, category);
        }
    }
}
