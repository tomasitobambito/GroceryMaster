using System;
using System.Globalization;
using System.Windows.Data;

namespace GroceryMaster.Converters
{
    public class DateTimeToDateStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString()?.Split(" ")[0];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? null : DateTime.Parse((string) value);
        }
    }
}