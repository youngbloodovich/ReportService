using ReportService.Domain;

namespace ReportService.Services.Interfaces
{
    public interface IReportFormatterService
    {
        string Format(CompanyReport report);
    }
}
