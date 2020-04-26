using System;
using System.Globalization;
using System.Windows.Data;

namespace KTANEAssistant.Converter
{
    public class FontSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (ConverterUtil.SafeConvert(value, out double x))
            {
                bool paramDouble = ConverterUtil.SafeConvert(parameter, out double y);
                if (!paramDouble)
                {
                    y = 0.6;
                }
                double fontSize = x * y - 4;
                return fontSize > 8 ? fontSize : 8;
            }
            return 20;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (ConverterUtil.SafeConvert(value, out double x))
            {
                bool paramDouble = ConverterUtil.SafeConvert(parameter, out double y);
                if (!paramDouble)
                {
                    y = 5 / 3;
                }
                return y * (x + 4);
            }
            return 0;
        }
    }
}
