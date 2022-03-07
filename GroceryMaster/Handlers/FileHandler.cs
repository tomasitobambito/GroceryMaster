using System;
using System.IO;
using System.Text.Json;

namespace GroceryMaster.Handlers
{
    public static class FileHandler
    {
        public static string GetAppDataFile(string fileName)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            path = Path.Combine(path, "GroceryMaster");
            
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            
            path = Path.Combine(path, fileName);

            return path;
        }

        /// <summary>
        /// Reads file, only call inside try catch block to ensure no FileNotFound Errors.
        /// </summary>
        /// <param name="path">Path of JSON file to read.</param>
        /// <typeparam name="T">Object to serialize to.</typeparam>
        /// <returns>Serialized object of type T.</returns>
        public static T ReadFromJSONFile<T>(string path)
        {
            var fileText = File.ReadAllText(path);
            if (fileText == "") fileText = "{}";

            return JsonSerializer.Deserialize<T>(fileText);
        }

        public static void WriteToFile(string path, object objectToSerialize)
        {
            var jsonString = JsonSerializer.Serialize(objectToSerialize);
            File.WriteAllText(path, jsonString);
        }
    }
}