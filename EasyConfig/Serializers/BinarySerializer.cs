using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using EasyConfig.Types;
using EasyConfig.Utils;

namespace EasyConfig.Serializers {
    public class JsonSerializer : ISerializer {
        public SerializeFormat Format => SerializeFormat.Binary;
        private BinaryFormatter _formatter = new BinaryFormatter();
        public void Save(Types.EasyConfig config) 
        {
            using (FileStream _stream = new FileStream(config.GetConfigPath()), FileMode.Create)
            {
               _formatter.Serialize(_stream, config);
            }
        }
        T ISerializer.Load<T>(string path) 
        {
            using (FileStream _stream = new FileStream(path, FileMode.Open))
            {
                return  _formatter.Deserialize(_stream);
            }
        }
    }
}