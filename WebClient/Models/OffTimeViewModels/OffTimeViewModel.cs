using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models.OffTimeViewModels
{
    public class OffTimeViewModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        [Required]
        public OffTimeType OffTimeType { get; set; }

        public bool IsApproved { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public string Discription { get; set; }

        public OffTimeViewModel()
        {

        }

        public OffTimeViewModel(OffTime offTime)
        {
            Date = offTime.Date;
            OffTimeType = offTime.OffTimeType;
            IsApproved = offTime.IsApproved;
            EmployeeId = offTime.EmployeeId;
        }
    }
}
