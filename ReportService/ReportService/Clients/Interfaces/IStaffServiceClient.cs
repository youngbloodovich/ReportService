using System.Threading.Tasks;

namespace ReportService.Clients.Interfaces
{
    public interface IStaffServiceClient
    {
        Task<string> GetCode(string inn);
    }
}
