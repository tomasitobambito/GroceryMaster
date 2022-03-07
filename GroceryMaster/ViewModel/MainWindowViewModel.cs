﻿using System.Collections.ObjectModel;
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

        private ObservableCollection<StorageItem> _storageItems;
        public ObservableCollection<StorageItem> StorageItems
        {
            get => _storageItems;
            set => SetProperty(ref _storageItems, value);
        }

        private ObservableCollection<ShoppingItem> _shoppingItems;
        public ObservableCollection<ShoppingItem> ShoppingItems
        {
            get => _shoppingItems;
            set => SetProperty(ref _shoppingItems, value);
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
            _newEntryCommand = new CommandHandler(OnNewEntry, CanNewEntry);
        }

        public void OnNewEntry(object commandParameter)
        {
            if (_selectedTabIndex == 0)
            {
                StorageItemInputDialog inputDialog = new();
                if (inputDialog.ShowDialog() == true)
                {
                    StorageItems.Add(inputDialog.NewItem);
                    StorageItems.SaveToFile();
                }
            }
            else
            {
                ShoppingItemInputDialog inputDialog = new();
                if (inputDialog.ShowDialog() == true)
                {
                    ShoppingItems.Add(inputDialog.NewItem);
                    ShoppingItems.SaveToFile();
                }
            }
            _newEntryCommand.InvokeCanExecuteChanged();
        }

        public bool CanNewEntry(object commandParameter)
        {
            return true;
        }
    }
}