using Microsoft.Extensions.Configuration;
using ReportService.Services.Interfaces;
using System;
using System.IO;

namespace ReportService.Services
{
    public class FileCacheService : IFileCacheService
    {
        private readonly string _fileCachePath;

        public FileCacheService(IConfiguration configuration)
        {
            _fileCachePath = configuration.GetSection("FileCachePath").Value;
        }

        public bool FileExists(string filename)
        {
            return File.Exists($"{_fileCachePath}\\{filename}");
        }

        public string Read(string filename)
        {
            if (!FileExists(filename))
            {
                throw new FileNotFoundException($"Specified file does not exists: {filename}");
            }

            return File.ReadAllText($"{_fileCachePath}\\{filename}");
        }

        public void Write(string filename, string content)
        {
            var fullFilePath = $"{_fileCachePath}\\{filename}";

            File.WriteAllText(fullFilePath, content);
        }
    }
}
