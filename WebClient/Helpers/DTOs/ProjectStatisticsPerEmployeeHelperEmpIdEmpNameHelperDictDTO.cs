using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Models.ClientViewModels;

namespace WebClient.Helpers.DTOs
{
    public class ProjectStatisticsPerEmployeeHelperEmpIdEmpNameHelperDictDTO
    {
        public Dictionary<int, string> EmpIdEmpNameHelperDict { get; set; }

        public List<ProjectStatisticsPerEmployeeHelper> ProjectStatisticsPerEmployeeHelpers { get; set; }

        public ProjectStatisticsPerEmployeeHelperEmpIdEmpNameHelperDictDTO()
        {

        }

        public ProjectStatisticsPerEmployeeHelperEmpIdEmpNameHelperDictDTO(Dictionary<int, string> empIdEmpNameHelperDict,
            List<ProjectStatisticsPerEmployeeHelper> projectStatisticsPerEmployeeHelpers)
        {
            EmpIdEmpNameHelperDict = empIdEmpNameHelperDict;
            ProjectStatisticsPerEmployeeHelpers = projectStatisticsPerEmployeeHelpers;
        }
    }
}
