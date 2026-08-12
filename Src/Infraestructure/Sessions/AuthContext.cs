using ShopAIDesktop.Src.Domain.Dtos.Responses.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopAIDesktop.Src.Infraestructure.Sessions;

public static class AuthContext
{
    public static SignInResponse Session { get; private set; }

    public static bool IsAuthenticated => Session != null && !string.IsNullOrEmpty(Session.AccessToken);

    public static void SetSession(SignInResponse session)
    {
        Session = session;
    }

    public static void ClearSession()
    {
        Session = null;
    }

}
    
