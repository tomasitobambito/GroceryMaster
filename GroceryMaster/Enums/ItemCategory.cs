// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
using System.ComponentModel;

namespace GroceryMaster.Enums
{
    public enum ItemCategory
    {
        [Description("Fruits")]
        FR,
        
        [Description("Vegetables")]
        VE,
        
        [Description("Canned Goods")]
        CG,
        
        [Description("Frozen Foods")]
        FF,
        
        [Description("Meat")]
        ME,
        
        [Description("Fish and Shellfish")]
        FS,
        
        [Description("Dairy")]
        DA,
        
        [Description("Deli")]
        DE,
        
        [Description("Condiments and Spices")]
        CS,
        
        [Description("Sauces and Oils")]
        SO,
        
        [Description("Snacks")]
        SN,
        
        [Description("Bread and Bakery")]
        BB,
        
        [Description("Beverages")]
        BE,
        
        [Description("Pasta and Rice")]
        PR,
        
        [Description("Cereal")]
        CE,
        
        [Description("Baking")]
        BA,
        
        [Description("Personal Care")]
        PC,
        
        [Description("Health Care")]
        HC,
        
        [Description("Paper and Wrap")]
        PW,
        
        [Description("Household Supplies")]
        HS,
        
        [Description("Baby Items")]
        BI,
        
        [Description("Other")]
        OT
    }
}