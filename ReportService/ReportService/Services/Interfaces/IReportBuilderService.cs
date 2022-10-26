using ReportService.Domain;
using System.Threading.Tasks;

namespace ReportService.Services.Interfaces
{
    public interface IReportBuilderService
    {
        Task<CompanyReport> Build(int year, int month);
    }
}
