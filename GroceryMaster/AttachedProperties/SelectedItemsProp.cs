using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace GroceryMaster.AttachedProperties
{
    public static class SelectedItemsProp
    {
        // create selected items DependencyProperty that contains behaviour
        private static readonly DependencyProperty SelectedItemsBehaviorProperty =
            DependencyProperty.RegisterAttached(
                "SelectedItemsBehavior",
                typeof(SelectedItemsBehavior),
                typeof(ListView),
                null);
        
        // create items DependencyProperty that contains Items list that the view will bind to
        public static readonly DependencyProperty ItemsProperty = DependencyProperty.RegisterAttached(
            "Items",
            typeof(IList),
            typeof(SelectedItemsProp),
            new PropertyMetadata(null, OnItemPropertyChanged));

        // create the setter that the view will bind to
        public static void SetItems(ListView listView, IList list) { listView.SetValue(ItemsProperty, list); }
        // create the getter that the view will bind to
        public static IList GetItems(ListView listView) { return listView.GetValue(ItemsProperty) as IList; }

        private static void OnItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ListView target = d as ListView; 
            if (target != null)
            {
                GetOrCreateBehavior(target, e.NewValue as IList); // update behaviour when ListView changes
            }
        }

        // method to retrieve updated SelectedItemsBehaviour or create new one on initial start
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