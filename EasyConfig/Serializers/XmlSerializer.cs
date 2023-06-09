﻿using System.IO;
using EasyConfig.Types;
using EasyConfig.Utils;

namespace EasyConfig.Serializers {
    public class XmlSerializer : ISerializer {
        public SerializeFormat Format => SerializeFormat.Xml;

        public void Save(Types.EasyConfig config) {
            System.Type type = config.GetType();
            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(type);

            using StreamWriter streamWriter = new StreamWriter(config.GetConfigPath());
            xmlSerializer.Serialize(streamWriter, config);
        }

        T ISerializer.Load<T>(string path) {
            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using StreamReader streamReader = new StreamReader(path);
            // '10': Arbitrary number to check for root node
            if (streamReader.ReadToEnd().Length < 10) return null!;

            streamReader.BaseStream.Position = 0;
            var output = xmlSerializer.Deserialize(streamReader);
            return output.TryCast<T>(out T config) ? config : null;
        }
    }
}