using MermaidExtractionEngine.Interfaces;
using System.Reflection;

namespace MermaidExtractionEngine.Providers;

public class AssemblyProvider : IAssemblyProvider
{
    private Assembly? _assembly;

    public string Name => _assembly?.ManifestModule?.Name ?? string.Empty;

    public void LoadFrom( string filePath )
    {
        try
        {
            _assembly = Assembly.LoadFrom( filePath );
        }
        catch 
        {
            _assembly = null;
        }
    }

    public Type[ ] GetTypes( )
    {
        return _assembly?.GetTypes( ) ?? [ ];
    }

    public bool IsValid => _assembly is not null && !string.IsNullOrWhiteSpace( _assembly.ManifestModule.Name );
}
