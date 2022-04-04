using System.Collections.ObjectModel;
using GroceryMaster.Enums;
using GroceryMaster.Handlers;

namespace GroceryMaster.Model
{
    public class ShoppingItem
    {
        public int UID { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public ItemCategory Category { get; set; }

        public static ObservableCollection<ShoppingItem> GetShoppingItems()
        {
            var path = FileHandler.GetListFile("ShoppingItems.json");
            try // using try catch block in case file is not found
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