using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Services
{
    public class AppRepository : IAppRepository
    {
        private AppDbContext _context;

        public AppRepository(AppDbContext context)
        {
            _context = context;
        }
        public void AddEmployee(Employee employee)
        {
            employee.EmpId = Guid.NewGuid();
            _context.Employees.Add(employee);

            if(employee.EmployeeSalary != null)
            {
                employee.EmployeeSalary.id = Guid.NewGuid();            
            }
        }

        public void DeleteEmployee(Employee employee)
        {
            _context.Employees.Remove(employee);
        }

        public bool EmployeeExists(Guid employeeId)
        {
            return _context.Employees.Any(e => e.EmpId == employeeId);
        }

        public Employee GetEmployee(Guid employeeId)
        {
            return _context.Employees
                            .Include(e => e.EmployeeSalary)
                            .FirstOrDefault(e => e.EmpId == employeeId);
        }

        public IEnumerable<Employee> GetEmployees()
        {
            var employees = _context.Employees
                                    .Include(e => e.EmployeeSalary)
                                    .ToList();
            Console.WriteLine("************************Employees list employee salary.***************************");
            foreach (var employee in employees)
            {
                Console.WriteLine(employee.EmployeeSalary.BaseSalary);
                Console.WriteLine(employee.EmployeeSalary.Deduction401);
                Console.WriteLine(employee.EmployeeSalary.DeductionMedicare);
                Console.WriteLine(employee.EmployeeSalary.DeductionDental);
            }
            Console.WriteLine("*******************END*********************************************************");
            return employees;
        }

        public IEnumerable<Employee> GetEmployees(IEnumerable<Guid> employeeIds)
        {
            return _context.Employees
                            .Where(e => employeeIds.Contains(e.EmpId))
                            .OrderBy(e => e.FirstName)
                            .ThenBy(e => e.LastName)
                            .ToList();
        }

        public void UpdateEmployee(Employee employee)
        {
            // not required, stub for testing
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public EmployeeSalary GetEmployeeSalary(Guid employeeId)
        {
            return _context.EmployeeSalaries.FirstOrDefault(es => es.EmpId == employeeId);
        }

        public void AddEmployeeSalary(EmployeeSalary employeeSalary)
        {
            //would require Employee to be existing.
            employeeSalary.id = Guid.NewGuid();

        }

        public void UpdateEmployeeSalary(EmployeeSalary employeeSalary)
        {
            //no implementation reuired
        }

        public void AddPayCheck(PayChecks payCheck)
        {
            payCheck.TxnId = Guid.NewGuid();
            _context.PayChecks.Add(payCheck);
        }

        public IEnumerable<PayChecks> GetPayCheck()
        {
            return _context.PayChecks
                            .Include(p => p.Employee)
                            .ToList();
        }

        public PayChecks GetPayCheck(Guid txnId)
        {
            return _context.PayChecks
                            .Include(p => p.Employee)
                            .FirstOrDefault(p => p.TxnId == txnId);
        }
    }
}
