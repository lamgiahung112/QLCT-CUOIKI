using System;
using System.Globalization;
using System.Windows.Data;

namespace CuoiKi.ValueConverters
{
    public class TooltipPercentageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue)
            {
                return string.Format("{0} % complete", intValue);
            }
            return "0% complete";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
