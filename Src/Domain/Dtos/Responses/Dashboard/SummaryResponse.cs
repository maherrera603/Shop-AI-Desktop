using System;
using System.Collections.Generic;
using System.Text;

namespace ShopAIDesktop.Src.Domain.Dtos.Responses.Dashboard;

public class SummaryResponse
{
    public int CategoriesTotal { get; set; }
    public int CategoriesCreatedThisMonth { get; set; }
    public int ProductsTotal { get; set; }
    public int ProductsCreatedThisMonth { get; set; }
}

