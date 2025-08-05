namespace MermaidExtractionEngine.Interfaces;

public interface IFileProvider
{
    public bool Exists(string path) => File.Exists(path);
}
