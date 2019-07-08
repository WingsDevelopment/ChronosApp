using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models.OffTimeViewModels
{
    public class ListOfOffTimesViewModel
    {
        public IEnumerable<OffTime> OffTimes { get; set; }

        public string UserName { get; set; }

        public ListOfOffTimesViewModel()
        {

        }

        public ListOfOffTimesViewModel(IEnumerable<OffTime> items, string userName)
        {
            UserName = userName;
            OffTimes = items;
        }

        public int GetNumberOfOffTimesByType(OffTimeType type)
        {
            return OffTimes.Where(x => x.OffTimeType == type).Count();
        }
    }
}
