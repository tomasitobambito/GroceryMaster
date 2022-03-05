using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using GroceryMaster.Enums;
using GroceryMaster.Extensions;
using GroceryMaster.Model;

namespace GroceryMaster.Dialogs
{
    public partial class ShoppingItemInputDialog : Window
    {
        public List<string> ItemCategories { get; }

        public ShoppingItem NewItem = new();

        public ShoppingItemInputDialog()
        {
            ItemCategories = new List<string>();

            foreach (ItemCategory value in Enum.GetValues(typeof(ItemCategory)).Cast<ItemCategory>())
                ItemCategories.Add(value.GetDescript());

            InitializeComponent();
            DataContext = this;
        }

        private void BtnDialogOk_OnClick(object sender, RoutedEventArgs e)
        {
            if (TxtBoxDescript.Text != "" && CmbBox.SelectedItem != null)
            {
                NewItem.Description = TxtBoxDescript.Text;
                NewItem.Note = TxtBoxNote.Text;
                NewItem.Category = ItemCategoryExtension.GetCategoryFromDescript(CmbBox.SelectedItem.ToString());
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("You have to enter a category and an item");
            }
        }
    }
}