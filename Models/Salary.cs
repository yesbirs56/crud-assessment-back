using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EmployeeCrud.Models
{
    public class Salary
    {
        
        public int EmployeeId { get; set; }
        public double SalaryAmount { get; set; }
        public Employee Employee { get; set; }
    }
}