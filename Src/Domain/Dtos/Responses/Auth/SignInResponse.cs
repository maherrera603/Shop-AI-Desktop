using ShopAIDesktop.Src.Domain.entities;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Text;

namespace ShopAIDesktop.Src.Domain.Dtos.Responses.Auth;

public class SignInResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;

    public string Role { get; set;  } = string.Empty;

    public User User { get; set; }
}
