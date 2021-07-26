using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeCrud.Models
{
    public class Department
    {
        [Key]
        public int DeptId { get; set; }
        [Required]
        [MaxLength(50)]
        public string DptName { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
        public ICollection<Employee> Employees { get; set; }

    }
}