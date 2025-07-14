namespace MermaidExtractionEngine.Interfaces;

public interface IDirectoryProvider
{
    public string[] GetFiles(string path, string searchPattern) => Directory.GetFiles(path, searchPattern);
    public bool Exists( string path) => Directory.Exists( path );
}
