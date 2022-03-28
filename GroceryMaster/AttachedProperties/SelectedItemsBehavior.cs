using System.Collections;
using System.Windows.Controls;

namespace GroceryMaster.AttachedProperties
{
    public class SelectedItemsBehavior
    {
        private readonly ListView _listView;
        private readonly IList _boundList;

        // constructor receives ListView object and IList that are copied into private fields
        public SelectedItemsBehavior(ListView listView, IList boundList)
        {
            _boundList = boundList;
            _listView = listView;
            _listView.SelectionChanged += OnSelectionChanged; // subscribe to OnSelectionChanged event of ListView
        }

        // Method to update bound list whenever ListView changes (i.e. selection changes)
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _boundList.Clear();

            foreach (var item in _listView.SelectedItems)
            {
                _boundList.Add(item);
            }
        }
    }
}