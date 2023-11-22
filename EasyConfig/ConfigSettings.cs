using System;
using System.IO;
using EasyConfig.Types;

namespace EasyConfig {
    public static class ConfigSettings {
        
        // CONFIG SETTINGS, THESE CAN BE EDITED
        // (replace 'Environment.CurrentDirectory' with 'Application.persistentDataPath' for Unity)
        // Or the other way around for testing
        public static readonly string RootFolder = Path.Combine(UnityEngine.Application.persistentDataPath, "Config");
        public static readonly bool IncludeFields = false;
        public const SerializeFormat Serializer = SerializeFormat.Xml;
    }
}