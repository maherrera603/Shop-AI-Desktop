using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace ShopAIDesktop.UI.Components.ConfirmationAlert;

/// <summary>
/// Lógica de interacción para ConfirmationAlert.xaml
/// </summary>
public partial class ConfirmationAlert : Window
{
    public string _titleText = "Confirmar accíon";
    public string _messageText = "¿Estás seguro que deseas realizar esta accíon?";
    public string _confirmText = "Confirmar";
    public string _cancelText = "Cancelar";
    public ConfirmationAlert()
    {
        InitializeComponent();
    }

    public string TitleText
    {
        get => _titleText;
        set
        {
            _titleText = value;
            OnPropertyChanged();
        }
    }

    public string MessageText
    {
        get => _messageText;
        set
        {
            _messageText = value;
            OnPropertyChanged();
        }
    }

    public string ConfirmText
    {
        get => _confirmText;
        set
        {
            _confirmText = value;
            OnPropertyChanged();
        }
    }

    public string CancelText
    {
        get => _cancelText;
        set
        {
            _cancelText = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void HandleCancel_Click(object sender, RoutedEventArgs e) => DialogResult = false; 

    private void HandleConfirm_Click(object sender, RoutedEventArgs e) => DialogResult = true;


}
