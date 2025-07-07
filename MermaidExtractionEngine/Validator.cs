namespace MermaidExtractionEngine
{
    public class Validator: IValidator
    {
        public (bool, string) ValidateDirectoryPath( string path )
        {
            if (string.IsNullOrWhiteSpace( path ))
            {
                return (false, "Please provide input directory!");
            }

            var di = new DirectoryInfo( path );
            if (di is null || di.Parent is null || !di.Parent.Exists)
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

            if (strict && !File.Exists( path )) 
            {
                return (false, "Please provide a valid path to a .Net executable file");
            }

            if (!strict && !File.Exists( path ))
            {
                var di = new DirectoryInfo( path );

                if (di is null || di.Parent is null || !di.Parent.Exists)
                {
                    return (false, "Please provide a path containing a valid directory!");
                }
            }

            return (true, string.Empty);
        }
    }
}
