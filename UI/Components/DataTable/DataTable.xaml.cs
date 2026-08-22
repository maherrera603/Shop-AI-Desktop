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


    public void HandleEditCategory_Click(object sender, RoutedEventArgs e)
    {
        if(sender is Button button && button.DataContext is Category category)
        {
            Debug.WriteLine($"category: {JsonSerializer.Serialize(category)}");
            
            var categoryFormPage = ((App)Application.Current)
                .Services
                .GetRequiredService<CategoryFormPage>();

            categoryFormPage.TitleText = "Editar Categoría";
            categoryFormPage.Subtitle = "Actualiza la información de la categoría";
            categoryFormPage.CreateCategoryVisibility = Visibility.Collapsed;
            categoryFormPage.UpdateCategoryVisibility = Visibility.Visible;
            categoryFormPage.CancelVisibility = Visibility.Visible;

            categoryFormPage.Category = category;


            if (!string.IsNullOrWhiteSpace(category.ImageUrl)) categoryFormPage.ShowImagePreview(category.ImageUrl);

            NavigationService
                .GetNavigationService(this)
                .Navigate(categoryFormPage);
        }
    }
}
