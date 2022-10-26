using System.Globalization;
using System;

namespace ReportService.Helpers
{
    public static class MonthNameResolver
    {
        public static string GetName(int year, int monthNum)
        {
            if (year <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(year));
            }

            if (monthNum <= 0 || monthNum > 12)
            {
                throw new ArgumentOutOfRangeException(nameof(monthNum));
            }

            return new DateTime(year, monthNum, 1).ToString("MMMM", CultureInfo.CurrentCulture);
        }
    }
}
