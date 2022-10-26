using ReportService.Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportService.Repositories.Interfaces
{
    public interface IDepartamentsRepository
    {
        Task<IEnumerable<Departament>> ReadAll();
    }
}
