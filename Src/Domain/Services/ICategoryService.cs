using ShopAIDesktop.Src.Domain.Common;
using ShopAIDesktop.Src.Domain.entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopAIDesktop.Src.Domain.Services;

public interface ICategoryService
{
    Task<ApiResponse<List<Category>>> Find();
}
