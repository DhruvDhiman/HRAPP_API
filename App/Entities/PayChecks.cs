using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace App.Entities
{
    public class PayChecks
    {
        [Key]
        public Guid TxnId { get; set; }
        [Required]
        public double BaseSalary { get; set; }
        [Required]
        public DateTimeOffset TxnDate { get; set; }
        [Required]
        public double FederalTax { get; set; }
        [Required]
        public double StateTax { get; set; }
        [Required]
        public double Deduction401 { get; set; }
        [Required]
        public double DeductionMedicare { get; set; }
        [Required]
        public double DeductionDental { get; set; }
        [Required]
        public double TakeHomSalary { get; set; }

        public Guid EmpId { get; set; }
        public Employee Employee { get; set; }
    }
}
