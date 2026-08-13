using ShopAIDesktop.Src.Config;
using ShopAIDesktop.Src.Domain.Common;
using ShopAIDesktop.Src.Domain.Dtos.Requests.Auth;
using ShopAIDesktop.Src.Domain.Dtos.Responses.Auth;
using ShopAIDesktop.Src.Domain.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;

namespace ShopAIDesktop.Src.Infraestructure.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly ShopAIConfiguration _shopAIConfiguration;

    public AuthService(HttpClient httpClient, ShopAIConfiguration shopAIConfiguration)
    {
        _httpClient = httpClient;
        _shopAIConfiguration = shopAIConfiguration;
    }

    public async Task<ApiResponse<SignInResponse>> SignInAsync(SignInRequest request)
    {
        using var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"{_shopAIConfiguration.Gateway}/auth/sign-in");
        httpRequest.Headers.Add("x-platform", "WEB");
        httpRequest.Content = JsonContent.Create(request);

        var response = await _httpClient.SendAsync(httpRequest);


        var result = await response.Content.ReadFromJsonAsync<ApiResponse<SignInResponse>>();

        return result!;
    }

    public async Task<ApiResponse<object>> LogoutAsync(string accessToken, string refreshToken)
    {
        using var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"{_shopAIConfiguration.Gateway}/auth/logout");
        httpRequest.Headers.Add("x-platform", "WEB");
        httpRequest.Headers.Add("x-refresh-token", $"Bearer {refreshToken}");
        httpRequest.Headers.Add("Authorization", $"Bearer {accessToken}");

        var resposne = await _httpClient.SendAsync(httpRequest);

        var result = await resposne.Content.ReadFromJsonAsync<ApiResponse<object>>();

        return result!;
    }
}
