using System.Collections;
using System.Windows.Controls;

namespace GroceryMaster.AttachedProperties
{
    public class SelectedItemsBehavior
    {
        private readonly ListView _listView;
        private readonly IList _boundList;

        public SelectedItemsBehavior(ListView listView, IList boundList)
        {
            _boundList = boundList;
            _listView = listView;
            _listView.SelectionChanged += OnSelectionChanged;
        }

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