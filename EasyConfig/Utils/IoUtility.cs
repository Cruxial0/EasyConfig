namespace EasyConfig.Utils; 

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
}