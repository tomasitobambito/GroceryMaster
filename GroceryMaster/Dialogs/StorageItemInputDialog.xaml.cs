using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using GroceryMaster.Enums;
using GroceryMaster.Model;

namespace GroceryMaster.Dialogs
{
    public partial class StorageItemInputDialog : Window
    {
        public List<ItemCategory> ItemCategories { get; set; }

        public StorageItem NewItem = new();
        
        public string ButtonText { get; }
        public string WindowTitle { get; }

        public StorageItemInputDialog()
        {
            ButtonText = "Add Item";
            WindowTitle = "Add Storage Item";
            
            Initialize();
        }

        public StorageItemInputDialog(StorageItem editItem)
        {
            ButtonText = "Edit Item";
            WindowTitle = "Edit Storage Item";
            
            Initialize();

            TxtBox.Text = editItem.Description;
            CmbBox.SelectedItem = editItem.Category;
            DatePicker.SelectedDate = editItem.BestBefore;
        }

        private void Initialize()
        {
            ItemCategories = new List<ItemCategory>();
            
            foreach (ItemCategory value in Enum.GetValues(typeof(ItemCategory)).Cast<ItemCategory>())
                ItemCategories.Add(value);
            
            InitializeComponent();
            DataContext = this;
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