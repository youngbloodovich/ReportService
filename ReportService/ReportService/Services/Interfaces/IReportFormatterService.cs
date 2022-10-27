using ReportService.Domain;

namespace ReportService.Services.Interfaces
{
    /// <summary>
    /// Report formatter service
    /// </summary>
    public interface IReportFormatterService
    {
        /// <summary>
        /// Make and format a text representation of report.
        /// </summary>
        /// <param name="report">Company report</param>
        /// <returns>Text representation of report</returns>
        string Format(CompanyReport report);
    }
}
