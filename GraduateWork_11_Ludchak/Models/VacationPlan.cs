using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

#nullable enable

namespace GraduateWork_11_Ludchak.Models
{
    internal class VacationPlan
    {
        public string? Id { get; set; }
        public DateTime? WorkingPeriodStart { get; set; }
        public DateTime? WorkingPeriodEnd { get; set; }
        public int? AnnualBase { get; set; }
        public string? VacationTime { get; set; }
        public int? AdditionalVacation { get; set; }        
        public string? AddVacationTime { get; set; }        
        public Employee? employee { get; set; }

    }
}
