using System;
using System.Collections.Generic;
using System.Text;

namespace ShopAIDesktop.Src.Domain.entities;

public class Category
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;
    public string ImageProviderId { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}
