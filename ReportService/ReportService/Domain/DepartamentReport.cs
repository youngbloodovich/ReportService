using System.Linq;

namespace ReportService.Domain
{
    public class DepartamentReport
    {
        public string Name { get; set; }

        public EmployeeReport[] EmployeeReports { get; set; }

        public int Total => EmployeeReports.Sum(employee => employee.Salary);
    }
}
