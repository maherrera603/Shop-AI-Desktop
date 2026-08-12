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

namespace ShopAIDesktop.UI.Components.CustomAlert
{
    /// <summary>
    /// Lógica de interacción para CustomAlert.xaml
    /// </summary>
    public partial class CustomAlert : Window
    {
        public CustomAlert(AlertType alertType, string message)
        {
            InitializeComponent();
            MessageText.Text = message.ToLowerInvariant();
            SetAlertIcon(alertType);
        }

        public void HandleAccept(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SetAlertIcon(AlertType type)
        {
            string icon = type switch
            {
                AlertType.Success => "checkmark.svg",
                AlertType.Warning => "warning.svg",
                _ => throw new ArgumentOutOfRangeException()
            };

            AlertIcon.Source = new Uri($"pack://application:,,,/Assets/Icons/{icon}", UriKind.Absolute);
        }
    }
}
