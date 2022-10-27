using ReportService.Clients.Interfaces;
using ReportService.Domain;
using ReportService.Repositories.Entities;
using ReportService.Repositories.Interfaces;
using ReportService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            _departamentsRepository = departamentsRepository ?? throw new ArgumentNullException(nameof(departamentsRepository));
            _employeesRepository = employeesRepository ?? throw new ArgumentNullException(nameof(employeesRepository));
            _staffServiceClient = staffServiceClient ?? throw new ArgumentNullException(nameof(staffServiceClient));
            _accountingServiceClient = accountingServiceClient ?? throw new ArgumentNullException(nameof(accountingServiceClient));
        }

        /// <inheritdoc/>
        public async Task<CompanyReport> Build(int year, int month)
        {
            var employees = await _employeesRepository.ReadAll();
            var departaments = await _departamentsRepository.ReadAll();

            var groups = GroupEmployeesByDepartmaents(employees, departaments);
            
            return new CompanyReport
            {
                Month = month, Year = year,
                DepartamentReports = await BuildDepartmentReports(groups)
            };
        }

        private Dictionary<string, Employee[]> GroupEmployeesByDepartmaents(
            IEnumerable<Employee> employees,
            IEnumerable<Departament> departaments)
        {
            return departaments
                .Where(departament => departament.IsActive)
                .GroupJoin(
                    employees,
                    dep => dep.DepartmentId,
                    emp => emp.DepartmentId,
                    (dep, emps) => new { dep.Name, emps })
                .ToDictionary(
                    group => group.Name, 
                    group => group.emps.ToArray()
                );
        }

        private async Task<DepartamentReport[]> BuildDepartmentReports(Dictionary<string, Employee[]> groups)
        {
            var result = new List<DepartamentReport>(groups.Count);

            foreach (var group in groups)
            {
                var departmentReport = new DepartamentReport
                {
                    Name = group.Key,
                    EmployeeReports = await BuildEmployeeReports(group.Value)
                };

                result.Add(departmentReport);
            }; 

            return result.ToArray();
        }

        private async Task<EmployeeReport[]> BuildEmployeeReports(Employee[] employees)
        {
            var result = new List<EmployeeReport>(employees.Length);

            foreach (var employee in employees)
            {
                var employeeReports = new EmployeeReport
                {
                    Name = employee.Name,
                    Salary = await GetSalary(employee)
                };

                result.Add(employeeReports);
            }

            return result.ToArray();
        }

        private async Task<int> GetSalary(Employee employee)
        {
            var code = await _staffServiceClient.GetCode(employee.Inn);

            return await _accountingServiceClient.GetSalary(code);
        }
    }
}
