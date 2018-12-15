using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Entities
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(p => p.EmployeeSalary)
                .WithOne(i => i.Employee)
                .HasForeignKey<EmployeeSalary>(b => b.EmpId);

            modelBuilder.Entity<PayChecks>()
                .HasOne(s => s.Employee)
                .WithMany(e => e.IssuedPayChecks);

            //modelBuilder.Entity<Employee>()
            //    .HasData(
            //    new Employee()
            //    {
            //        EmpId = new Guid("e52fb6e8-118b-4fb6-a76c-c655400ee7ca"),
            //        FirstName = "Michael",
            //        LastName = "Jordan",
            //        Location = "Tampa",
            //        EmailId = "michael.jordan@app.com",
            //        EmployeeSalary = new EmployeeSalary()
            //        {
            //            id = new Guid("90e69e1b-0967-4e5e-9359-ffbc25da1ffa"),
            //            BaseSalary = 75980,
            //            Deduction401 = 0.0,
            //            DeductionMedicare = 0.0,
            //            DeductionDental = 0

            //        }
            //    }
            //);
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeSalary> EmployeeSalaries { get; set; }
    }
}
