using ShopAIDesktop.Src.Domain.entities;
using System;
using System.Collections.Generic;
using System.Text;
using CategoryEntity = ShopAIDesktop.Src.Domain.entities.Category;

namespace ShopAIDesktop.Src.Domain.Dtos.Responses.Category;

public class PaginationCategoryResponse
{
    public int TotalItems { get; set; }
    public List<CategoryEntity> Categories { get; set; } = [];
}
