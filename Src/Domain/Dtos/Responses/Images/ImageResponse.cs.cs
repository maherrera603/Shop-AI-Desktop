using System;
using System.Collections.Generic;
using System.Text;

namespace ShopAIDesktop.Src.Domain.Dtos.Responses.Images;

public class ImageResponse
{
    public string SecureUrl { get; set; } = string.Empty;
    public string PublicId { get; set; } = string.Empty;
}