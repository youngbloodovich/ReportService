using ReportService.Domain;
using ReportService.Helpers;
using ReportService.Repositories.Interfaces;
using ReportService.Services.Interfaces;
using System;
using System.Text;

namespace ReportService.Services
{
    public class ReportFormatterService : IReportFormatterService
    {
        private const string Separator = "---";

        private readonly StringBuilder _result = new StringBuilder();

        /// <inheritdoc/>
        public string Format(CompanyReport report)
        {
            var monthName = MonthNameResolver.GetName(report.Month);
            _result.AppendLine($"{monthName} {report.Year}");
            _result.AppendLine(Environment.NewLine);

            foreach (var departament in report.DepartamentReports)
            {
                _result.AppendLine(Separator);
                _result.AppendLine(departament.Name);
                _result.AppendLine(Environment.NewLine);

                foreach (var employee in departament.EmployeeReports)
                {
                    _result.AppendLine($"{employee.Name} {employee.Salary}р");
                    _result.AppendLine(Environment.NewLine);
                }

                _result.AppendLine($"Всего по отделу {departament.Total}р");
                _result.AppendLine(Environment.NewLine);
            }
            
            _result.AppendLine(Separator);
            _result.AppendLine(Environment.NewLine);
            _result.AppendLine($"Всего по предприятию {report.Total}р");

            return _result.ToString();
        }
    }
}
