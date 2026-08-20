using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShopAIDesktop.Src.Config;
using ShopAIDesktop.Src.Domain.Services;
using ShopAIDesktop.Src.Infraestructure.Services;
using ShopAIDesktop.UI.Components.Sidebar;
using ShopAIDesktop.UI.Dashboard;
using ShopAIDesktop.UI.Pages.Categories;
using ShopAIDesktop.UI.Pages.Home;
using ShopAIDesktop.UI.Pages.Login;

namespace ShopAIDesktop;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public IServiceProvider Services { get; }

    public App()
    {

        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile(
                "appsettings.Development.json",
                optional: false,
                reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();


        var services = new ServiceCollection();

        services.AddHttpClient();
        services.AddSingleton<IConfiguration>(configuration);
        services.AddSingleton<ShopAIConfiguration>();

        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IDashboardService, DashboardService>();
        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<IImageService, ImageService>();


        services.AddTransient<LoginPage>();
        services.AddTransient<Sidebar>();
        services.AddTransient<Dashboard>();
        services.AddTransient<HomePage>();
        services.AddTransient<CategoryPage>();
        services.AddTransient<CategoryFormPage>();

        Services = services.BuildServiceProvider();

    }


}
