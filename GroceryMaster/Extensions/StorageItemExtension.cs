using System.Collections.ObjectModel;
using GroceryMaster.Handlers;
using GroceryMaster.Model;

namespace GroceryMaster.Extensions
{
    public static class StorageItemExtension
    {
        public static void SaveToFile(this ObservableCollection<StorageItem> storageItems)
        {
            var path = FileHandler.GetListFile("StorageItems.json");
            FileHandler.WriteToFile(path, storageItems);
        }
    }
}