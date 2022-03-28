using System;
using System.IO;

namespace GroceryMaster.Logic
{
    public class SettingsLogic
    {
        public UserSettings User { get; private set; }
        public const string UserSettingsFileName = "usersettings.xml"; // set file name
        
        // get appdata path
        private string _settingsPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GroceryMaster", "Settings");

        private string _userSettingsPath;

        public SettingsLogic()
        {
            _userSettingsPath = _settingsPath + "\\" + UserSettingsFileName; // get user settings file path
            
            if (!Directory.Exists(_settingsPath)) Directory.CreateDirectory(_settingsPath);

            // read file if it exists, if not read default values
            User = File.Exists(_userSettingsPath) ? UserSettings.Read(_userSettingsPath) : UserSettings.GetDefault();
        }

        public void SaveUserSettings() // save to file
        {
            User.Save(_userSettingsPath);
        }
    }
}