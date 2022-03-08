using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using GroceryMaster.Dialogs;
using GroceryMaster.Extensions;
using GroceryMaster.Handlers;
using GroceryMaster.Model;

namespace GroceryMaster.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
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
        }

        private void OnNewEntry(object commandParameter)
        {
            AddEntry(true);
            _storageItems.SaveToFile();
            _shoppingItems.SaveToFile();
        }

        private bool CanNewEntry(object commandParameter)
        {
            return true;
        }
        
        private void OnDeleteEntries(object commandParameter)
        {
            DeleteEntries();
            _storageItems.SaveToFile();
            _shoppingItems.SaveToFile();
        }

        private bool CanDeleteEntries(object commandParameter)
        {
            return _selectedTabIndex == 0 && _selectedStorageItems.Count > 0 ||
                   _selectedTabIndex == 1 && _selectedShoppingItems.Count > 0;
        }
        
        private void OnEditEntry(object commandParameter)
        {
            if (AddEntry(false))
                DeleteEntries();
        }

        private bool CanEditEntry(object commandParameter)
        {
            return _selectedTabIndex == 0 && _selectedStorageItems.Count == 1 ||
                   _selectedTabIndex == 1 && _selectedShoppingItems.Count == 1;
        }

        private bool AddEntry(bool AddingNew)
        {
            bool? result;
            if (_selectedTabIndex == 0)
            {
                StorageItemInputDialog inputDialog = AddingNew ? 
                        new StorageItemInputDialog("Add Item", "Add Storage Item") : 
                        new StorageItemInputDialog("Edit Item", "Edit Storage Item");
                result = inputDialog.ShowDialog();
                if (result == true)
                {
                    StorageItems.Add(inputDialog.NewItem);
                }
            }
            else
            {
                ShoppingItemInputDialog inputDialog = AddingNew ? 
                    new ShoppingItemInputDialog("Add Item", "Add Shopping Item") : 
                    new ShoppingItemInputDialog("Edit Item", "Edit Shopping Item");
                result = inputDialog.ShowDialog();
                if (result == true)
                {
                    ShoppingItems.Add(inputDialog.NewItem);
                }
            }

            return result == true;
        }

        private void DeleteEntries()
        {
            switch (_selectedTabIndex)
            {
                case 0:
                    foreach (StorageItem storageItem in new ObservableCollection<StorageItem>(_selectedStorageItems))
                    {
                        StorageItems.Remove(StorageItems.Single(i => i.Description == 
                                                                     storageItem.Description));
                    }
                    break;
                case 1:
                    foreach (ShoppingItem shoppingItem in new 
                        ObservableCollection<ShoppingItem>(_selectedShoppingItems))
                    {
                        ShoppingItems.Remove(ShoppingItems.Single(i => i.Description ==
                                                                       shoppingItem.Description));
                    }
                    break;
            }
        }
    }
}