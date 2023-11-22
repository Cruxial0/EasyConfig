using System;
using System.IO;
using System.Linq;
using EasyConfig.Types;

namespace EasyConfig.Utils {
    public static class ConfigUtility {
        // Get correct saving utility
        private static readonly ISerializer ActiveSerializer = SerializerLoader.GetSerializerFromFormat(ConfigSettings.Serializer);

        private static readonly MappingUtilities MappingUtilities = new MappingUtilities();
        
        /// <summary>
        /// Get path full path to config file.
        /// </summary>
        /// <param name="config">Config to retrieve path for. Does not check for file existence.</param>
        /// <returns>The assumed path of config file.</returns>
        public static string GetConfigPath(this Types.EasyConfig config) {
            return Path.Combine(GetOrCreateDirectory(ConfigSettings.RootFolder),
                config.ConfigName + GetFileExtension());
        }
        
        /// <summary>
        /// Passes data to the configured serializer.
        /// </summary>
        /// <param name="config">Config to serialize</param>
        public static void SaveConfig(this Types.EasyConfig config) {
            IoUtility.CheckOrCreateFileSafe(config.GetConfigPath());
            ActiveSerializer.Save(config);
        }
        
        /// <summary>
        /// Retrieves data from the configured serializer.
        /// </summary>
        /// <param name="path">Path to load data from</param>
        /// <typeparam name="T">Config type to deserialize</typeparam>
        /// <returns>Config of type T</returns>
        public static T LoadConfig<T>(string path) where T : Types.EasyConfig{
            IoUtility.CheckOrCreateFileSafe(path);
            return ActiveSerializer.Load<T>(path);
        }
        
        /// <summary>
        /// Transfers the config to the original object.
        /// </summary>
        /// <param name="source">Copy of object</param>
        /// <param name="dst">Main object</param>
        public static void ApplyLoad(Types.EasyConfig? source, Types.EasyConfig dst) {
            if(ConfigSettings.IncludeFields) MappingUtilities.MapAllFields(source, dst);
            MappingUtilities.MapAllProperties(source, dst);
        }
        
        public static string GetFileExtension(bool excludeDot = false) 
            => excludeDot ? string.Empty : "." + Enum.GetName(typeof(SerializeFormat), ConfigSettings.Serializer)?.ToLower();
        
        // Gets or creates a directory by given path.
        private static string GetOrCreateDirectory(string path) {
            // Check if path is a file in a platform friendly manner
            bool isFile = path.Contains("\\")
                ? path.Split("\\").Last().Contains(".")
                : path.Split("/").Last().Contains(".");
            
            var directoryName = isFile ? Path.GetDirectoryName(path) : path;
            if (Directory.Exists(directoryName)) return directoryName;

            if (directoryName == null)
                throw new IOException("Directory could not be located. Possible null reference value.");

            return Directory.CreateDirectory(directoryName).FullName;
        }
    }
}