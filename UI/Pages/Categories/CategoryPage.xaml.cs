using ShopAIDesktop.Src.Domain.Services;
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

namespace ShopAIDesktop.UI.Pages.Categories;

/// <summary>
/// Lógica de interacción para CategoryPage.xaml
/// </summary>
public partial class CategoryPage : Page
{
    private readonly ICategoryService _categoryService;
    public CategoryPage(ICategoryService categoryService)
    {
        InitializeComponent();
        _categoryService = categoryService;
    }
}
