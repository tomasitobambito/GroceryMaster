using System;
using System.Windows;
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
    }
}