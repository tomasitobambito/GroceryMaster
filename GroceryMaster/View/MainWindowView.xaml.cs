using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using GroceryMaster.Model;
using GroceryMaster.ViewModel;

namespace GroceryMaster.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        private MainWindowViewModel viewModel;
        public MainWindowView()
        {
            viewModel = new MainWindowViewModel();

            DataContext = viewModel;
            InitializeComponent();

            // create CollectionViews of both lists and add filter method to both
            CollectionView storageView = (CollectionView) CollectionViewSource.GetDefaultView(LvStorage.ItemsSource);
            storageView.Filter = ItemFilter;
            CollectionView shoppingView = (CollectionView) CollectionViewSource.GetDefaultView(LvShopping.ItemsSource);
            shoppingView.Filter = ItemFilter;
        }

        private void MainWindowView_OnClosed(object? sender, EventArgs e) // pass OnClosed event to viewModel
        {
            viewModel.OnWindowClosed();
        }

        // Re-sort when a new sort description is selected
        private void ComboBoxStorage_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StorageSortChanged();
        }

        // Re-sort when a new sort description is selected
        private void ComboBoxShopping_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShoppingSortChanged();
        }

        private void TextFilter_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(viewModel.SelectedTabIndex == 0 ? 
                    LvStorage.ItemsSource : LvShopping.ItemsSource).Refresh();
        }

        private bool ItemFilter(object item)
        {
            if (string.IsNullOrEmpty(TextFilter.Text)) return true;

            if (viewModel.SelectedTabIndex == 0)
            {
                return ((StorageItem) item).Description
                    .IndexOf(TextFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0;
            }

            return ((ShoppingItem) item).Description
                .IndexOf(TextFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            StorageSortChanged();
            ShoppingSortChanged();
        }

        private void StorageSortChanged()
        {
            if (!ComboBoxStorage.IsLoaded) return;

            var args = ((string) ((ComboBoxItem) ComboBoxStorage.SelectedItem).Content).Split("-");
            var dir = (args[1] == "Ascending") ? ListSortDirection.Ascending : ListSortDirection.Descending;

            LvStorage.Items.SortDescriptions.Clear(); 

            if (args[0] == "Description")
            {
                LvStorage.Items.SortDescriptions.Add(new SortDescription("Description", ListSortDirection.Ascending));
            }
            else
            {
                LvStorage.Items.SortDescriptions.Add(new SortDescription(args[0], dir));
                LvStorage.Items.SortDescriptions.Add(new SortDescription("Description", ListSortDirection.Ascending));
            }
        }

        private void ShoppingSortChanged()
        {
            if (!ComboBoxShopping.IsLoaded) return;

            // determine direction of sort based on second half of 
            var dir = ((string) ((ComboBoxItem) ComboBoxShopping.SelectedItem).Content).Split("-")[1]
                      == "Ascending" ? ListSortDirection.Ascending : ListSortDirection.Descending;
            
            LvShopping.Items.SortDescriptions.Clear();
            LvShopping.Items.SortDescriptions.Add(new SortDescription("Description", dir));
        }

        private void OnTabChanged(object sender, SelectionChangedEventArgs e)
        {
            TextFilter.Text = "";
        }
    }
}