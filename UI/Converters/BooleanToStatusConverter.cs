using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace ShopAIDesktop.UI.Converters;

public class BooleanToStatusConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object paramenter, CultureInfo culture)
    {
        if (value is bool isActive) return isActive ? "Activa" : "Inactiva";
        return "Inactiva";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
