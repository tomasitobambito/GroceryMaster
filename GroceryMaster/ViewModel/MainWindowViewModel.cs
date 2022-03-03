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

        public string TextText
        {
            get => _testText;
            set => SetProperty(ref _testText, value);
        }

        public MainWindowViewModel()
        {
            _testText = "Ben Dover";
            _newEntryCommand = new CommandHandler(OnNewEntry, CanNewEntry);
        }

        public void OnNewEntry(object commandParameter)
        {
            InputTextDialog inputDialog = new InputTextDialog("Please enter your name:", "John Doe");
            if (inputDialog.ShowDialog() == true)
                _testText = inputDialog.Answer;
            
            _newEntryCommand.InvokeCanExecuteChanged();
        }

        public bool CanNewEntry(object commandParameter)
        {
            return true;
        }
    }
}