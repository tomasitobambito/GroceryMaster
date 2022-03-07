using System.Collections.ObjectModel;
using GroceryMaster.Handlers;
using GroceryMaster.Model;

namespace GroceryMaster.Extensions
{
    public static class ShoppingItemExtension
    {
        public static void SaveToFile(this ObservableCollection<ShoppingItem> shoppingItems)
        {
            var path = FileHandler.GetAppDataFile("ShoppingItems.json");
            FileHandler.WriteToFile(path, shoppingItems);
        }
    }
}