using System;
using System.Globalization;
using System.Windows.Data;

namespace KTANEAssistant.Converter
{
    public class DivisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (ConverterUtil.SafeConvert(value, out double x) && ConverterUtil.SafeConvert(parameter, out double y))
            {
                return x / y;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (ConverterUtil.SafeConvert(value, out double x) && ConverterUtil.SafeConvert(parameter, out double y))
            {
                return x * y;
            }
            return value;
        }
    }
}
