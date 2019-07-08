using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Helpers.DTOs
{
    public class ProjectIdEmpIdListOfTimeShiftItemsDictEmpIdEmpNameDictDTO
    {
        public Dictionary<int, Dictionary<int, List<TimeShiftItem>>> ListOfTimeShiftsByProjectPerEmployee { get; set; }
        public Dictionary<int, string> EmpIdEmpNameHelperDict { get; set; }

        public ProjectIdEmpIdListOfTimeShiftItemsDictEmpIdEmpNameDictDTO()
        {

        }

        public ProjectIdEmpIdListOfTimeShiftItemsDictEmpIdEmpNameDictDTO(
            Dictionary<int, Dictionary<int, List<TimeShiftItem>>> listOfTimeShiftsByProjectPerEmployee,
            Dictionary<int, string> empIdEmpNameHelperDict)
        {
            ListOfTimeShiftsByProjectPerEmployee = listOfTimeShiftsByProjectPerEmployee;
            EmpIdEmpNameHelperDict = empIdEmpNameHelperDict;
        }
    }
}
