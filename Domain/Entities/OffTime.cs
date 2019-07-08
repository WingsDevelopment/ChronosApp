using Dapper.Contrib.Extensions;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class OffTime
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public DateTime Date { get; set; }

        public OffTimeType OffTimeType { get; set; }

        public string Discription { get; set; }

        public bool IsApproved { get; set; }

        public bool IsDeleted { get; set; }

        [Computed]
        public Dictionary<OffTimeType, Delegate> ApprovalHelperDict { get; set; }

        public OffTime()
        {
            InitialazeHelperDicts();
        }

        public OffTime(int employeeId, DateTime date, OffTimeType offTimeType, string discription) : this()
        {
            EmployeeId = employeeId;
            Date = date;
            OffTimeType = offTimeType;
            Discription = discription;
            ApprovalHelperDict[offTimeType].DynamicInvoke(false);
        }
        
        public void SetApproved(bool approved)
        {
            this.IsApproved = approved;
        }

        // prebaciti u helper klasu?
        public void InitialazeHelperDicts()
        {
            ApprovalHelperDict = new Dictionary<OffTimeType, Delegate>();

            ApprovalHelperDict[OffTimeType.FreeDay] = new Action<bool>(SetApproved);
            ApprovalHelperDict[OffTimeType.UnpaidTime] = new Action<bool>(SetApproved);
            ApprovalHelperDict[OffTimeType.Vacation] = new Action<bool>(SetApproved); 
            ApprovalHelperDict[OffTimeType.Holiday] = new Action<bool>((a) => IsApproved = true);
            ApprovalHelperDict[OffTimeType.SickLeave] = new Action<bool>((a) => IsApproved = true);
        }
    }
}
