using System.Threading.Tasks;

namespace ReportService.Clients.Interfaces
{
    /// <summary>
    /// Accounting service client
    /// </summary>
    public interface IAccountingServiceClient
    {
        /// <summary>
        /// Requests salary by employee code
        /// </summary>
        /// <param name="code">Employee code</param>
        /// <returns>Employee salary</returns>
        Task<int> GetSalary(string code);
    }
}
