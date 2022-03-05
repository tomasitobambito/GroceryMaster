using System;
using GroceryMaster.Enums;

namespace GroceryMaster.Model
{
    public class StorageItem
    {
        public string Description;
        public ItemCategory Category;
        public DateTime? BestBefore;
    }
}