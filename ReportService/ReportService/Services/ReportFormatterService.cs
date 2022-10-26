using ReportService.Domain;
using ReportService.Repositories.Interfaces;
using ReportService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReportService.Services
{
    public class ReportFormatterService : IReportFormatterService
    {
        public ReportFormatterService()
        {
        }

        public string Format(CompanyReport report)
        {
            throw new NotImplementedException();
        }
    }
}
