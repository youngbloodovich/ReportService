using ReportService.Domain;
using ReportService.Helpers;
using ReportService.Repositories.Entities;
using ReportService.Services.Interfaces;
using System;
using System.Linq;
using System.Text;

namespace ReportService.Services
{
    public class ReportFormatterService : IReportFormatterService
    {
        private const string Separator = "---";
        private const int EmployeeNameLength = 50;

        private readonly StringBuilder _result = new StringBuilder();

        /// <inheritdoc/>
        public string Format(CompanyReport report)
        {
            if (!report.DepartamentReports.Any())
            {
                return "В компании нет департаментов!";
            }

            var monthName = MonthNameResolver.GetName(report.Month);
            _result.AppendLine($"{monthName} {report.Year}");
            _result.AppendLine(Environment.NewLine);
            _result.AppendLine(Separator);
            
            foreach (var departament in report.DepartamentReports)
            {
                _result.AppendLine(departament.Name);
                _result.AppendLine(Environment.NewLine);

                if (!departament.EmployeeReports.Any())
                {
                    _result.AppendLine("В департаменте нет сотрудников!");
                    _result.AppendLine(Environment.NewLine);
                    _result.AppendLine(Separator);
                    continue;
                }

                foreach (var employee in departament.EmployeeReports)
                {
                    _result.AppendLine($"{employee.Name, -EmployeeNameLength}{employee.Salary}р");
                    _result.AppendLine(Environment.NewLine);
                }

                _result.AppendLine($"Всего по отделу {departament.Total}р");
                _result.AppendLine(Environment.NewLine);
                _result.AppendLine(Separator);
            }

            _result.AppendLine(Environment.NewLine);
            _result.AppendLine($"Всего по предприятию {report.Total}р");

            return _result.ToString();
        }
    }
}
