using System.Collections.Generic;
using System.Windows;

namespace GroceryMaster.Dialogs
{
    public partial class StorageItemInputDialog : Window
    {
        private readonly List<string> _itemCategories;
        public List<string> ItemCategories => _itemCategories;

        public StorageItemInputDialog()
        {
            _itemCategories = new List<string>
            {
                "Item1",
                "Item2",
                "Item3"
            };

            DataContext = this;
            InitializeComponent();
        }

        private void BtnDialogOk_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        public string Selected => CmbBox.SelectedItem.ToString();
    }
}