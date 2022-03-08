using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using GroceryMaster.Enums;
using GroceryMaster.Model;

namespace GroceryMaster.Dialogs
{
    public partial class ShoppingItemInputDialog : Window
    {
        public List<ItemCategory> ItemCategories { get; }

        public ShoppingItem NewItem = new();
        
        public string ButtonText { get; }
        public string WindowTitle { get; }

        public ShoppingItemInputDialog(string buttonText, string windowTitle)
        {
            ButtonText = buttonText;
            WindowTitle = windowTitle;
            
            ItemCategories = new List<ItemCategory>();

            foreach (ItemCategory value in Enum.GetValues(typeof(ItemCategory)).Cast<ItemCategory>())
                ItemCategories.Add(value);

            InitializeComponent();
            DataContext = this;
        }

        private void BtnDialogOk_OnClick(object sender, RoutedEventArgs e)
        {
            if (TxtBoxDescript.Text != "" && CmbBox.SelectedItem != null)
            {
                NewItem.Description = TxtBoxDescript.Text;
                NewItem.Note = TxtBoxNote.Text;
                NewItem.Category = (ItemCategory) CmbBox.SelectedItem;
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("You have to enter a category and an item");
            }
        }
    }
}