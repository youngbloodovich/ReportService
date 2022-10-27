using System.Globalization;
using System;

namespace ReportService.Helpers
{
    /// <summary>
    /// Month name resolver
    /// </summary>
    public static class MonthNameResolver
    {
        /// <summary>
        /// Returns human-readable name of month in the current culture
        /// </summary>
        /// <param name="monthNum">Month number between 1 and 12</param>
        /// <returns>Month name</returns>
        /// <exception cref="ArgumentOutOfRangeException">Throws if given incorrect month nuber</exception>
        public static string GetName(int monthNum)
        {
            if (monthNum <= 0 || monthNum > 12)
            {
                throw new ArgumentOutOfRangeException("Month number out of range");
            }

            return new DateTime(1, monthNum, 1).ToString("MMMM", CultureInfo.CurrentCulture);
        }
    }
}
