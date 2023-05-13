using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace CuoiKi.ValueConverters
{
    public class IntToColorProgressBarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            MessageBox.Show(value.ToString());
            if (value is int intValue)
            {
                if (intValue < 30)
                {
                    return Brushes.Red;
                }
                else if (intValue < 50)
                {
                    return new SolidColorBrush(Color.FromRgb(255, 172, 0));
                }
                else if (intValue < 80)
                {
                    return new SolidColorBrush(Color.FromRgb(249, 255, 85));
                }
                else
                {
                    return new SolidColorBrush(Color.FromRgb(0, 255, 25));
                }
            }
            return Brushes.Gray;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}