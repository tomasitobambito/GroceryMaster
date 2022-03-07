using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace GroceryMaster.Behaviors
{
    public static class SelectedItems
    {
        private static readonly DependencyProperty SelectedItemsBehaviorProperty =
            DependencyProperty.RegisterAttached(
                "SelectedItemsBehavior",
                typeof(SelectedItemsBehavior),
                typeof(ListView),
                null);
        
        public static readonly DependencyProperty ItemsProperty = DependencyProperty.RegisterAttached(
            "Items",
            typeof(IList),
            typeof(SelectedItems),
            new PropertyMetadata(null, OnItemPropertyChanged));

            public static void SetItems(ListView listView, IList list) { listView.SetValue(ItemsProperty, list); }
            public static IList GetItems(ListView listView) { return listView.GetValue(ItemsProperty) as IList; }

            private static void OnItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ListView target = d as ListView;
                if (target != null)
                {
                    GetOrCreateBehavior(target, e.NewValue as IList);
                }
            }

            private static SelectedItemsBehavior GetOrCreateBehavior(ListView target, IList list)
            {
                var behavior = target.GetValue(SelectedItemsBehaviorProperty) as SelectedItemsBehavior;
                if (behavior == null)
                {
                    behavior = new SelectedItemsBehavior(target, list);
                    target.SetValue(SelectedItemsBehaviorProperty, behavior);
                }

                return behavior;
            }
    }
}

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