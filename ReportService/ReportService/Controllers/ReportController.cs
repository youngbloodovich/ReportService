using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReportService.Services.Interfaces;

namespace ReportService.Controllers
{
    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        private readonly IReportBuilderService _reportBuilderService;
        private readonly IReportFormatterService _reportFormatterService;
        private readonly IFileCacheService _fileCacheService;

        public ReportController(
            IReportBuilderService reportBuilderService, 
            IReportFormatterService reportFormatterService,
            IFileCacheService fileCacheService)
        {

            _reportBuilderService = reportBuilderService ?? throw new ArgumentNullException(nameof(reportBuilderService));
            _reportFormatterService = reportFormatterService ?? throw new ArgumentNullException(nameof(reportFormatterService));
            _fileCacheService = fileCacheService ?? throw new ArgumentNullException(nameof(fileCacheService));
        }

        [HttpGet]
        [Route("{year}/{month}")]
        public async Task<IActionResult> DownloadAsync(int year, int month)
        {
            var report = await _reportBuilderService.Build(year, month);
            var formatted = _reportFormatterService.Format(report);

            var filename = $"{year}_{month}.txt";
            _fileCacheService.Write(filename, formatted);

            return File(Encoding.UTF8.GetBytes(formatted), "application/octet-stream", filename);
        }

        [HttpGet]
        [Route("{year}/{month}/cached")]
        public async Task<IActionResult> DownloadCachedAsync(int year, int month)
        {
            var filename = $"{year}_{month}.txt";
            if (!_fileCacheService.FileExists(filename))
            {
                return await DownloadAsync(year, month);
            }

            var cached = _fileCacheService.Read(filename);

            return File(Encoding.UTF8.GetBytes(cached), "application/octet-stream", filename);
        }
    }
}
