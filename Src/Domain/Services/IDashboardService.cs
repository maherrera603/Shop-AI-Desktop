using ShopAIDesktop.Src.Domain.Common;
using ShopAIDesktop.Src.Domain.Dtos.Responses.Dashboard;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopAIDesktop.Src.Domain.Services;

public interface IDashboardService
{
    Task<ApiResponse<SummaryResponse>> SummaryCatalog();
}
