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

        // write contents of self to file as xml
        public void Save(string fileName)
        {
            using StreamWriter sw = new (fileName);
            XmlSerializer xmls = new (typeof(UserSettings));
            xmls.Serialize(sw, this); // serialize to file
        }
        
        // read UserSettings object from file
        public static UserSettings Read(string fileName)
        {
            using StreamReader sw = new (fileName);
            XmlSerializer xmls = new (typeof(UserSettings));
            return xmls.Deserialize(sw) as UserSettings; // read from file
        }

        public static UserSettings GetDefault()
        {
            UserSettings settings = new()
            {
                // set default values for first program start
                SelectedTabIndex = 0,
                CurrentHighestIndex = 1
            };

            return settings;
        }
    }
}