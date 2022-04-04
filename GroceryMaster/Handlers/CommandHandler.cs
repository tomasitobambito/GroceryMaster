using System;
using System.Windows.Input;

namespace GroceryMaster.Handlers
{
    public class CommandHandler : ICommand
    {
        // create private fields
        private readonly Action<object> _executeAction;
        private readonly Func<object, bool> _canExecuteAction;

        public CommandHandler(Action<object> executeAction, Func<object, bool> canExecuteAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = canExecuteAction;
        }

        // assign public methods accessed by view to private fields
        public void Execute(object parameter) => _executeAction(parameter);

        public bool CanExecute(object parameter) => _canExecuteAction?.Invoke(parameter) ?? true;

        /* updates the CanExecute method every time something in the view changes that could affect the commands
        ability to execute */
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested += value;
        }
    }
}