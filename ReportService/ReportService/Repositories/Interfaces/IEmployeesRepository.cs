using ReportService.Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportService.Repositories.Interfaces
{
    /// <summary>
    /// Employees repository
    /// </summary>
    public interface IEmployeesRepository
    {
        /// <summary>
        /// Reads all entities from employees
        /// </summary>
        /// <returns>Collection of employees</returns>
        Task<IEnumerable<Employee>> ReadAll();
    }
}
