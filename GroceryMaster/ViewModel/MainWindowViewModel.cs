using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using GroceryMaster.Dialogs;
using GroceryMaster.Extensions;
using GroceryMaster.Handlers;
using GroceryMaster.Logic;
using GroceryMaster.Model;

namespace GroceryMaster.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private SettingsLogic _settings;
        
        private Func<SettingsLogic, int> Increment = settings => // Increment current highest ID on new Item creation
        {
            int ID = settings.User.CurrentHighestIndex;
            settings.User.CurrentHighestIndex += 1;
            return ID;
        };
        
        // Define private fields for direct access and public properties for GUI binding
        // Commands
        private readonly CommandHandler _newEntryCommand;
        public ICommand NewEntryCommand => _newEntryCommand;

        private readonly CommandHandler _deleteEntriesCommand;
        public ICommand DeleteEntriesCommand => _deleteEntriesCommand;

        private readonly CommandHandler _editEntryCommand;
        public ICommand EditEntryCommand => _editEntryCommand;

        // ListView ItemSources
        private ObservableCollection<StorageItem> _storageItems;
        public ObservableCollection<StorageItem> StorageItems
        {
            get => _storageItems;
            set => SetProperty(ref _storageItems, value);
        }

        private ObservableCollection<StorageItem> _selectedStorageItems;
        public ObservableCollection<StorageItem> SelectedStorageItems
        {
            get => _selectedStorageItems;
            set => SetProperty(ref _selectedStorageItems, value); 
        }

        // Selected Items attached property
        private ObservableCollection<ShoppingItem> _shoppingItems;
        public ObservableCollection<ShoppingItem> ShoppingItems
        {
            get => _shoppingItems;
            set => SetProperty(ref _shoppingItems, value);
        }

        private ObservableCollection<ShoppingItem> _selectedShoppingItems;
        public ObservableCollection<ShoppingItem> SelectedShoppingItems
        {
            get => _selectedShoppingItems;
            set => SetProperty(ref _selectedShoppingItems, value);
        }

        // Miscellaneous
        private int _selectedTabIndex;
        public int SelectedTabIndex
        {
            get => _selectedTabIndex;
            set => SetProperty(ref _selectedTabIndex, value);
        }

        private int _storageSortIndex;
        public int StorageSortIndex
        {
            get => _storageSortIndex;
            set => SetProperty(ref _storageSortIndex, value);
        }

        private int _shoppingSortIndex;
        public int ShoppingSortIndex
        {
            get => _shoppingSortIndex;
            set => SetProperty(ref _shoppingSortIndex, value);
        }

        // Constructor, gathers all data needed from settings and appdata, then initialises fields
        public MainWindowViewModel()
        {
            _storageItems = StorageItem.GetStorageItems();
            _shoppingItems = ShoppingItem.GetShoppingItems();
            
            _selectedStorageItems = new ObservableCollection<StorageItem>();
            _selectedShoppingItems = new ObservableCollection<ShoppingItem>();

            _newEntryCommand = new CommandHandler(OnNewEntry, CanNewEntry);
            _deleteEntriesCommand = new CommandHandler(OnDeleteEntries, CanDeleteEntries);
            _editEntryCommand = new CommandHandler(OnEditEntry, CanEditEntry);

            _settings = new SettingsLogic();
            _selectedTabIndex = _settings.User.SelectedTabIndex;
            _storageSortIndex = _settings.User.StorageSortIndex;
            _shoppingSortIndex = _settings.User.ShoppingSortIndex;
        }

        // handling command functionality
        private void OnNewEntry(object commandParameter) // Execute New Entry
        {
            if (_selectedTabIndex == 0) // executed if tab is Storage
            {
                StorageItemInputDialog inputDialog = new(); 
                if (inputDialog.ShowDialog() == true) // if the ok button has been pressed
                {
                    inputDialog.NewItem.UID = Increment(_settings);
                    StorageItems.Add(inputDialog.NewItem);
                }
            }
            else // executed if tab is Shopping
            {
                ShoppingItemInputDialog inputDialog = new(); 
                if (inputDialog.ShowDialog() == true) // if the ok button has been pressed
                {
                    inputDialog.NewItem.UID = Increment(_settings);
                    ShoppingItems.Add(inputDialog.NewItem);
                }
            }
        }

        private bool CanNewEntry(object commandParameter) // Check if can execute new Entry
        {
            return true; // can always add new entry, no conditions
        }
        
        private void OnDeleteEntries(object commandParameter) // Execute Delete Entries
        {
            switch (_selectedTabIndex)
            {
                case 0: // executed if tab is storage
                    // loop through all selected items
                    foreach (StorageItem storageItem in new ObservableCollection<StorageItem>(_selectedStorageItems))
                    {
                        // remove item with the same UID as the storageItem
                        StorageItems.Remove(StorageItems.Single(i => i.UID == 
                                                                     storageItem.UID)); 
                    }
                    break;
                case 1: // executed if tab is shopping
                    // loop through all selected items
                    foreach (ShoppingItem shoppingItem in new 
                        ObservableCollection<ShoppingItem>(_selectedShoppingItems))
                    {
                        // remove item with the same UID as shoppingItem
                        ShoppingItems.Remove(ShoppingItems.Single(i => i.UID ==
                                                                       shoppingItem.UID));
                    }
                    break;
            }
        }

        private bool CanDeleteEntries(object commandParameter) // Check if can execute delete entries
        {
            // checks that the SelectedItems list of the current tab is not empty
            return _selectedTabIndex == 0 && _selectedStorageItems.Count > 0 ||
                   _selectedTabIndex == 1 && _selectedShoppingItems.Count > 0;
        }
        
        private void OnEditEntry(object commandParameter) // Execute edit entry
        {
            if (_selectedTabIndex == 0) // executed if tab is Storage
            {
                StorageItem selectedStorageItem = _selectedStorageItems.First(); // get only item in selectedItems
                StorageItemInputDialog inputDialog = new(selectedStorageItem);

                if (inputDialog.ShowDialog() == true) // if ok button was pressed
                {
                    inputDialog.NewItem.UID = selectedStorageItem.UID; // give NewItem the UID of the one it replaces
                    StorageItems.Remove(selectedStorageItem);
                    StorageItems.Add(inputDialog.NewItem);
                }
            }
            else // executed if tab is Shopping
            {
                ShoppingItem selectedShoppingItem = _selectedShoppingItems.First(); // get only item in selectedItems
                ShoppingItemInputDialog inputDialog = new(selectedShoppingItem);

                if (inputDialog.ShowDialog() == true) // if ok button was pressed
                {
                    inputDialog.NewItem.UID = selectedShoppingItem.UID; // give NewItem the UID of the one it replaces
                    ShoppingItems.Remove(selectedShoppingItem);
                    ShoppingItems.Add(inputDialog.NewItem);
                }
            }
        }

        private bool CanEditEntry(object commandParameter) // Check if can execute edit entry
        {
            // checks if SelectedItems list of current tab contains exactly one item
            return _selectedTabIndex == 0 && _selectedStorageItems.Count == 1 ||
                   _selectedTabIndex == 1 && _selectedShoppingItems.Count == 1;
        }

        public void OnWindowClosed() // called by the codebehind when the window is closed
        {
            _settings.User.SelectedTabIndex = _selectedTabIndex;
            _settings.User.StorageSortIndex = _storageSortIndex;
            _settings.User.ShoppingSortIndex = _shoppingSortIndex;
            _settings.SaveUserSettings();
            _storageItems.SaveToFile();
            _shoppingItems.SaveToFile();
        }
    }
}