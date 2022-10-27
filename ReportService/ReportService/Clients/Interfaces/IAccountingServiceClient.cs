using System.Threading.Tasks;

namespace ReportService.Clients.Interfaces
{
    public interface IAccountingServiceClient
    {
        Task<int> GetSalary(string code);
    }
}
