using Microsoft.Extensions.DependencyInjection;
using ShopAIDesktop.Src.Domain.Services;
using ShopAIDesktop.Src.Infraestructure.Sessions;
using ShopAIDesktop.UI.Components.CustomAlert;
using ShopAIDesktop.UI.Pages.Categories;
using ShopAIDesktop.UI.Pages.Home;
using ShopAIDesktop.UI.Pages.Login;
using System;
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

namespace ShopAIDesktop.UI.Components.Sidebar;

/// <summary>
/// Lógica de interacción para Sidebar.xaml
/// </summary>
public partial class Sidebar : UserControl
{
    private readonly IAuthService _authService;
    private CustomAlert.CustomAlert _customAlert;
    public event EventHandler? HomeClicked;
    public event EventHandler? CategoriesClicked;

    public Sidebar(IAuthService authService)
    {
        InitializeComponent();
        _authService = authService;

        UserNameText.Text = $"{AuthContext.Session.User.FirstName} {AuthContext.Session.User.LastName}";
    }


    private async void HandleLogout(object sender, RoutedEventArgs e)
    {
        var response = await _authService.LogoutAsync(AuthContext.Session.AccessToken, AuthContext.Session.RefreshToken);

        if(response.Code >= 400)
        {
            _customAlert = new CustomAlert.CustomAlert(AlertType.Warning, response.Message);
            _customAlert.Owner = Window.GetWindow(this);
            _customAlert.ShowDialog();
            return;
        }

        _customAlert = new CustomAlert.CustomAlert(AlertType.Success, response.Message);
        _customAlert.Owner = Window.GetWindow(this);
        _customAlert.ShowDialog();


        AuthContext.ClearSession();

        var mainWindow = Window.GetWindow(this) as MainWindow;
        if (mainWindow! != null) mainWindow.WindowState = WindowState.Normal;


        var loginPage = ((App)Application.Current)
            .Services
            .GetRequiredService<LoginPage>();

        NavigationService
            .GetNavigationService(this)
            .Navigate(loginPage);
        
    }


    private void HandleHomePage(object sender, RoutedEventArgs e)
    {
        HomeClicked?.Invoke(this, EventArgs.Empty);
    }


    private void HandleCategoryPage(object sender, RoutedEventArgs e)
    {
        CategoriesClicked?.Invoke(this, EventArgs.Empty);
    }
}
