using MermaidExtractionEngine.Interfaces;

namespace MermaidExtractionEngine;

public class Validator( IFileProvider fileProvider, IDirectoryProvider directoryProvider ) : IValidator
{
    private readonly IFileProvider _fileProvider = fileProvider;
    private readonly IDirectoryProvider _directoryProvider = directoryProvider;

    public (bool, string) ValidateDirectoryPath( string path )
    {
        if (string.IsNullOrWhiteSpace( path ))
        {
            return (false, "Please provide input directory!");
        }

        if (!_directoryProvider.Exists( path ))
        {
            return (false, "Please provide a valid input directory!");
        }

        return (true, string.Empty);
    }

    public (bool, string) ValidateFilePath( string path, bool strict = true )
    {
        if (string.IsNullOrWhiteSpace( path ))
        {
            return (false, "Please provide a file name!");
        }

        if (strict && !_fileProvider.Exists( path )) 
        {
            return (false, "Please provide a valid path to a .Net executable file!");
        }

        if (!strict && !_fileProvider.Exists( path ))
        {
            if (!_directoryProvider.Exists( path ))
            {
                return (false, "Please provide a path containing a valid directory!");
            }
        }

        return (true, string.Empty);
    }
}
