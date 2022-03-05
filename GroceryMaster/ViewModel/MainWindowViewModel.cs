using System.Text.Json;
using System.Windows;
using System.Windows.Input;
using GroceryMaster.Dialogs;
using GroceryMaster.Handlers;

namespace GroceryMaster.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly CommandHandler _newEntryCommand;
        public ICommand NewEntryCommand => _newEntryCommand;

        private string _testText;

        public string TestText
        {
            get => _testText;
            set
            {
                SetProperty(ref _testText, value);
            }
        }

        public MainWindowViewModel()
        {
            _testText = "Ben Dover";
            _newEntryCommand = new CommandHandler(OnNewEntry, CanNewEntry);
        }

        public void OnNewEntry(object commandParameter)
        {
            StorageItemInputDialog inputDialog = new StorageItemInputDialog();
            if (inputDialog.ShowDialog() == true)
                MessageBox.Show(inputDialog.NewItem.Description);
            TestText = "this is some new text, take that ruben";

            _newEntryCommand.InvokeCanExecuteChanged();
            
            //subject to change (this is for testing atm)
        }

        public bool CanNewEntry(object commandParameter)
        {
            return true;
            
            //subject to change (this is for testing atm)
        }
    }
}