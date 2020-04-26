using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace KTANEAssistant.Converter
{
    public class IntToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                if ((int)value > 0) return Visibility.Collapsed;
            }

            return Visibility.Visible;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool negate = false;
            if (parameter is string y)
            {
                negate = y.Equals("True");
            }
            if (value is bool x)
            {
                if (negate && !x || !negate && x)
                {
                    return Visibility.Visible;
                }
            }
            return Visibility.Hidden;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility x)
            {
                if (x.Equals(Visibility.Visible)) return true;
            }
            return false;
        }
    }
}
