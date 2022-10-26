using System.Collections.Generic;
using System.Linq;

namespace ReportService.Domain
{
    public class CompanyReport
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public DepartamentReport[] DepartamentReports { get; set; }

        public int Total => DepartamentReports.Sum(departament => departament.Total);
    }
}
