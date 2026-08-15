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

    public string Cloudinary => _configuration["Gateway:Service:Url"]
         ?? throw new InvalidOperationException("La variable de configuracion 'Gateway:Service:Url' no esta definida");

    public string CloudinaryApiKey => _configuration["Gateway:Service:ApiKey"]
         ?? throw new InvalidOperationException("La variable de configuracion 'Gateway:Service:ApiKey' no esta definida");

    public string CloudinaryApiSecret => _configuration["Gateway:Service:ApiSecret"]
         ?? throw new InvalidOperationException("La variable de configuracion 'Gateway:Service:ApiSecret' no esta definida");

}
