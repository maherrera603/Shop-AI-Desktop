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
using ShopAIDesktop.UI.Components.CustomAlert;
namespace ShopAIDesktop.UI.Pages.Categories;



/// <summary>
/// Lógica de interacción para CategoryFormPage.xaml
/// </summary>
public partial class CategoryFormPage : Page
{
    private readonly ICategoryService _categoryService;
    private readonly IImageService _imageService;
    public Category Category { get; set; }
    private string? _selectedImagePath;

    public CategoryFormPage(ICategoryService categoryService, IImageService imageService)
    {
        InitializeComponent();
        _categoryService = categoryService;
        _imageService = imageService;

        DataContext = this;
        Category = new Category { IsActive = true, ImageUrl = null };
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

    private async void HandleSaveCategorie_Click(object sender, RoutedEventArgs e) {
        var responseImage = await _imageService.UploadImageAsync(_selectedImagePath!);
        if(responseImage.Code >= 400)
        {
            ShowAlert(AlertType.Warning, responseImage.Message);
            return;
        }

        Category.ImageUrl = responseImage.Data!.SecureUrl;
        var responseCategory = await _categoryService.Create(Category);
        if (responseCategory.Code >= 400) {
            await _imageService.DeleteImageAsync(responseImage.Data.PublicId);
            ShowAlert(AlertType.Warning, responseCategory.Message);
            return;
        }


        ShowAlert(AlertType.Success, responseCategory.Message);
        Category = new Category();
    }

    private void ShowAlert(AlertType alertType,  string message)
    {
        var alert = new CustomAlert(alertType, message)
        {
            Owner = Window.GetWindow(this)
        };

        alert.ShowDialog();
    }
}