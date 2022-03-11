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

        private Func<SettingsLogic, int> Increment = settings =>
        {
            int ID = settings.User.CurrentHighestIndex;
            settings.User.CurrentHighestIndex += 1;
            return ID;
        };

        private readonly CommandHandler _newEntryCommand;
        public ICommand NewEntryCommand => _newEntryCommand;

        private readonly CommandHandler _deleteEntriesCommand;
        public ICommand DeleteEntriesCommand => _deleteEntriesCommand;

        private readonly CommandHandler _editEntryCommand;
        public ICommand EditEntryCommand => _editEntryCommand;

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

        private int _selectedTabIndex;
        public int SelectedTabIndex
        {
            get => _selectedTabIndex;
            set => SetProperty(ref _selectedTabIndex, value);
        }

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
        }

        private void OnNewEntry(object commandParameter)
        {
            if (_selectedTabIndex == 0)
            {
                StorageItemInputDialog inputDialog = new();
                if (inputDialog.ShowDialog() == true)
                {
                    inputDialog.NewItem.UID = Increment(_settings);
                    StorageItems.Add(inputDialog.NewItem);
                }
            }
            else
            {
                ShoppingItemInputDialog inputDialog = new(); 
                if (inputDialog.ShowDialog() == true)
                {
                    inputDialog.NewItem.UID = Increment(_settings);
                    ShoppingItems.Add(inputDialog.NewItem);
                }
            }
        }

        private bool CanNewEntry(object commandParameter)
        {
            return true;
        }
        
        private void OnDeleteEntries(object commandParameter)
        {
            switch (_selectedTabIndex)
            {
                case 0:
                    foreach (StorageItem storageItem in new ObservableCollection<StorageItem>(_selectedStorageItems))
                    {
                        StorageItems.Remove(StorageItems.Single(i => i.UID == 
                                                                     storageItem.UID));
                    }
                    break;
                case 1:
                    foreach (ShoppingItem shoppingItem in new 
                        ObservableCollection<ShoppingItem>(_selectedShoppingItems))
                    {
                        ShoppingItems.Remove(ShoppingItems.Single(i => i.UID ==
                                                                       shoppingItem.UID));
                    }
                    break;
            }
        }

        private bool CanDeleteEntries(object commandParameter)
        {
            return _selectedTabIndex == 0 && _selectedStorageItems.Count > 0 ||
                   _selectedTabIndex == 1 && _selectedShoppingItems.Count > 0;
        }
        
        private void OnEditEntry(object commandParameter)
        {
            if (_selectedTabIndex == 0)
            {
                StorageItem selectedStorageItem = _selectedStorageItems.First();
                StorageItemInputDialog inputDialog = new(selectedStorageItem);

                if (inputDialog.ShowDialog() == true)
                {
                    inputDialog.NewItem.UID = selectedStorageItem.UID;
                    StorageItems.Remove(selectedStorageItem);
                    StorageItems.Add(inputDialog.NewItem);
                }
            }
            else
            {
                ShoppingItem selectedShoppingItem = _selectedShoppingItems.First();
                ShoppingItemInputDialog inputDialog = new(selectedShoppingItem);

                if (inputDialog.ShowDialog() == true)
                {
                    inputDialog.NewItem.UID = selectedShoppingItem.UID;
                    ShoppingItems.Remove(selectedShoppingItem);
                    ShoppingItems.Add(inputDialog.NewItem);
                }
            }
        }

        private bool CanEditEntry(object commandParameter)
        {
            return _selectedTabIndex == 0 && _selectedStorageItems.Count == 1 ||
                   _selectedTabIndex == 1 && _selectedShoppingItems.Count == 1;
        }

        public void OnWindowClosed()
        {
            _settings.User.SelectedTabIndex = _selectedTabIndex;
            _settings.SaveUserSettings();
            _storageItems.SaveToFile();
            _shoppingItems.SaveToFile();
        }
    }
}