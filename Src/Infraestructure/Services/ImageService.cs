using ShopAIDesktop.Src.Config;
using ShopAIDesktop.Src.Domain.Common;
using ShopAIDesktop.Src.Domain.Dtos.Responses.Images;
using ShopAIDesktop.Src.Domain.Services;
using ShopAIDesktop.Src.Exceptions;
using ShopAIDesktop.Src.Infraestructure.Sessions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text;
using System.Text.Json;

namespace ShopAIDesktop.Src.Infraestructure.Services;

public class ImageService : IImageService
{
    private readonly HttpClient _httpClient;
    private readonly ShopAIConfiguration _shopAIConfiguration;
    public ImageService(HttpClient httpClient, ShopAIConfiguration shopAIConfiguration) {
        _shopAIConfiguration = shopAIConfiguration;
        _httpClient = httpClient;
    }

    public async Task<ApiResponse<ImageResponse>> UploadImageAsync(string filePath)
    {
        try
        {
            using var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"{_shopAIConfiguration.Gateway}/images/category");
            httpRequest.Headers.Add("x-platform", "WEB");
            httpRequest.Headers.Add("Authorization", $"Bearer {AuthContext.Session.AccessToken}");

            // subir imagenes
            using var multipartContent = new MultipartFormDataContent();
            await using var fileStream = File.OpenRead(filePath);
            using var fileContent = new StreamContent(fileStream);

            multipartContent.Add(fileContent, "Image", Path.GetFileName(filePath));

            httpRequest.Content = multipartContent;

            var response = await _httpClient.SendAsync(httpRequest);

            return await response.Content.ReadFromJsonAsync<ApiResponse<ImageResponse>>();

        }
        catch (ServiceException)
        {
            throw;
        }
    }

    public async Task<ApiResponse<object>> DeleteImageAsync(string publicId)
    {
        try
        {
            using var httpRequest = new HttpRequestMessage(HttpMethod.Delete, $"{_shopAIConfiguration.Gateway}/images");
            httpRequest.Headers.Add("x-platform", "WEB");
            httpRequest.Headers.Add("Authorization", $"Bearer {AuthContext.Session.AccessToken}");
            httpRequest.Content = JsonContent.Create(new { publicId });

            var response = await _httpClient.SendAsync(httpRequest);

            return await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
        }
        catch (ServiceException)
        {
            throw;
        }
    }
}
