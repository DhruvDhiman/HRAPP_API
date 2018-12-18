using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Entities
{
    public static class AppDbSeeder
    {
        public static void EnsureSeedData(this AppDbContext context)
        {
            ////empty the Employee
            //context.Employees.RemoveRange(context.Employees);
            //context.SaveChanges();

            //creating list for seeding
            if (!context.Employees.Any())
            {
                var EmployeeList = new List<Employee>(){
                    new Employee()
                    {
                        EmpId = new Guid("e52fb6e8-118b-4fb6-a76c-c655400ee7ca"),
                        FirstName = "Michael",
                        LastName = "Jordan",
                        Location = "Tampa",
                        EmailId = "michael.jordan@app.com",
                        EmployeeSalary = new EmployeeSalary()
                        {
                            id = new Guid("90e69e1b-0967-4e5e-9359-ffbc25da1ffa"),
                            BaseSalary = 75980,
                            Deduction401 = 0.0,
                            DeductionMedicare = 0.0,
                            DeductionDental = 0

                        }
                    }
                };
                context.Employees.AddRange(EmployeeList);
            }
            //saving all employees
            context.SaveChanges();
        }
    }
}
