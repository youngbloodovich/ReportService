using ReportService.Domain;
using System.Threading.Tasks;

namespace ReportService.Services.Interfaces
{
    /// <summary>
    /// Report builder service
    /// </summary>
    public interface IReportBuilderService
    {
        /// <summary>
        /// Builds company report.
        /// Gets data about employees and departments from the employee database.
        /// Requests salary data from the accounting service using employee code from the staff service.
        /// </summary>
        /// <param name="year">Year</param>
        /// <param name="month">Month number</param>
        /// <returns>Company report</returns>
        Task<CompanyReport> Build(int year, int month);
    }
}
