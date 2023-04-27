using EasyConfig.Types;
using EasyConfig.Utils;

namespace EasyConfig.Serializers {
    public class XmlSerializer : ISerializer {
        public SerializeFormat Format => SerializeFormat.Xml;

        public void Save(Types.EasyConfig config) {
            Type type = config.GetType();
            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(type);

            using StreamWriter streamWriter = new StreamWriter(config.GetConfigPath());
            xmlSerializer.Serialize(streamWriter, config);
        }

        T ISerializer.Load<T>(string path) {
            var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using StreamReader streamReader = new StreamReader(path);
            return xmlSerializer.Deserialize(streamReader) as T;
        }
    }
}