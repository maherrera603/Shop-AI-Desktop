using ShopAIDesktop.Src.Domain.Dtos.Responses.Dashboard;
using ShopAIDesktop.Src.Domain.Services;
using ShopAIDesktop.UI.Components.CustomAlert;
using ShopAIDesktop.UI.Components.CustomCard;
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

namespace ShopAIDesktop.UI.Pages.Home;

/// <summary>
/// Lógica de interacción para HomePage.xaml
/// </summary>
public partial class HomePage : Page
{
    private readonly IDashboardService _dashboardService;
    private SummaryResponse _summaryResponse = new ();
    private bool _isDragging;
    private Point _startPoint;
    private double _startHorizontalOffset;

    public HomePage(IDashboardService dashboardService)
    {
        InitializeComponent();
        _dashboardService = dashboardService;

        var mainWindow = Window.GetWindow(this) as MainWindow;
        mainWindow?.SetTitle("ShopAI - Inicio");

        SummaryCatalog();
    }

    private async void SummaryCatalog()
    {

        var response = await _dashboardService.SummaryCatalog();
        _summaryResponse = response.Data!;

        if(response.Code >= 400)
        {
            var alert = new CustomAlert(AlertType.Warning, response.Message)
            {
                Owner = Window.GetWindow(this)
            };
            alert.ShowDialog();

            return;
        }

        LoadCustomCard(CardCategory, 
            "Categorias",
            "pricetags",
            _summaryResponse.CategoriesTotal,
            _summaryResponse.CategoriesCreatedThisMonth
        );

        LoadCustomCard(
            CardProduct,
            "Productos",
            "box",
            _summaryResponse.ProductsTotal,
            _summaryResponse.ProductsCreatedThisMonth
        );
    }


    private void LoadCustomCard(CustomCard customcard, string title, string icon, int quantity, int createdThisMonth)
    {
        customcard.Title = title;
        customcard.Icon = $"/Assets/Icons/{icon}.svg";
        customcard.Value = $"{quantity} {title}";
        customcard.Description = $"{title} agregados: {createdThisMonth}";

    }

    private void CardsScrollViewer_MouseLeftButtonDown(object sender, MouseEventArgs e)
    {
        _isDragging = true;
        _startPoint = e.GetPosition(CardsScrollViewer);
        _startHorizontalOffset = CardsScrollViewer.HorizontalOffset;

        CardsScrollViewer.CaptureMouse();
    }


    private void CardsScrollViewer_MouseMove(object sender, MouseEventArgs e)
    {
        if (!_isDragging) return;

        Point currentPoint = e.GetPosition(CardsScrollViewer);

        double difference = currentPoint.X - _startPoint.X;

        CardsScrollViewer.ScrollToHorizontalOffset(_startHorizontalOffset - difference);
    }

    private void CardsScrollViewer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        _isDragging = false;
        CardsScrollViewer.ReleaseMouseCapture();
    }

}
