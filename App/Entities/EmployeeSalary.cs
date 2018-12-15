using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace App.Entities
{
    public class EmployeeSalary
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        public double BaseSalary { get; set; }
        public double Deduction401 { get; set; }
        public double DeductionMedicare { get; set; }
        public double DeductionDental { get; set; }

        public Guid EmpId { get; set; }
        public Employee Employee { get; set; }
    }
}
