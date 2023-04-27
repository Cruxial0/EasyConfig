using System.Xml.Serialization;
using Newtonsoft.Json;

namespace EasyConfig.Types {
    public interface IBaseConfig {
        [JsonIgnore] [XmlIgnore] public string ConfigName { get; }
        
    }
}