using System.IO;
using System.Xml.Serialization;

namespace GroceryMaster.Logic
{
    public class UserSettings
    {
        public int SelectedTabIndex { get; set; }
        public int CurrentHighestIndex { get; set; }
        public int StorageSortIndex { get; set; }
        public int ShoppingSortIndex { get; set; }

        public void Save(string fileName)
        {
            using StreamWriter sw = new (fileName);
            XmlSerializer xmls = new (typeof(UserSettings));
            xmls.Serialize(sw, this);
        }

        public static UserSettings Read(string fileName)
        {
            using StreamReader sw = new (fileName);
            XmlSerializer xmls = new (typeof(UserSettings));
            return xmls.Deserialize(sw) as UserSettings;
        }

        public static UserSettings GetDefault()
        {
            UserSettings settings = new()
            {
                SelectedTabIndex = 0,
                CurrentHighestIndex = 1
            };

            return settings;
        }
    }
}