using EmployeeCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeCrud.ViewModel
{
    public class EmployeeSalaryViewModel
    {
        public Employee Employee { get; set; }
        public double Salary { get; set; }
    }
}