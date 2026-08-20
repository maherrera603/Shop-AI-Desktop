using System;
using System.Collections.Generic;
using System.Text;
using ShopAIDesktop.Src.Domain.Dtos.Responses.Images;
using ShopAIDesktop.Src.Domain.Common;

namespace ShopAIDesktop.Src.Domain.Services;

public interface IImageService
{
    Task<ApiResponse<ImageResponse>> UploadImageAsync(string filePath);

    Task<ApiResponse<object>> DeleteImageAsync(string publicId);
}
