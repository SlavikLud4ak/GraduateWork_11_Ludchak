using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

#nullable enable

namespace GraduateWork_11_Ludchak.Models
{
    internal class Employee
    {
        public string? Id { get; set; }
        public string? FullName { get; set; }
        public string? Position { get; set; }
        public string? Departament { get; set; }
        public DateTime? StartDate { get; set; }
        public List<VacationPlan>? VacationPlans { get; set; }
    }
}
