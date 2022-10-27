namespace ReportService.Services.Interfaces
{
    /// <summary>
    /// File cache service.
    /// Storage takes place on a file disk
    /// </summary>
    public interface IFileCacheService
    {
        /// <summary>
        /// Checks that specified file exists
        /// </summary>
        /// <param name="filename">Filename</param>
        /// <returns>if exists true, otherwise false</returns>
        bool FileExists(string filename);

        /// <summary>
        /// Reads file from cache
        /// </summary>
        /// <param name="filename">Filename</param>
        /// <returns>Text content of file</returns>
        string Read(string filename);

        /// <summary>
        /// Writes file to cache.
        /// If target file already exists, it is overwritten
        /// </summary>
        /// <param name="filename">Filename</param>
        /// <param name="content">Text content</param>
        void Write(string filename, string content);
    }
}
