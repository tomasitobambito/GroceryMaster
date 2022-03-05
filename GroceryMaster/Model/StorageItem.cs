﻿using System;
using GroceryMaster.Enums;

namespace GroceryMaster.Model
{
    public class StorageItem
    {
        public string Description { get; set; }
        public ItemCategory Category { get; set; }
        public DateTime? BestBefore { get; set; }
    }
}