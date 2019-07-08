using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models.TimeShiftItemViewModels
{
    public class ProjectStatisticsHelper
    {
        public string ProjectName { get; set; }

        public Dictionary<DateTime, decimal> TimeStatisticsByProject { get; set; }

        public ProjectStatisticsHelper()
        {

        }

        public ProjectStatisticsHelper(string projectName,
                                       Dictionary<DateTime, decimal> timeStatisticsByProject)
        {
            ProjectName = projectName;
            TimeStatisticsByProject = timeStatisticsByProject;
        }
    }
}
