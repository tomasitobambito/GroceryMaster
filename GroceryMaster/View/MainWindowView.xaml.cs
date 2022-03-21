using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
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
        }

        private void MainWindowView_OnClosed(object? sender, EventArgs e)
        {
            viewModel.OnWindowClosed();
        }

        private void ComboBoxStorage_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
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

        private void ComboBoxShopping_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!ComboBoxShopping.IsLoaded) return;

            var dir = ((string) ((ComboBoxItem) ComboBoxShopping.SelectedItem).Content).Split("-")[1]
                      == "Ascending" ? ListSortDirection.Ascending : ListSortDirection.Descending;
            
            LvShopping.Items.SortDescriptions.Clear();
            LvShopping.Items.SortDescriptions.Add(new SortDescription("Description", dir));
        }
    }
}