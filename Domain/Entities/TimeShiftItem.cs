using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class TimeShiftItem
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public int ProjectId { get; set; }

        [Computed]
        public Project Project { get; set; }

        public int Duration { get; set; }

        public DateTime Date { get; set; }

        public string Discription { get; set; }

        public bool IsBillable { get; set; }

        public bool IsDeleted { get; set; }

        public TimeShiftItem()
        {

        }

        public TimeShiftItem(int employeeId, int projectId, int duration, DateTime date, string discription, bool isBillable)
        {
            EmployeeId = employeeId;
            ProjectId = projectId;
            Duration = duration;
            Date = date;
            Discription = discription;
            IsBillable = isBillable;
        }
    }
}
