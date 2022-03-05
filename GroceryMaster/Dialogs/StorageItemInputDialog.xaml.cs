using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using GroceryMaster.Enums;
using GroceryMaster.Extensions;
using GroceryMaster.Model;

namespace GroceryMaster.Dialogs
{
    public partial class StorageItemInputDialog : Window
    {
        private readonly List<string> _itemCategories = new();
        public List<string> ItemCategories => _itemCategories;

        public StorageItem NewItem = new StorageItem
        {
            Description = "dumbass made thing that doesn't work",
            Category = ItemCategory.BA
        };

        public StorageItemInputDialog()
        {
            foreach (var value in Enum.GetValues(typeof(ItemCategory)).Cast<ItemCategory>())
            {
                _itemCategories.Add(value.GetDescript());
            }

            DataContext = this;
            InitializeComponent();
        }

        private void BtnDialogOk_OnClick(object sender, RoutedEventArgs e)
        {
            NewItem.Description = TxtBox.Text;
            NewItem.Category = ItemCategoryExtension.GetCategoryFromDescript(CmbBox.SelectedItem.ToString());
            NewItem.BestBefore = DatePicker.SelectedDate;
            DialogResult = true;
        }
    }
}