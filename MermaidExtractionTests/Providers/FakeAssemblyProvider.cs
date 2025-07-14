using MermaidExtractionEngine.Interfaces;

namespace MermaidExtractionTests.Providers;

internal class FakeAssemblyProvider : IAssemblyProvider
{
    private string _filePath = string.Empty;
    public string Name => "fake assembly name";

    public void LoadFrom( string filePath )
    {
        _filePath = filePath;
    }

    public Type[ ] GetTypes( ) => [ typeof( string ) ];

    public bool IsValid => _filePath.EndsWith("dll");
}
