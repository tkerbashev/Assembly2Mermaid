using MermaidExtractionEngine.Interfaces;

namespace MermaidExtractionTests.Providers;

internal class FakeDirectoryProvider: IDirectoryProvider
{
    public string[ ] GetFiles( string path, string searchPattern )
    {
        if ( searchPattern.EndsWith( ".exe" ) )
        {
            return [ "file1.exe" ];
        }

        return [ "file1.dll" ];
    }
    public bool Exists( string path ) => path.StartsWith( "valid path" );
}
