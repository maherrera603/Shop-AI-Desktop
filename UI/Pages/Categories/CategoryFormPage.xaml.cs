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
using System.ComponentModel;
using ShopAIDesktop.Src.Domain.Dtos.Responses.Images;
using ShopAIDesktop.Src.Domain.Common;
namespace ShopAIDesktop.UI.Pages.Categories;



/// <summary>
/// Lógica de interacción para CategoryFormPage.xaml
/// </summary>
public partial class CategoryFormPage : Page, INotifyPropertyChanged
{
    private readonly ICategoryService _categoryService;
    private readonly IImageService _imageService;
    public Category? _category = null;
    private string? _selectedImagePath;
    public string _titleText = "Nueva Categoría";
    public string _subtitle = "Crea una nueva categoría para tu catálogo";
    public Visibility _createCategoryVisibility = Visibility.Visible;
    public Visibility _updateCategoryVisibility = Visibility.Collapsed;
    public Visibility _cancelVisibility = Visibility.Collapsed;


    public Visibility CancelVisibility
    {
        get => _cancelVisibility;
        set
        {
            _cancelVisibility = value;
            OnPropertyChanged(nameof(CancelVisibility));
        }
    }

    public Visibility UpdateCategoryVisibility
    {
        get => _updateCategoryVisibility;
        set
        {
            _updateCategoryVisibility = value;
            OnPropertyChanged(nameof(UpdateCategoryVisibility));
        }
    }

    public Visibility CreateCategoryVisibility
    {
        get => _createCategoryVisibility;
        set
        {
            _createCategoryVisibility = value;
            OnPropertyChanged(nameof(CreateCategoryVisibility));
        }
    }

    public string TitleText
    {
        get => _titleText;
        set
        {
            _titleText = value;
            OnPropertyChanged(nameof(TitleText));
        }
    }

    public string Subtitle
    {
        get => _subtitle;
        set
        {
            _subtitle = value;
            OnPropertyChanged(nameof(Subtitle));
        }
    }

    public Category? Category
    {
        get => _category;
        set
        {
            _category = value;
            OnPropertyChanged(nameof(Category));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


    public CategoryFormPage(ICategoryService categoryService, IImageService imageService)
    {
        InitializeComponent();
        _categoryService = categoryService;
        _imageService = imageService;

        DataContext = this;
        _category = new Category { IsActive = true, ImageUrl = null };
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

    public void ShowImagePreview(string imagePath)
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

    private void HandleActiveClick(object sender, RoutedEventArgs e)
    {
        if (Category != null) Category.IsActive = true;
    }
    private void HandleInactiveClick(object sender, RoutedEventArgs e)
    {
        if (Category != null) Category.IsActive = false;
    }

    private async void HandleSaveCategorie_Click(object sender, RoutedEventArgs e) {
        var responseImage = await _imageService.UploadImageAsync(_selectedImagePath!);
        if(responseImage.Code >= 400)
        {
            ShowAlert(AlertType.Warning, responseImage.Message);
            return;
        }

        _category?.ImageUrl = responseImage.Data!.SecureUrl;
        _category?.ImageProviderId = responseImage.Data!.PublicId;
        var responseCategory = await _categoryService.Create(_category!);
        if (responseCategory.Code >= 400) {
            await _imageService.DeleteImageAsync(responseImage.Data!.PublicId);
            ShowAlert(AlertType.Warning, responseCategory.Message);
            return;
        }

        Category = null;
        CancelImagePreview();
        ShowAlert(AlertType.Success, responseCategory.Message);
    }

    private void ShowAlert(AlertType alertType,  string message)
    {
        var alert = new CustomAlert(alertType, message)
        {
            Owner = Window.GetWindow(this)
        };

        alert.ShowDialog();
    }


    private void HandleCancel_Click(object sender, RoutedEventArgs e) {
        Category = null;
        TitleText = "Nueva Categoría";
        Subtitle = "Crea una nueva Categoría para tu catálogo";
        CancelVisibility = Visibility.Collapsed;
        UpdateCategoryVisibility = Visibility.Collapsed;
        CreateCategoryVisibility = Visibility.Visible;
        CancelImagePreview();
    }

    private void CancelImagePreview()
    {
        ImagePreview.Source = null;
        ImagePreview.Visibility = Visibility.Collapsed;
        ImagePlaceholder.Visibility = Visibility.Visible;
    }

    
    public async void HandleUpdateCategory_Click(object sender, RoutedEventArgs e) {
        // public id image old
        string imageProviderIdOld = _category!.ImageProviderId;

        // send  image the provider if selected
        ImageResponse? imageResponse = null;
        if (!string.IsNullOrWhiteSpace(_selectedImagePath)){
            var responseUpload = await _imageService.UploadImageAsync(_selectedImagePath);

            if(responseUpload.Code >= 400)
            {
                ShowAlert(AlertType.Warning, responseUpload.Message);
                return;
            }
            imageResponse = responseUpload.Data!;

            _category?.ImageUrl = imageResponse.SecureUrl;
            _category?.ImageProviderId = imageResponse.PublicId;
        }

        // update category with or without image selected
        var categoryResponse = await _categoryService.Update(_category!);
        if(categoryResponse.Code >= 400)
        {
            if (!string.IsNullOrWhiteSpace(imageResponse?.PublicId)) 
                await _imageService.DeleteImageAsync(imageResponse!.PublicId);

            ShowAlert(AlertType.Warning, categoryResponse.Message);
            return;
        }

        // delete image old of the provider of images
        if (!string.IsNullOrWhiteSpace(imageResponse?.PublicId) && !string.IsNullOrWhiteSpace(imageProviderIdOld))
            await _imageService.DeleteImageAsync(imageProviderIdOld);

        // actualizar formulario o regresar a categorias
        _category = categoryResponse.Data;
        ShowAlert(AlertType.Success, categoryResponse.Message);
    }
}