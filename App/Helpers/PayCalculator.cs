using App.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Helpers
{
    public class PayCalculator
    {
        public static PayChecks GeneratePayCheck(Employee employee)
        {
            //following semimonthly i.e. 24 paychecks
            double salary = Math.Round(employee.EmployeeSalary.BaseSalary / 24, 2);
            double taxableIncome = CalculateTaxableIncome(salary, employee.EmployeeSalary.Deduction401, employee.EmployeeSalary.DeductionMedicare,
                                                        employee.EmployeeSalary.DeductionDental);
            double tax = CalculateFederalTax(taxableIncome);
            double takeHomeSalary = taxableIncome - tax;
            var payCheckToReturn = new PayChecks
            {
                BaseSalary = salary,
                Deduction401 = salary * employee.EmployeeSalary.Deduction401,
                DeductionMedicare = salary * employee.EmployeeSalary.DeductionMedicare,
                DeductionDental = salary * salary * employee.EmployeeSalary.DeductionDental,
                FederalTax = tax,
                StateTax = 0,
                TakeHomSalary = takeHomeSalary,
                TxnDate = DateTime.Today,
                EmpId = employee.EmpId,
                Employee = employee,
                TxnId = Guid.NewGuid()

            };
            Console.WriteLine("payCheckToReturn" + payCheckToReturn);
            return payCheckToReturn;
        }

        private static double CalculateTaxableIncome(double baseSalary, double deduction401, double deductionMedicare, double deductionDental)
        {
            return Math.Round(baseSalary - ((baseSalary * deduction401) + (baseSalary * deductionMedicare) + (baseSalary * deductionDental)), 2);
        }
        private static double CalculateFederalTax(double salary)
        {
            double tax = 0;
            if(salary < 154)
            {
                tax = 0;
            }
            else if( salary <= 551)
            {
                tax = (salary - 154) * 0.1;
            }
            else if( salary <= 1767)
            {
                tax = 39.7 + (salary - 551) * 0.12;
            }
            else if(salary <= 3592)
            {
                tax = 185.62 + (salary - 1767) * 0.22;
            }
            else if( salary <= 6717)
            {
                tax = 587.12 + (salary - 3592) * 0.24;
            }
            else if(salary<= 8488)
            {
                tax = 1337.12 + (salary - 6717) * 0.32;
            }
            else if(salary <= 20988)
            {
                tax = 1903.84 + (salary - 8488) * 0.35;
            }
            else
            {
                tax = 6278.84 + (salary - 20988) * 0.37;
            }
            return Math.Round(tax, 2);
        }
    }
}
