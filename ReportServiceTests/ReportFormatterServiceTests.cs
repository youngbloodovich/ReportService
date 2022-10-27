using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using ReportService.Domain;
using ReportService.Helpers;
using ReportService.Repositories.Entities;
using ReportService.Services;
using System;
using System.Text;

namespace ReportServiceTests
{
    public class ReportFormatterServiceTests
    {
        private const string Separator = "---";

        [Test]
        public void Format_GivenCompanyReport_WorksOk()
        {
            var report = new CompanyReport
            {
                Year = 2022,
                Month = 2,
                DepartamentReports = new DepartamentReport[]
                {
                    new DepartamentReport
                    {
                        Name = "departament1",
                        EmployeeReports = new EmployeeReport[]
                        {
                            new EmployeeReport
                            {
                                Name = "employee1",
                                Salary = 70000
                            }
                        }
                    },
                    new DepartamentReport
                    {
                        Name = "departament2",
                        EmployeeReports = new EmployeeReport[]
                        {
                            new EmployeeReport
                            {
                                Name = "employee2",
                                Salary = 70000
                            },
                            new EmployeeReport
                            {
                                Name = "employee3",
                                Salary = 20000
                            }
                        }
                    },
                    new DepartamentReport
                    {
                        Name = "departament3",
                        EmployeeReports = new EmployeeReport[]
                        {
                            new EmployeeReport
                            {
                                Name = "employee4",
                                Salary = 50000
                            },
                            new EmployeeReport
                            {
                                Name = "employee5",
                                Salary = 25000
                            },
                            new EmployeeReport
                            {
                                Name = "employee6",
                                Salary = 65000
                            }
                        }
                    }
                }
            };

            var expected = new StringBuilder();
            expected.AppendLine("������� 2022");
            expected.AppendLine(Environment.NewLine);
            expected.AppendLine(Separator);
            expected.AppendLine("departament1");
            expected.AppendLine(Environment.NewLine);
            expected.AppendLine("employee1 70000�");
            expected.AppendLine(Environment.NewLine);
            expected.AppendLine($"����� �� ������ 70000�");
            expected.AppendLine(Environment.NewLine);
            expected.AppendLine(Separator);
            expected.AppendLine("departament2");
            expected.AppendLine(Environment.NewLine);
            expected.AppendLine("employee2 70000�");
            expected.AppendLine(Environment.NewLine);
            expected.AppendLine("employee3 20000�");
            expected.AppendLine(Environment.NewLine);
            expected.AppendLine($"����� �� ������ 90000�");
            expected.AppendLine(Environment.NewLine);
            expected.AppendLine(Separator);
            expected.AppendLine("departament3");
            expected.AppendLine(Environment.NewLine);
            expected.AppendLine("employee4 50000�");
            expected.AppendLine(Environment.NewLine);
            expected.AppendLine("employee5 25000�");
            expected.AppendLine(Environment.NewLine);
            expected.AppendLine("employee6 65000�");
            expected.AppendLine(Environment.NewLine);
            expected.AppendLine($"����� �� ������ 140000�");
            expected.AppendLine(Environment.NewLine);
            expected.AppendLine(Separator);
            expected.AppendLine(Environment.NewLine);
            expected.AppendLine($"����� �� ����������� 300000�");

            var service = new ReportFormatterService();
            var actual = service.Format(report);

            Assert.AreEqual(expected.ToString(), actual);
        }
    }
}