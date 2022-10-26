using ReportService.Domain;
using ReportService.Repositories.Interfaces;
using ReportService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportService.Services
{
    public class ReportGetterService : IReportGetterService
    {
        private readonly IDepartamentsRepository _departamentsRepository;
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IReportBuilderService _reportBuilderService;

        public ReportGetterService(
            IDepartamentsRepository departamentsRepository, 
            IEmployeesRepository employeesRepository,
            IReportBuilderService reportBuilderService)
        {
            _departamentsRepository = departamentsRepository ?? throw new ArgumentNullException(nameof(departamentsRepository));
            _employeesRepository = employeesRepository ?? throw new ArgumentNullException(nameof(employeesRepository));
            _reportBuilderService = reportBuilderService ?? throw new ArgumentNullException(nameof(reportBuilderService));
        }

        public async Task<string> GetReportAsync(int year, int month)
        {
            var departaments = await _departamentsRepository.ReadAll();
            var employees = await _employeesRepository.ReadAll();

            throw new System.NotImplementedException();
        }
    }
}
