using KTANEAssistant.API;
using System;
using System.Globalization;
using System.Windows.Data;

namespace KTANEAssistant.Converter
{
    public class DependencyStringifier : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DependencyFlag[] flags)
            {
                return flags.ToExtendedString();
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
