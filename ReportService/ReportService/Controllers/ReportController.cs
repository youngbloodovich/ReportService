using System;
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

        public ReportController(
            IReportBuilderService reportBuilderService, 
            IReportFormatterService reportFormatterService)
        {

            _reportBuilderService = reportBuilderService ?? throw new ArgumentNullException(nameof(reportBuilderService));
            _reportFormatterService = reportFormatterService ?? throw new ArgumentNullException(nameof(reportFormatterService));
        }

        [HttpGet]
        [Route("{year}/{month}")]
        public async Task<IActionResult> DownloadAsync(int year, int month)
        {
            var report = await _reportBuilderService.Build(year, month);
            var formatted = _reportFormatterService.Format(report);

            return File(
                System.Text.Encoding.UTF8.GetBytes(formatted), 
                "application/octet-stream", 
                $"{year}_{month}.txt");
        }
    }
}
