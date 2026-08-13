using System;
using System.Collections.Generic;
using System.Text;

namespace ShopAIDesktop.Src.Exceptions;

public class ServiceException: Exception
{
    public int Code { get;  }

    public string Status { get; }

    public ServiceException(string message, int code = 500,string status = "Error"): base(message)
    {
        Code = code;
        Status = status;
    }
}
