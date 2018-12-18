using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Models
{
    public class PayCheckDto
    {
        public Guid TxnId { get; set; }
        public float BaseSalary { get; set; }
        public DateTimeOffset TxnDate { get; set; }
        public float FederalTax { get; set; }
        public float StateTax { get; set; }
        public float Deduction401 { get; set; }
        public float DeductionMedicare { get; set; }
        public float DeductionDental { get; set; }
        public float TakeHomSalary { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
