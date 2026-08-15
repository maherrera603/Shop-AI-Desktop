using System;
using System.Collections.Generic;
using System.Text;

namespace ShopAIDesktop.Src.Domain.Services;

public interface ICloudinayService
{
    Task<string> UploadImageAsync(string filepath);
}
