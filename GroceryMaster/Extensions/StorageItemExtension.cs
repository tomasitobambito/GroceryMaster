using System.Collections.ObjectModel;
using System.IO;
using GroceryMaster.Handlers;
using GroceryMaster.Model;

namespace GroceryMaster.Extensions
{
    public static class StorageItemExtension
    {
        public static void SaveToFile(this ObservableCollection<StorageItem> storageItems)
        {
            var path = FileHandler.GetAppDataFile("StorageItems.json");
            FileHandler.WriteToFile(path, storageItems);
        }
    }
}