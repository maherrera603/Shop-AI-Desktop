using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShopAIDesktop.UI.Components.Pagination;

/// <summary>
/// Lógica de interacción para Pagination.xaml
/// </summary>
public partial class Pagination : UserControl, INotifyPropertyChanged
{
    private int _currentPage;
    private int _pageSize;
    private int _totalItems;
    public event EventHandler<int>? PageChanged;
    public ObservableCollection<int> PageNumbers { get; set; } = new ();

    public Pagination()
    {
        InitializeComponent();
        LayoutRoot.DataContext = this;

        // Nos aseguramos de inicializar las propiedades cuando el control se carga en la interfaz
        Loaded += (s, e) => NotifyAllProperties();
        
    }


    public int TotalPages
    {
        get
        {
            if (PageSize <= 0) return 0;
            return (int)Math.Ceiling((double)TotalItems / PageSize);
        }
    }

    private int StartItem => TotalItems == 0 ? 0 : ((CurrentPage - 1) * PageSize) + 1;
    private int EndItem => Math.Min(CurrentPage * PageSize, TotalItems);
    public string InfoText => $"Mostrando {StartItem} - {EndItem} de {TotalItems}";


    public event PropertyChangedEventHandler? PropertyChanged;

    public int CurrentPage
    {
        get => _currentPage;
        set
        {
            if (_currentPage != value)
            {
                _currentPage = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CurrentPage));
                NotifyAllProperties();
            }
        }
    }

    public int PageSize
    {
        get => _pageSize;
        set
        {
            if (_pageSize != value)
            {
                _pageSize = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(PageSize));
                NotifyAllProperties();
            }
        }
    }

    public int TotalItems
    {
        get => _totalItems;
        set
        {
            if (_totalItems != value)
            {
                _totalItems = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalItems));
                NotifyAllProperties();
            }
        }
    }

    private void NotifyAllProperties()
    {
        UpdatePageNumbers();
        OnPropertyChanged(nameof(CurrentPage));
        OnPropertyChanged(nameof(PageSize));
        OnPropertyChanged(nameof(TotalItems));
        OnPropertyChanged(nameof(TotalPages));
        OnPropertyChanged(nameof(StartItem));
        OnPropertyChanged(nameof(EndItem));
        OnPropertyChanged(nameof(InfoText));
        OnPropertyChanged(nameof(PageNumbers));
    }

    private void UpdatePageNumbers()
    {
        PageNumbers.Clear();
        for(int i = 1; i <= TotalPages; i++)
        {
            PageNumbers.Add(i);
        }
    }

    public void GoToPage(int targetPage)
    {
        // 1. Validar que la nueva pagina no sea menor a 1 ni mayor a TotalPages
        if (targetPage < 1 || (TotalPages > 0 && targetPage > TotalPages)) return;

        // Evitar disparar evento si la pagina solicitada es la misma actual
        if (targetPage == CurrentPage) return;

        // 2. Actualizar CurrentPage
        CurrentPage = targetPage;

        // 3. Disparar PageChange?.Invoke(this, CurrentPage)
        PageChanged?.Invoke(this, CurrentPage);
     }

    private void OnPageNumberClick(object sender, RoutedEventArgs e)
    {
        if(sender is Button button && button.DataContext is int pageNumber)
        {
            GoToPage(pageNumber);
        }
    }

    private void OnPreviousPageClick(object sender, RoutedEventArgs e)
    {
        GoToPage(CurrentPage - 1 );
    }
    private void OnNextPageClick(object sender, RoutedEventArgs e)
    {
        GoToPage(CurrentPage + 1);
    }



    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
