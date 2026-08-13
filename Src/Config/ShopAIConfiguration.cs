using Microsoft.Extensions.Configuration;


namespace ShopAIDesktop.Src.Config;

public class ShopAIConfiguration
{
    private readonly IConfiguration _configuration;

    public ShopAIConfiguration(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Gateway => _configuration["ShopAI:Service:Gateway"] 
        ?? throw new InvalidOperationException("La variable de configuracion 'ShopAI:Service:Gateway' no esta definida");


}
