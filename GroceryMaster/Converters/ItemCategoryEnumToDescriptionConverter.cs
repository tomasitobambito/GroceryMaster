using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;
using GroceryMaster.Enums;

namespace GroceryMaster.Converters
{
    public class ItemCategoryEnumToDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DescriptionAttribute[] descriptionAttributes = (DescriptionAttribute[]) ((ItemCategory)value)
                .GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return descriptionAttributes[0].Description;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            foreach (var field in typeof(ItemCategory).GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == (string) value)
                        return (ItemCategory) field.GetValue(null);
                }
            }

            throw new ArgumentException("Not Found.", nameof(value));
        }
    }
}