/*
using System;
using System.Windows.Data;
using GroceryMaster.Enums;
using GroceryMaster.Extensions;

namespace GroceryMaster.Converters
{
    public class ItemCategoryEnumToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, 
            System.Globalization.CultureInfo culture)
        {
            return (ItemCategory)value;
        }

        public object ConvertBack(string value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return ItemCategoryExtension.GetCategoryFromDescript(value);
        }
    }
}
*/