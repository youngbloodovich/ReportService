using ReportService.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportService.Repositories.Interfaces
{
    public interface IEmployeesRepository
    {
        Task<IEnumerable<Employee>> ReadAll();
    }
}
