namespace MermaidExtractionEngine
{
    /// <summary>
    /// Given a path to a single file or a directory containing a .Net assembly
    /// it extracts and stores information about the asssembly structure
    /// </summary>
    public interface IExtractor
    {
        /// <summary>
        /// Any additional information about the extraction that needs to be
        /// brought to the attention of the end user.
        /// </summary>
        string Extract( bool isInputDirectory, string path );
        /// <summary>
        /// Extracts information about the assembly structure, given a path.
        /// </summary>
        /// <param name="isInputDirectory">True if the path points to a directory, false if it points to a single file</param>
        /// <param name="path">Path to the .Net assembly to be examined</param>
        /// <returns></returns>
        string Messages { get; }
    }
}
