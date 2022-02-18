using System.Windows;
using GroceryMaster.View;

namespace GroceryMaster
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var win = new MainWindowView
            {
                Title = "Grocery Master"
            };
            win.Show();
        }
    }
}