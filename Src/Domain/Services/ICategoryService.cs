using ShopAIDesktop.Src.Domain.Common;
using ShopAIDesktop.Src.Domain.Dtos.Responses.Category;
using ShopAIDesktop.Src.Domain.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopAIDesktop.Src.Domain.Services;

public interface ICategoryService
{
    Task<ApiResponse<PaginationCategoryResponse>> Find(int currentPage, int pageSize, string status);

    Task<ApiResponse<Category>> Create(Category category);

    Task<ApiResponse<Category>> Update(Category category);

    Task<ApiResponse<Category>> Delete(Category category);
}
