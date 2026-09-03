using Microsoft.Extensions.DependencyInjection;
using ShopAIDesktop.Src.Domain.entities;
using ShopAIDesktop.Src.Domain.Services;
using ShopAIDesktop.UI.Components.ConfirmationAlert;
using ShopAIDesktop.UI.Components.CustomAlert;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
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
    private readonly IImageService _imageService;
    private int _totalItems;
    private int _currentPage = 1;
    private int _pageSize = 30;
    private string _selectedStatus = "all";

    // Bandera para evitar ejecucione prematuras en InitilizaeComponente
    private bool _isInitialized = false;
    public ObservableCollection<Category> Categories { get; set; } = new();

    public CategoryPage(ICategoryService categoryService, IImageService imageService)
    {
        InitializeComponent();
        _categoryService = categoryService;
        _imageService = imageService;
        DataContext = this;

        Loaded += CategoryPage_Loaded;
        CategoriesTable.DeleteRequest += HandleDeleteCategoryRequested;
        CategoriesTable.OpenFormUpdate += HandleFormUpdateRequested;

        // Suscripcion al evento de cambio de pagina de paginacion
        PaginationControl.PageChanged += HandlePageChanged;
    }


    private async void CategoryPage_Loaded(object sender, RoutedEventArgs e)
    {

        _isInitialized = true;
        await LoadCategoriesAsync();
    }


    private async Task LoadCategoriesAsync()
    { 
        var response = await _categoryService.Find(_currentPage, _pageSize, _selectedStatus);

        if( response.Code >= 400)
        {
            ShowAlert(AlertType.Warning, response.Message);
            return;
        }

        Categories.Clear();
        _totalItems = response.Data!.TotalItems;
        foreach (var category in response.Data!.Categories)
        {
            Categories.Add(category);    
        }

        // Actualizar las propiedades del control visual de paginacion
        PaginationControl.CurrentPage = _currentPage;
        PaginationControl.PageSize = _pageSize;
        PaginationControl.TotalItems = _totalItems;
    }

    private async void HandleStatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (!_isInitialized) return;

        if (StatusComboBox.SelectedItem is ComboBoxItem selectedItem)
        {
            string content = selectedItem.Content.ToString() ?? "Todas";

            _selectedStatus = content switch
            {
                "Activas" => "active",
                "Inactivas" => "inactive",
                _ => "all"
            };

            _currentPage = 1;
            await LoadCategoriesAsync();
        }
    }


    private async void HandlePageChanged(object sender, int newPage)
    {
        _currentPage = newPage;
        await LoadCategoriesAsync();
    }

    private void HandleNewCategoryButton_Click(object sender, RoutedEventArgs e)
    {
        var categoryFormPage = ((App)Application.Current)
            .Services
            .GetRequiredService<CategoryFormPage>();

        NavigationService.Navigate(categoryFormPage);
    }

    private async void HandleDeleteCategoryRequested(object sender, Category category)
    {
        // confirmacion de la accion del usuairo
        var confirmationAlert = new ConfirmationAlert
        {
            Owner = Application.Current.MainWindow,
            TitleText = "Eliminar Categoria",
            MessageText = $"Estás seguro de que deseas eliminar '{category.Name}'",
            ConfirmText = "Eliminar",
            CancelText = "Cancelar"
        };


        if (confirmationAlert.ShowDialog() != true) return;

        // eliminar la categoria de la bd 
        var categoryResponse = await _categoryService.Delete(category);
        if(categoryResponse.Code >= 400)
        {
            ShowAlert(AlertType.Warning, categoryResponse.Message);
            return;
        }

        // eliminar la imagen del provedor
        await _imageService.DeleteImageAsync(category.ImageProviderId);
        ShowAlert(AlertType.Success, categoryResponse.Message);
        Categories.Remove(category);
    }

    private void ShowAlert(AlertType alertType, string message)
    {
        var alert = new CustomAlert(alertType, message)
        {
            Owner = Window.GetWindow(this)
        };
        alert.ShowDialog();
    }

    // !TODO: Traerme el redireccionamiento ha CategoryFormPage y quitarlo de la tabla
    public async void HandleFormUpdateRequested(object sender, Category category)
    {
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
