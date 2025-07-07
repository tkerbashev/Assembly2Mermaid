namespace MermaidExtractionEngine
{
    /// <summary>
    /// Collection of helper methods for path validation.
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// Checks if a valid path to a directory is provided.
        /// </summary>
        /// <param name="path">Path to the directory</param>
        /// <returns>True for a valid path, false otherwise; Details if the path is invalid</returns>
        (bool, string) ValidateDirectoryPath( string path );

        /// <summary>
        /// Checks if a valid path to a file is provided
        /// </summary>
        /// <param name="path">Path to the file</param>
        /// <param name="strict">When true the file must exist, when false at least the containing directory must exist</param>
        /// <returns>True for a valid path, false otherwise; Details if the path is invalid</returns>
        (bool, string) ValidateFilePath( string path, bool strict = true );
    }
}