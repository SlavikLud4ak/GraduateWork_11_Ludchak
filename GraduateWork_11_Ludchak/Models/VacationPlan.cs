using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> WorkingPeriodStart { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> WorkingPeriodEnd { get; set; }
        public int? AnnualBase { get; set; }
        public string? VacationTime { get; set; }
        public int? AdditionalVacation { get; set; }        
        public string? AddVacationTime { get; set; }
    }
}
