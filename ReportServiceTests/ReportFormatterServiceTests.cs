using NUnit.Framework;
using ReportService.Domain;
using ReportService.Repositories.Entities;
using ReportService.Services;
using System;
using System.Text;

namespace ReportServiceTests
{
    public class ReportFormatterServiceTests
    {
        private const string Separator = "---";
        private const int EmployeeNameLength = 50;

        [Test]
        public void Format_GivenCorrectCompanyReport_WorksOk()
        {
            var employee1 = new EmployeeReport { Name = "employee1", Salary = 70000 };
            var employee2 = new EmployeeReport { Name = "employee2", Salary = 35000 };
            var employee3 = new EmployeeReport { Name = "employee3", Salary = 45000 };

            var departament1 = new DepartamentReport
            {
                Name = "departament1",
                EmployeeReports = new EmployeeReport[] { employee1 }
            };
            var departament2 = new DepartamentReport
            {
                Name = "departament2",
                EmployeeReports = new EmployeeReport[] { employee2, employee3 }
            };

            var report = new CompanyReport
            {
                Year = 2022, Month = 2,
                DepartamentReports = new DepartamentReport[] { departament1, departament2 }
            };

            var expected = new StringBuilder();
            expected.AppendLine("������� 2022");
            expected.AppendLine(Environment.NewLine);
            expected.AppendLine(Separator);

            expected.AppendLine(departament1.Name);
            expected.AppendLine(Environment.NewLine);
            expected.AppendLine($"{employee1.Name, -EmployeeNameLength}{employee1.Salary}�");
            expected.AppendLine(Environment.NewLine);
            expected.AppendLine($"����� �� ������ 70000�");
            expected.AppendLine(Environment.NewLine);
            expected.AppendLine(Separator);

            expected.AppendLine(departament2.Name);
            expected.AppendLine(Environment.NewLine);
            expected.AppendLine($"{employee2.Name, -EmployeeNameLength}{employee2.Salary}�");
            expected.AppendLine(Environment.NewLine);
            expected.AppendLine($"{employee3.Name, -EmployeeNameLength}{employee3.Salary}�");
            expected.AppendLine(Environment.NewLine);
            expected.AppendLine($"����� �� ������ 80000�");
            expected.AppendLine(Environment.NewLine);
            expected.AppendLine(Separator);

            expected.AppendLine(Environment.NewLine);
            expected.AppendLine($"����� �� ����������� 150000�");

            var service = new ReportFormatterService();
            var actual = service.Format(report);

            Assert.AreEqual(expected.ToString(), actual);
        }

        [Test]
        public void Format_GivenDeparmentWithoutEmployees_DisplaysCorrepondingMessage()
        {
            var employee1 = new EmployeeReport { Name = "employee1", Salary = 70000 };

            var departament1 = new DepartamentReport
            {
                Name = "departament1",
                EmployeeReports = new EmployeeReport[] { }
            };
            var departament2 = new DepartamentReport
            {
                Name = "departament2",
                EmployeeReports = new EmployeeReport[] { employee1 }
            };

            var report = new CompanyReport
            {
                Year = 2022, Month = 2,
                DepartamentReports = new DepartamentReport[] { departament1, departament2 }
            };

            var expected = new StringBuilder();
            expected.AppendLine("������� 2022");
            expected.AppendLine(Environment.NewLine);
            expected.AppendLine(Separator);

            expected.AppendLine(departament1.Name);
            expected.AppendLine(Environment.NewLine);
            expected.AppendLine("� ������������ ��� �����������!");
            expected.AppendLine(Environment.NewLine);
            expected.AppendLine(Separator);

            expected.AppendLine(departament2.Name);
            expected.AppendLine(Environment.NewLine);
            expected.AppendLine($"{employee1.Name,-EmployeeNameLength}{employee1.Salary}�");
            expected.AppendLine(Environment.NewLine);
            expected.AppendLine($"����� �� ������ 70000�");
            expected.AppendLine(Environment.NewLine);
            expected.AppendLine(Separator);

            expected.AppendLine(Environment.NewLine);
            expected.AppendLine($"����� �� ����������� 70000�");

            var service = new ReportFormatterService();
            var actual = service.Format(report);

            Assert.AreEqual(expected.ToString(), actual);
        }

        [Test]
        public void Format_GivenReportWithoutDepartments_DisplaysCorrepondingMessage()
        {
            var report = new CompanyReport
            {
                Year = 2022,
                Month = 2,
                DepartamentReports = new DepartamentReport[] { }
            };

            var expected = "� �������� ��� �������������!";

            var service = new ReportFormatterService();
            var actual = service.Format(report);

            Assert.AreEqual(expected, actual);
        }
    }
}