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
        public List<ItemCategory> ItemCategories { get; }

        public StorageItem NewItem = new();

        public StorageItemInputDialog()
        {
            ItemCategories = new List<ItemCategory>();
            
            foreach (ItemCategory value in Enum.GetValues(typeof(ItemCategory)).Cast<ItemCategory>())
                ItemCategories.Add(value);

            DataContext = this;
            InitializeComponent();
        }

        private void BtnDialogOk_OnClick(object sender, RoutedEventArgs e)
        {
            if (TxtBox.Text != "" && CmbBox.SelectedItem != null)
            {
                NewItem.Description = TxtBox.Text;
                NewItem.Category = (ItemCategory) CmbBox.SelectedItem;
                NewItem.BestBefore = DatePicker.SelectedDate;
                DialogResult = true;   
            }
            else
            {
                MessageBox.Show("You have to enter a category and an item");
            }
        }
    }
}