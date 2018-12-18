using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Entities;

namespace App.Services
{
    public interface IAppRepository
    {
        IEnumerable<Employee> GetEmployees();
        Employee GetEmployee(Guid employeeId);
        IEnumerable<Employee> GetEmployees(IEnumerable<Guid> employeeIds);
        void AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);

        bool EmployeeExists(Guid employeeId);

        EmployeeSalary GetEmployeeSalary(Guid employeeId);
        void AddEmployeeSalary(EmployeeSalary employeeSalary);

        void UpdateEmployeeSalary(EmployeeSalary employeeSalary);

        void AddPayCheck(PayChecks payChecks);

        IEnumerable<PayChecks> GetPayCheck();

        PayChecks GetPayCheck(Guid txnId);
        bool Save();
    }
}
