namespace MermaidExtractionEngine.Interfaces;

public interface IAssemblyProvider
{
    public void LoadFrom( string filePath );
    public string Name { get; }
    public bool IsValid { get; }
    public Type[] GetTypes();
}
