using Microsoft.Extensions.DependencyInjection;
using ShopAIDesktop.Src.Domain.entities;
using ShopAIDesktop.Src.Domain.Services;
using ShopAIDesktop.UI.Components.CustomAlert;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ShopAIDesktop.UI.Pages.Categories;

/// <summary>
/// Lógica de interacción para CategoryPage.xaml
/// </summary>
public partial class CategoryPage : Page
{
    private readonly ICategoryService _categoryService;
    public ObservableCollection<Category> Categories { get; set; } = new();
    public CategoryPage(ICategoryService categoryService)
    {
        InitializeComponent();
        _categoryService = categoryService;
        DataContext = this;
        Loaded += CategoryPage_Loaded;
    }


    private async void CategoryPage_Loaded(object sender, RoutedEventArgs e)
    {
        await LoadCategoriesAsync();
    }


    private async Task LoadCategoriesAsync()
    { 
        var response = await _categoryService.Find();

        if( response.Code >= 400)
        {
            var alert = new CustomAlert(AlertType.Warning, response.Message)
            {
                Owner = Window.GetWindow(this)
            };
            alert.ShowDialog();
            return;
        }

        Categories.Clear();

        foreach (var category in response.Data!)
        {
            Categories.Add(category);    
        }

        Debug.WriteLine($"categorias {Categories.ToList().Count}");

    }

    private void HandleNewCategoryButton_Click(object sender, RoutedEventArgs e)
    {
        var categoryFormPage = ((App)Application.Current)
            .Services
            .GetRequiredService<CategoryFormPage>();

        NavigationService.Navigate(categoryFormPage);
    }
}
