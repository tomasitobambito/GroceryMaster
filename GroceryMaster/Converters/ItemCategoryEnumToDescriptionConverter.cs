using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;
using GroceryMaster.Enums;
// ReSharper disable PossibleNullReferenceException <- Disables a hint that isn't useful in this case

namespace GroceryMaster.Converters
{
    public class ItemCategoryEnumToDescriptionConverter : IValueConverter
    {
        // Convert from enum to description string
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // read description attributes from each enum
            DescriptionAttribute[] descriptionAttributes = (DescriptionAttribute[]) ((ItemCategory)value)
                .GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return descriptionAttributes[0].Description;
        }

        // Convert from description string to enum
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // finding an enum from its description attribute
            foreach (var field in typeof(ItemCategory).GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is not DescriptionAttribute
                    attribute) continue;
                if (attribute.Description == (string) value)
                    return (ItemCategory) field.GetValue(null);
            }

            throw new ArgumentException("Not Found.", nameof(value)); // throw exception if not found
        }
    }
}