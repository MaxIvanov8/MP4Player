using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Mp4Player
{
    internal class Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
                return ((TimeSpan) value).ToString(@"hh\:mm\:ss\:ff");
            return 0d;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
