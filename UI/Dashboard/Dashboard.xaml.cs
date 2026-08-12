using ShopAIDesktop.UI.Components.Sidebar;
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
using ShopAIDesktop.UI.Pages.Home;
using Microsoft.Extensions.DependencyInjection;
using ShopAIDesktop.UI.Pages.Categories;

namespace ShopAIDesktop.UI.Dashboard
{
    /// <summary>
    /// Lógica de interacción para Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        private readonly Sidebar _sidebar;
        public Dashboard(Sidebar sidebar)
        {
            InitializeComponent();

            _sidebar = sidebar;
            SidebarContainer.Content = _sidebar;
            Loaded += DashboardLoaded;

            var homePage = ((App)Application.Current)
                .Services
                .GetRequiredService<HomePage>();

            DashboardFrame.Navigate(homePage);

            sidebar.CategoriesClicked += HandleCategories;
            sidebar.HomeClicked += HandleHome;

        }

        private void DashboardLoaded(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow?.SetTitle("ShopAI - Inicio");
            mainWindow?.WindowState = WindowState.Maximized;
            mainWindow?.IsMinimize(true);
            mainWindow?.IsMaximize(true);
        }


        private void HandleHome(object sender, EventArgs e)
        {
            var homePage = ((App)Application.Current)
                .Services
                .GetRequiredService<HomePage>();

            DashboardFrame.Navigate(homePage);
        }

        private void HandleCategories(object sender, EventArgs e)
        {
            var categoriesPage = ((App)Application.Current)
                .Services
                .GetRequiredService<CategoryPage>();

            DashboardFrame.Navigate(categoriesPage);
        }
    }
}
