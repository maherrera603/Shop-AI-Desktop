using System;
using System.Collections.Generic;
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

namespace ShopAIDesktop.UI.Components.CustomSearch;

/// <summary>
/// Lógica de interacción para CustomSearch.xaml
/// </summary>
public partial class CustomSearch : UserControl
{
    public CustomSearch()
    {
        InitializeComponent();
    }

    #region Eventos Expuestos para el exterior
    public event EventHandler<string>? SearchTrigged;
    public event EventHandler<string>? StatusChanged;
    public event EventHandler<bool>? ViewModeChanged;

    #endregion


    #region Propiedades de lectura publica
    public string SearchText => SearchBox.Text.Trim();

    public string SelectedStatus
    {
        get
        {
            if (StatusComboBox.SelectedItem is ComboBoxItem selectedItem)
                return selectedItem.Content.ToString() ?? "Todas";
            return "Todas";
        }
    }

    #endregion

    #region Manejadores de Eventos internos
    private void HandleSearchTextBox_KeyDown(object sender, KeyEventArgs e) {
        if (e.Key == Key.Enter)
        {
            SearchTrigged?.Invoke(this, SearchText);
        }
    }

    private void HandleStatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) { 
        if(IsLoaded){
            StatusChanged?.Invoke(this, SelectedStatus);
        }
    }

    private void HandleTableViewButton_Click(object sender, RoutedEventArgs e) =>
        ViewModeChanged?.Invoke(this, false);
    

    private void HandleCardsViewButton_Click(object sender, RoutedEventArgs e) =>   
        ViewModeChanged?.Invoke(this, true);
    
    #endregion

}
