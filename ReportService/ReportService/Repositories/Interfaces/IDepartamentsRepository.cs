using ReportService.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportService.Repositories.Interfaces
{
    public interface IDepartamentsRepository
    {
        Task<IEnumerable<Departament>> ReadAll();
    }
}
