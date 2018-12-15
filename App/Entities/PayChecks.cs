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
        public float BaseSalary { get; set; }
        [Required]
        public DateTimeOffset TxnDate { get; set; }
        [Required]
        public float FederalTax { get; set; }
        [Required]
        public float StateTax { get; set; }
        [Required]
        public float Deduction401 { get; set; }
        [Required]
        public float DeductionMedicare { get; set; }
        [Required]
        public float DeductionDental { get; set; }
        [Required]
        public float TakeHomSalary { get; set; }

        public Guid EmpId { get; set; }
        public Employee Employee { get; set; }
    }
}
