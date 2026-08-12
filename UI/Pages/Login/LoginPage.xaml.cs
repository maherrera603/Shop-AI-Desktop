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
using Microsoft.Extensions.DependencyInjection;
using ShopAIDesktop.Src.Domain.Dtos.Requests.Auth;
using ShopAIDesktop.Src.Domain.Services;
using ShopAIDesktop.Src.Infraestructure.Sessions;
using ShopAIDesktop.UI.Components.CustomAlert;
using ShopAIDesktop.UI.Dashboard;

namespace ShopAIDesktop.UI.Pages.Login;

/// <summary>
/// Lógica de interacción para LoginPage.xaml
/// </summary>
public partial class LoginPage : Page
{
    private readonly IAuthService _authService;
    private CustomAlert _customAlert;

    public LoginPage(IAuthService authService)
    {
        InitializeComponent();
        _authService = authService;
    }

    private async void HandleSignIn(object sender, RoutedEventArgs e)
    {
        
        var request = new SignInRequest { 
            Email = EmailText.Text,
            Password = PasswordText.Password
        };


        var response = await _authService.SignInAsync(request);

        if(response.Code >= 400)
        {
            _customAlert = new CustomAlert(AlertType.Warning, response.Message);
            _customAlert.Owner = Window.GetWindow(this);
            _customAlert.ShowDialog();
            return;
        }

        
        if(response.Data != null ) AuthContext.SetSession(response.Data!);


        _customAlert = new CustomAlert(AlertType.Success, response.Message);
        _customAlert.Owner = Window.GetWindow(this);
        _customAlert.ShowDialog();

        var dashboard = ((App)Application.Current)
            .Services
            .GetRequiredService<Dashboard.Dashboard>();
         
        NavigationService.Navigate(dashboard);
        return;
    }
}
