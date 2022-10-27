using System.Threading.Tasks;

namespace ReportService.Clients.Interfaces
{
    /// <summary>
    /// Staff service client
    /// </summary>
    public interface IStaffServiceClient
    {
        /// <summary>
        /// Requests empoyee code by inn
        /// </summary>
        /// <param name="inn">Inn</param>
        /// <returns>Empoyee code</returns>
        Task<string> GetCode(string inn);
    }
}
