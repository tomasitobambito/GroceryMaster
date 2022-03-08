﻿using System.Collections.ObjectModel;
using GroceryMaster.Enums;
using GroceryMaster.Handlers;

namespace GroceryMaster.Model
{
    public class ShoppingItem
    {
        public string Description { get; set; }
        public string Note { get; set; }
        public ItemCategory Category { get; set; }
        
        public static ObservableCollection<ShoppingItem> GetShoppingItems()
        {
            var path = FileHandler.GetAppDataFile("ShoppingItems.json");
            try
            {
                return FileHandler.ReadFromJSONFile<ObservableCollection<ShoppingItem>>(path);
            }
            catch
            {
                return new ObservableCollection<ShoppingItem>();
            }
        }
    }
}