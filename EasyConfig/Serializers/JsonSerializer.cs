using EasyConfig.Types;
using EasyConfig.Utils;
using Newtonsoft.Json;

namespace EasyConfig.Serializers {
    public class JsonSerializer : ISerializer {
        public SerializeFormat Format => SerializeFormat.Json;

        public void Save(Types.EasyConfig config) {
            System.IO.File.WriteAllText(config.GetConfigPath(), JsonConvert.SerializeObject(config));
        }

        T ISerializer.Load<T>(string path) {
            return JsonConvert.DeserializeObject<T>(System.IO.File.ReadAllText(path))!;
        }
    }
}