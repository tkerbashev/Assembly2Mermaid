using MermaidExtractionEngine.Interfaces;

namespace MermaidExtractionTests.Providers;

internal class FakeFileProvider: IFileProvider
{
    public bool Exists( string path ) => path.StartsWith( "valid path" );
}
