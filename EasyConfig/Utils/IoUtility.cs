using System.IO;
using System.Linq;

namespace EasyConfig.Utils
{
    public static class IoUtility {

        /// <summary>
        /// Checks or creates a file safely.
        /// </summary>
        /// <param name="path"></param>
        public static void CheckOrCreateFileSafe(string path) {
            bool isFile = path.Contains("\\")
                ? path.Split("\\").Last().Contains(".")
                : path.Split("/").Last().Contains(".");
            
            if(!isFile) throw new IOException("Given path is not a file.");
        
            if(!File.Exists(path)) CreateFileSafe(path);
        }
    
        /// <summary>
        /// Creates a file safely, while avoiding sharing path violations.
        /// </summary>
        /// <param name="path">File to create.</param>
        private static void CreateFileSafe(string path)
        {
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(string.Empty);
                sw.Flush();
                sw.Dispose();
            }
        }
        
        /// <summary>
        /// Tries casting the object to a given type.
        /// </summary>
        /// <param name="obj">Object to cast</param>
        /// <param name="result">Successfully casted object</param>
        /// <typeparam name="T">Type to cast into</typeparam>
        /// <returns>Operation success</returns>
        public static bool TryCast<T>(this object? obj, out T result) {
            if (obj == null) {
                result = default(T);
                return false;
            }
            
            if (obj is T)
            {
                result = (T)obj;
                return true;
            }

            result = default(T);
            return false;
        }
    }
}