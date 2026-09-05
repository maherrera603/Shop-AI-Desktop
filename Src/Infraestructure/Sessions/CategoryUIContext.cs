using System;
using System.Collections.Generic;
using System.Text;

namespace ShopAIDesktop.Src.Infraestructure.Sessions;

public enum CategoryViewMode
{
    Table,
    Cards
}

public static class CategoryUIContext
{
    // por defecto inicia en modo tabla
    public static CategoryViewMode CurrentViewMode { get; private set; } = CategoryViewMode.Table;

    public static void SetViewMode(CategoryViewMode viewMode)
    {
        CurrentViewMode = viewMode;
    }

    public static bool IsCardsView => CurrentViewMode == CategoryViewMode.Cards;
    public static bool IsTableView => CurrentViewMode == CategoryViewMode.Table;
}
