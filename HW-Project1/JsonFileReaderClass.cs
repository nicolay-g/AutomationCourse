using System;
using System.IO;
using Newtonsoft.Json.Linq;

/// <summary>
    /// Static class that manages the json files in the Directory.
    /// </summary>
    internal static class JsonDataFileReader
    {
        /// <summary>
        /// Directory name.
        /// </summary>
        public static string DirectoryName { get; set; } = "JsonData";

        /// <summary>
        /// Gets A JSON Object from a file.
        /// </summary>
        /// <param name="fileName">The name of the file (with extension).</param>
        /// <returns><see cref="JObject"/> instance.</returns>
        public static JObject GetJObject(string fileName) => JObject.Parse(GetFileTextContent(fileName));

        /// <summary>
        /// Gets A JSON Array from a file.
        /// </summary>
        /// <param name="fileName">The name of the file (with extension).</param>
        /// <returns><see cref="JArray"/> instance.</returns>
        public static JArray GetJArray(string fileName) => JArray.Parse(GetFileTextContent(fileName));

        private static string GetFileTextContent(string fileName)
        {
            string directoryPath = GetDirectoryPath(DirectoryName);
            string filePath = $"{directoryPath}\\{fileName}";
            string textContent = File.ReadAllText(filePath);
            return textContent;
        }
        private static string GetDirectoryPath(string directoryName)
        {
            string relativePath = $"\\{directoryName}";
            string baseDirPath = AppDomain.CurrentDomain.BaseDirectory;
            baseDirPath = baseDirPath.Replace("\\bin\\Debug\\net5.0\\", "");
            string absolutePath = baseDirPath + relativePath;
            return absolutePath;
        }
    }