using ShopAIDesktop.Src.Config;
using ShopAIDesktop.Src.Domain.Common;
using ShopAIDesktop.Src.Domain.Dtos.Responses.Dashboard;
using ShopAIDesktop.Src.Domain.Services;
using ShopAIDesktop.Src.Infraestructure.Sessions;


using System.Net.Http;
using System.Net.Http.Json;


namespace ShopAIDesktop.Src.Infraestructure.Services;

public class DashboardService : IDashboardService
{
    private readonly HttpClient _httpClient;
    private readonly ShopAIConfiguration _shopAIConfiguration;

    public DashboardService(HttpClient httpClient, ShopAIConfiguration shopAIConfiguration)
    {
        _httpClient = httpClient;
        _shopAIConfiguration = shopAIConfiguration;
    }

    public async Task<ApiResponse<SummaryResponse>> SummaryCatalog()
    {
        
        using var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{_shopAIConfiguration.Gateway}/dashboard/summary");
        httpRequest.Headers.Add("x-platform", "WEB");
        httpRequest.Headers.Add("Authorization", $"Bearer {AuthContext.Session.AccessToken}");

        var response = await _httpClient.SendAsync(httpRequest);

        var result = await response.Content.ReadFromJsonAsync<ApiResponse<SummaryResponse>>();

        return result!;
    }
}
