using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using ShopAIDesktop.Src.Config;
using ShopAIDesktop.Src.Domain.Common;
using ShopAIDesktop.Src.Domain.Dtos.Responses.Category;
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

    public async Task<ApiResponse<PaginationCategoryResponse>> Find(int currentPage, int pageSize, string status)
    {
        try
        { 
            string url = $"{_shopAIConfiguration.Gateway}/categories/all?page={currentPage}&pageSize={pageSize}&isActive={status}";
            using var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
            httpRequest.Headers.Add("x-platform", "WEB");
            httpRequest.Headers.Add("Authorization", $"Bearer {AuthContext.Session.AccessToken}");

            var resposne = await _httpClient.SendAsync(httpRequest);

            var result = await resposne.Content.ReadFromJsonAsync<ApiResponse<PaginationCategoryResponse>>();

            return result!;
        }
        catch (ServiceException)
        {
            throw;
        }
    }


    public async Task<ApiResponse<Category>> Create(Category category)
    {
        try
        {
            using var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"{_shopAIConfiguration.Gateway}/categories");
            httpRequest.Headers.Add("x-platform", "WEB");
            httpRequest.Headers.Add("Authorization", $"Bearer {AuthContext.Session.AccessToken}");

            httpRequest.Content = JsonContent.Create(category);

            var response = await _httpClient.SendAsync(httpRequest);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<Category>>();
            return result!;
        }
        catch (ServiceException)
        {
            throw;
        }
    }

    public async Task<ApiResponse<Category>> Update(Category category)
    {
        try
        {
            using var httpRequest = new HttpRequestMessage(HttpMethod.Put, $"{_shopAIConfiguration.Gateway}/categories/{category.Id}");
            httpRequest.Headers.Add("x-platform", "WEB");
            httpRequest.Headers.Add("Authorization", $"Bearer {AuthContext.Session.AccessToken}");
            httpRequest.Content = JsonContent.Create(category);

            var response = await _httpClient.SendAsync(httpRequest);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<Category>>();
            return result!;
        }
        catch (ServiceException)
        {
            throw;
        }
    }

    public async Task<ApiResponse<Category>> Delete(Category category)
    {
        try
        {
            using var httpRequest = new HttpRequestMessage(HttpMethod.Delete, $"{_shopAIConfiguration.Gateway}/categories/{category.Id}");
            httpRequest.Headers.Add("x-platform", "WEB");
            httpRequest.Headers.Add("Authorization", $"Bearer {AuthContext.Session.AccessToken}");

            var response = await _httpClient.SendAsync(httpRequest);

            var result = await response.Content.ReadFromJsonAsync<ApiResponse<Category>>();

            return result!;
        }
        catch (ServiceException)
        {
            throw;
        }
    }
}
