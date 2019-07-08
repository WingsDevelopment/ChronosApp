using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models.ClientViewModels
{
    public class ProjectStatisticsPerEmployeeHelper
    {
        public string ProjectName { get; set; }

        public Dictionary<DateTime, Dictionary<int, decimal>> TimeStatisticsByProjectPerEmployee { get; set; }
        
        public ProjectStatisticsPerEmployeeHelper()
        {

        }

        public ProjectStatisticsPerEmployeeHelper(string projectName,
                                       Dictionary<DateTime, Dictionary<int, decimal>> timeStatisticsByProjectPerEmployee)
        {
            ProjectName = projectName;
            TimeStatisticsByProjectPerEmployee = timeStatisticsByProjectPerEmployee;
        }
    }
}
