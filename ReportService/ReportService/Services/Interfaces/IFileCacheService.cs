namespace ReportService.Services.Interfaces
{
    public interface IFileCacheService
    {
        bool FileExists(string filename);

        string Read(string filename);

        void Write(string filename, string content);
    }
}
