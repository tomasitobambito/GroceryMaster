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
        // creating properties for the UI to bind to
        public List<ItemCategory> ItemCategories { get; set; }

        public StorageItem NewItem = new();
        
        public string ButtonText { get; }
        public string WindowTitle { get; }

        // two separate constructors for creating a new item or editing an existing one
        public StorageItemInputDialog() // creating a new item
        {
            ButtonText = "Add Item";
            WindowTitle = "Add Storage Item";
            
            Initialize();
        }

        public StorageItemInputDialog(StorageItem editItem) // editing an existing item
        {
            ButtonText = "Edit Item";
            WindowTitle = "Edit Storage Item";
            
            Initialize();

            TxtBox.Text = editItem.Description;
            CmbBox.SelectedItem = editItem.Category;
            DatePicker.SelectedDate = editItem.BestBefore;
        }

        // a method to house the overlap between creating a new item or editing one
        private void Initialize()
        {
            ItemCategories = new List<ItemCategory>();
            
            foreach (ItemCategory value in Enum.GetValues(typeof(ItemCategory)).Cast<ItemCategory>())
                ItemCategories.Add(value);
            
            InitializeComponent();
            DataContext = this;
        }

        // method to handle what happens when the ok button is clicked
        private void BtnDialogOk_OnClick(object sender, RoutedEventArgs e)
        {
            // checks to see if required fields have been filled
            if (TxtBox.Text != "" && CmbBox.SelectedItem != null)
            {
                NewItem.Description = TxtBox.Text;
                NewItem.Category = (ItemCategory) CmbBox.SelectedItem;
                NewItem.BestBefore = DatePicker.SelectedDate;
                DialogResult = true;   
            }
            else
            {
                // informs user that not everything is entered correctly
                MessageBox.Show("You have to enter a category and an item");
            }
        }
    }
}