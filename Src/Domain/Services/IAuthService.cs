using ShopAIDesktop.Src.Domain.Common;
using ShopAIDesktop.Src.Domain.Dtos.Requests.Auth;
using ShopAIDesktop.Src.Domain.Dtos.Responses.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopAIDesktop.Src.Domain.Services;

public interface IAuthService
{
    Task<ApiResponse<SignInResponse>> SignInAsync(SignInRequest request);
    Task<ApiResponse<object>> LogoutAsync(string accessToken, string refreshToken);
}
