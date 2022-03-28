using System;
using System.Globalization;
using System.Windows.Data;

namespace GroceryMaster.Converters
{
    public class DateTimeToDateStringConverter : IValueConverter
    {
        // Convert from DateTime to String
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString()?.Split(" ")[0]; // since DateTime is nullable the return of this function must be too
        }
        
        // Convert from String to DateTime
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? null : DateTime.Parse((string) value); // since value is nullable, the return must account for it
        }
    }
}