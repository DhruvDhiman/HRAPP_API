using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Entities;

namespace App.Models
{
    public class EmployeeDto
    {
        public Guid EmpId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Location { get; set; }
        public string EmailId { get; set; }
        public double BaseSalary { get; set; }
        public double Deduction401 { get; set; }
        public double DeductionMedicare { get; set; }
        public double DeductionDental { get; set; }
    }
}
