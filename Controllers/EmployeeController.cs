using EmployeeCrud.Data;
using EmployeeCrud.Models;
using EmployeeCrud.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace EmployeeCrud.Controllers
{
    public class EmployeeController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetEmployees()
        {
            using (EmployeeContext context = new EmployeeContext())
            {
                var employees = context.Employees.Select(e=>new { e.EmployeeId, e.Name,e.DOJ,e.Mobile,e.Email,e.Address,e.Department.DptName,Salary=e.Salary.SalaryAmount }).ToList();
                var employees1 = employees.OrderByDescending(e => e.Salary);
                return Ok(employees1.ToList());
            }
        }
        [HttpGet]
        public IHttpActionResult GetEmployeeById(int id)
        {
            try
            {
                using (EmployeeContext context = new EmployeeContext())
                {
                    var employee = context.Employees.Where(e => e.EmployeeId == id).Select(e => new { e.DepartmentId,e.Department.DptName, e.DOJ, e.Email, e.EmployeeId, e.Mobile, e.Address, e.Name,Salary= e.Salary.SalaryAmount }).FirstOrDefault();
                    if (employee == null)
                    {
                        return NotFound();
                    }
                    return Ok(employee);
                }
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }

        }
       
        public IHttpActionResult CreateEmployee([FromBody] EmployeeSalaryViewModel employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (EmployeeContext context = new EmployeeContext())
                    {
                        context.Employees.Add(employee.Employee);
                        context.Salaries.Add(new Salary()
                        {
                            Employee = employee.Employee,
                            EmployeeId=employee.Employee.EmployeeId,
                            SalaryAmount = employee.Salary
                            
                        });
                        context.SaveChanges();
                    }
                    return Created(Request.RequestUri + "/" + employee.Employee.EmployeeId, employee);
                }
                else
                {
                    return BadRequest("Something Went Wrong");
                }
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }
        [HttpPut]
        public IHttpActionResult UpdateEmployee(int id, [FromBody] EmployeeSalaryViewModel employeeSalary)
        {
            try
            {
                Employee employee = employeeSalary.Employee;
                using (EmployeeContext context = new EmployeeContext())
                {
                    Employee savedEmployee = context.Employees.Include(e=>e.Salary).FirstOrDefault(e=>e.EmployeeId==id);
                    if (savedEmployee == null)
                    {
                        return NotFound();
                    }
                    savedEmployee.Name = employee.Name;
                    savedEmployee.Email = employee.Email;
                    savedEmployee.DOJ = employee.DOJ;
                    savedEmployee.Address = employee.Address;
                    savedEmployee.DepartmentId = employee.DepartmentId;
                    savedEmployee.Salary.SalaryAmount = employeeSalary.Salary;
                    context.SaveChanges();
                    return Ok(savedEmployee);
                }
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }
        [HttpDelete]
        public IHttpActionResult DeleteEmployee(int id)
        {
            try
            {
                using (EmployeeContext context = new EmployeeContext())
                {
                    Employee employee = context.Employees.FirstOrDefault(e=>e.EmployeeId==id);
                    if (employee == null)
                    {
                        return NotFound();
                    }
                    context.Employees.Remove(employee);
                    context.SaveChanges();
                    return Ok($"Employee {employee.Name} is deleted Succesfully");
                }
            }
            catch (Exception exp)
            {
                return BadRequest(exp.Message);
            }

        }
    }
}
