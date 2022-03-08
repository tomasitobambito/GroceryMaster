using System;
using System.Collections.ObjectModel;
using GroceryMaster.Enums;
using GroceryMaster.Handlers;

namespace GroceryMaster.Model
{
    public class StorageItem
    {
        public int UID { get; set; }
        public string Description { get; set; }
        public ItemCategory Category { get; set; }
        public DateTime? BestBefore { get; set; }

        public static ObservableCollection<StorageItem> GetStorageItems()
        {
            var path = FileHandler.GetAppDataFile("StorageItems.json");
            try
            {
                return FileHandler.ReadFromJSONFile<ObservableCollection<StorageItem>>(path);
            }
            catch
            {
                return new ObservableCollection<StorageItem>();
            }
        }
    }
}