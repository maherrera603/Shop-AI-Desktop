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

    public string Cloudinary => _configuration["Cloudinay:Service:Url"]
         ?? throw new InvalidOperationException("La variable de configuracion 'Cloudinay:Service:Url' no esta definida");

    public string CloudinaryApiKey => _configuration["Cloudinay:Service:ApiKey"]
         ?? throw new InvalidOperationException("La variable de configuracion 'Cloudinay:Service:ApiKey' no esta definida");

    public string CloudinaryApiSecret => _configuration["Cloudinay:Service:ApiSecret"]
         ?? throw new InvalidOperationException("La variable de configuracion 'Cloudinay:Service:ApiSecret' no esta definida");

    public string CloudinaryUploadPresetCategorie => _configuration["Cloudinay:Service:UploadPresetCategorie"]
         ?? throw new InvalidOperationException("La variable de configuracion 'Cloudinay:Service:UploadPresetCategorie' no esta definida");

}
