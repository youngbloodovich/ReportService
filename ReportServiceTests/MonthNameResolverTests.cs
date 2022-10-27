using NUnit.Framework;
using ReportService.Helpers;
using System;

namespace ReportServiceTests
{
    public class MonthNameResolverTests
    {
        [Test]
        public void MonthNameResolver_GivenMonthBetween1And12_WorksOk()
        {
            var expected = new string[]
            {
                "Январь", "Февраль", "Март", "Апрель",
                "Май", "Июнь", "Июль", "Август",
                "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"
            };

            for (int i = 1; i <= 12; ++i)
            {
                var actual = MonthNameResolver.GetName(i);
                Assert.AreEqual(expected[i - 1], actual);
            }
        }

        [Test]
        public void MonthNameResolver_GivenNegativeMonth_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => MonthNameResolver.GetName(-1), "Month number out of range");
        }

        [Test]
        public void MonthNameResolver_GiveMonthMoreThan12_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => MonthNameResolver.GetName(13), "Month number out of range");
        }
    }
}