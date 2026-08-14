using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using ShopAIDesktop.Src.Config;
using ShopAIDesktop.Src.Domain.Common;
using ShopAIDesktop.Src.Domain.entities;
using ShopAIDesktop.Src.Domain.Services;
using ShopAIDesktop.Src.Exceptions;
using ShopAIDesktop.Src.Infraestructure.Sessions;

namespace ShopAIDesktop.Src.Infraestructure.Services;

public class CategoryService : ICategoryService
{
    private readonly HttpClient _httpClient;
    private readonly ShopAIConfiguration _shopAIConfiguration;

    public CategoryService(HttpClient httpClient, ShopAIConfiguration shopAIConfiguration)
    {
        _httpClient = httpClient;
        _shopAIConfiguration = shopAIConfiguration;
    }

    public async Task<ApiResponse<List<Category>>> Find()
    {
        try
        {
            using var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"{_shopAIConfiguration.Gateway}/categories/all");
            httpRequest.Headers.Add("x-platform", "WEB");
            httpRequest.Headers.Add("Authorization", $"Bearer {AuthContext.Session.AccessToken}");

            var resposne = await _httpClient.SendAsync(httpRequest);

            var result = await resposne.Content.ReadFromJsonAsync<ApiResponse<List<Category>>>();

            return result!;
        }
        catch (ServiceException)
        {
            throw;
        }
    }
}
