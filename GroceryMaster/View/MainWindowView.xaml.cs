using System.Windows;
using GroceryMaster.ViewModel;

namespace GroceryMaster.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        public MainWindowView()
        {
            var viewModel = new MainWindowViewModel();

            DataContext = viewModel;
            InitializeComponent();
        }
    }
}