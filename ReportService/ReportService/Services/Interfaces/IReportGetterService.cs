using System.Threading.Tasks;

namespace ReportService.Services.Interfaces
{
    public interface IReportGetterService
    {
        Task<string> GetReportAsync(int year, int month);
    }
}
