using GroceryMaster.Enums;

namespace GroceryMaster.Model
{
    public class ShoppingItem
    {
        public string Description { get; set; }
        public string Note { get; set; }
        public ItemCategory Category { get; set; }
    }
}