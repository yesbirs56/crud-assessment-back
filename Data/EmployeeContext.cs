using EmployeeCrud.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmployeeCrud.Data
{
    public class EmployeeContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Employee>()
                .HasKey(e=>e.EmployeeId)
                .HasRequired(e => e.Salary)
                .WithRequiredPrincipal(s => s.Employee);
            modelBuilder.Entity<Salary>().HasKey(s => s.EmployeeId);
        }
    }
}