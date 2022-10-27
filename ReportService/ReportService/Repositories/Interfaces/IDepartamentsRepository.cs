using ReportService.Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportService.Repositories.Interfaces
{
    /// <summary>
    /// Departaments repository
    /// </summary>
    public interface IDepartamentsRepository
    {
        /// <summary>
        /// Reads all entities from departments
        /// </summary>
        /// <returns>Collection of departments</returns>
        Task<IEnumerable<Departament>> ReadAll();
    }
}
