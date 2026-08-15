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
using Microsoft.Win32;
using Microsoft.Extensions.DependencyInjection;
using ShopAIDesktop.Src.Domain.entities;
using ShopAIDesktop.Src.Domain.Services;
using System.Diagnostics;
namespace ShopAIDesktop.UI.Pages.Categories;



/// <summary>
/// Lógica de interacción para CategoryFormPage.xaml
/// </summary>
public partial class CategoryFormPage : Page
{
    private readonly ICategoryService _categoryService;
    public Category category { get; set; }
    private string? _selectedImagePath;

    public CategoryFormPage(ICategoryService categoryService)
    {
        InitializeComponent();
        _categoryService = categoryService;
        category = new Category { IsActive = true, ImageUrl = null };
        DataContext = this;
    }

    private void HandleBackCategory_Click(object sender, RoutedEventArgs e)
    {
        var categoryPage = ((App)Application.Current)
            .Services
            .GetRequiredService<CategoryPage>();

        NavigationService.Navigate(categoryPage);

    }

    private void HandleSelectedImage_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog
        {
            Title = "Seleccionar Imagen",
            Filter = "Imagenes|*.webp;*.png",
            Multiselect = false
        };

        if (dialog.ShowDialog() != true) return;

        _selectedImagePath = dialog.FileName;
        ShowImagePreview(_selectedImagePath);
    }

    private void ShowImagePreview(string imagePath)
    {
        var bitmap = new BitmapImage();
        bitmap.BeginInit();
        bitmap.UriSource = new Uri(imagePath);
        bitmap.CacheOption = BitmapCacheOption.OnLoad;
        bitmap.EndInit();

        ImagePreview.Source = bitmap;
        ImagePreview.Visibility = Visibility.Visible;
        ImagePlaceholder.Visibility = Visibility.Collapsed;
    }
}
