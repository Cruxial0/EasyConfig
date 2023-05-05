using System;
using System.IO;
using EasyConfig.Types;

namespace EasyConfig {
    public static class ConfigSettings {
        
        // CONFIG SETTINGS, THESE CAN BE EDITED
        // (replace 'Environment.CurrentDirectory' with 'Application.persistentDataPath' for Unity)
        public static readonly string RootFolder = Path.Combine(Environment.CurrentDirectory, "Config");
        public static readonly bool IncludeFields = false;
        public const SerializeFormat Serializer = SerializeFormat.Xml;
    }
}