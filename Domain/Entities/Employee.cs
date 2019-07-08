using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public int TeamdId { get; set; }

        public int MainProjectId { get; set; }

        [Computed]
        public List<Project> PermitedProjects { get; set; }

        [Computed]
        public Team Team { get; set; }

        public int PriceLevel { get; set; }

        public decimal HourPerDay { get; set; }

        public bool IsDeleted { get; set; }

        public Employee()
        {

        }

        public Employee(string userName)
        {
            this.UserName = userName;
            this.HourPerDay = 8;
            this.PriceLevel = 1;
        }
    }
}
