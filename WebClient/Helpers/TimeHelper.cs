using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Helpers
{
    public static class TimeHelper
    {
        public static bool IsBusinessDay(DateTime day)
        {
            if (day.DayOfWeek == DayOfWeek.Saturday || day.DayOfWeek == DayOfWeek.Sunday)
                return false;
            return true;
        }

        public static IEnumerable<DateTime> EachBusinessDay(DateTime from, DateTime to)
        {
            for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
            {
                if(IsBusinessDay(day))
                    yield return day;
            }
        }
    }
}
