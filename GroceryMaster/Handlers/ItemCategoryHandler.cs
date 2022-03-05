using System;
using System.Collections.Generic;
using System.Linq;
using GroceryMaster.Enums;
using GroceryMaster.Extensions;

namespace GroceryMaster.Handlers
{
    public class ItemCategoryHandler
    {
        public List<string> ItemCategoryStrings { get; }
        
        public ItemCategoryHandler()
        {
            ItemCategoryStrings = new List<string>();
            
            foreach (ItemCategory value in Enum.GetValues(typeof(ItemCategory)).Cast<ItemCategory>())
            {
                ItemCategoryStrings.Add(value.GetDescript());
            }
        }
    }
}