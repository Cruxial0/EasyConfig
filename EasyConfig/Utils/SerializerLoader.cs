using System;
using System.Collections.Generic;
using System.Linq;
using EasyConfig.Types;

namespace EasyConfig.Utils {
    public static class SerializerLoader {

        private static List<ISerializer> _serializers = new List<ISerializer>();

        public static ISerializer GetSerializerFromFormat(SerializeFormat format) =>
            _serializers.FirstOrDefault(s => s.Format == format);
        
        private static void ValidateSerializers() {
            foreach (SerializeFormat value in Enum.GetValues(typeof(SerializeFormat))) {
                if (_serializers.Count(s => s.Format == value) > 1) 
                    throw new FormatException($"More than one Serializer target the '{value}' format. Either add a new SerializerFormat, or remove one of the conflicting Serializers.");
            }
        }
        
        private static void LocateSerializers() {
            var type = typeof(ISerializer);
            var serializers = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));

            foreach (var serializer in serializers) {
                if(!serializer.IsClass) continue;
                _serializers.Add((ISerializer)Activator.CreateInstance(serializer));
            }
        }

        static SerializerLoader() {
            LocateSerializers();
            ValidateSerializers();
        }
    }
}