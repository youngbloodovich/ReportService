using ReportService.Clients.Interfaces;
using ReportService.Domain;
using ReportService.Repositories.Entities;
using ReportService.Repositories.Interfaces;
using ReportService.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ReportService.Services
{
    public class ReportBuilderService : IReportBuilderService
    {
        private readonly IDepartamentsRepository _departamentsRepository;
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IStaffServiceClient _staffServiceClient;
        private readonly IAccountingServiceClient _accountingServiceClient;

        public ReportBuilderService(
            IDepartamentsRepository departamentsRepository, 
            IEmployeesRepository employeesRepository,
            IStaffServiceClient staffServiceClient,
            IAccountingServiceClient accountingServiceClient)
        {
            _departamentsRepository = departamentsRepository ?? 
                throw new ArgumentNullException(nameof(departamentsRepository));
            _employeesRepository = employeesRepository ?? 
                throw new ArgumentNullException(nameof(employeesRepository));
            _staffServiceClient = staffServiceClient ?? 
                throw new ArgumentNullException(nameof(staffServiceClient));
            _accountingServiceClient = accountingServiceClient ?? 
                throw new ArgumentNullException(nameof(accountingServiceClient));
        }

        public async Task<CompanyReport> Build(int year, int month)
        {
            var departaments = await _departamentsRepository.ReadAll();
            var employees = await _employeesRepository.ReadAll();

            var groups = departaments
                .Where(departament => departament.IsActive)
                .GroupJoin(
                    employees,
                    dep => dep.DepartmentId,
                    emp => emp.DepartmentId,
                    (dep, emps) => new
                    {
                        DepartamentName = dep.Name,
                        Employees = emps.ToArray()
                    })
                .ToArray();

            var departamentReports = new DepartamentReport[groups.Length];
            for (var i = 0; i < groups.Length; ++i)
            {
                var group = groups[i];
                var employeeReports = new EmployeeReport[group.Employees.Length];
                for (var j = 0; j < group.Employees.Length; ++j)
                {
                    var employee = group.Employees[j];
                    employeeReports[j] = new EmployeeReport
                    {
                        Name = employee.Name,
                        Salary = await GetSalary(employee, year, month)
                    };
                }

                var departamentReport = new DepartamentReport
                {
                    Name = group.DepartamentName,
                    EmployeeReports = employeeReports
                };
            }
            
            return new CompanyReport
            {
                Month = month,
                Year = year,
                DepartamentReports = departamentReports
            };
        }

        private async Task<int> GetSalary(Employee employee, int year, int month)
        {
            var code = await _staffServiceClient.GetCode(employee.Inn);

            return await _accountingServiceClient.GetSalary(code, year, month);
        }
    }
}
