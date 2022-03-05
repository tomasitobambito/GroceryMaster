using System;
using System.ComponentModel;
using GroceryMaster.Enums;

namespace GroceryMaster.Extensions
{
    public static class ItemCategoryExtension
    {
        /// <summary>
        /// Can only access enums in ItemCategory (do not try with others or it will break)
        /// </summary>
        /// <param name="category">Category shorthand (this is an enum not a string!!!)</param>
        /// <returns>Category description as string</returns>
        public static string GetDescript(this ItemCategory category)
        {
            DescriptionAttribute[] descriptionAttributes = (DescriptionAttribute[]) category
                .GetType()
                .GetField(category.ToString())?
                .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return descriptionAttributes != null && descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : "";
        }

        public static ItemCategory GetCategoryFromDescript(string descript)
        {
            foreach (var field in typeof(ItemCategory).GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == descript)
                        return (ItemCategory) field.GetValue(null);
                }
                else
                {
                    if (field.Name == descript)
                        return (ItemCategory) field.GetValue(null);
                }
            }

            throw new ArgumentException("Not Found.", nameof(descript));
        }
    }
}