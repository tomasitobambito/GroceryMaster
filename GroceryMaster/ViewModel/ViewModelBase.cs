using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GroceryMaster.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged; // create event handler that can invoke PropertyChanged

        protected void SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, newValue)) // check if new value is different from previous
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); // update view
            }
        }
    }
}