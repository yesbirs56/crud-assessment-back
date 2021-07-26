using EmployeeCrud.Data;
using EmployeeCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeCrud.Controllers
{
    public class DepartmentController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetDepartments()
        {
            using( EmployeeContext context = new EmployeeContext())
            {
                IEnumerable<Department> departments = context.Departments.ToList();
                return Ok(departments);
            }
        }
        [HttpGet]
        public IHttpActionResult GetDepartmentById(int id)
        {
            try
            {
                using (EmployeeContext context = new EmployeeContext())
                {
                    Department department = context.Departments.FirstOrDefault(d => d.DeptId == id);
                    if(department == null)
                    {
                       return NotFound();
                    }
                    return Ok(department);
            }
            }
            catch(Exception exp)
            {
                return BadRequest(exp.Message);
            }
            
        }
        public IHttpActionResult CreateDepartment([FromBody]Department department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using(EmployeeContext context = new EmployeeContext())
                    {
                        context.Departments.Add(department);
                        context.SaveChanges();
                    }
                   return Created(Request.RequestUri+"/"+department.DeptId,department);
                }
                else
                {
                    return BadRequest("Something Went Wrong");
                }
            }
            catch(Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }
        [HttpPut]
        public IHttpActionResult UpdateDepartment(int id,[FromBody] Department department)
        {
            try
            {
                using(EmployeeContext context = new EmployeeContext())
                {
                    Department savedDepartment = context.Departments.FirstOrDefault(d => d.DeptId == id);
                    if (savedDepartment == null)
                    {
                        return NotFound();
                    }
                    savedDepartment.Description = department.Description;
                    savedDepartment.DptName = department.DptName;
                    context.SaveChanges();
                    return Ok(savedDepartment);
                }
            }
            catch(Exception exp)
            {
                return BadRequest(exp.Message);
            }
        }
        [HttpDelete]
        public IHttpActionResult DeleteDepartment(int id)
        {
            try
            {
                using (EmployeeContext context = new EmployeeContext())
                {
                    Department department = context.Departments.FirstOrDefault(d => d.DeptId == id);
                    if (department == null)
                    {
                        return NotFound();
                    }
                    context.Departments.Remove(department);
                    context.SaveChanges();
                    return Ok($"Department {department.DptName} is deleted Succesfully");
                }
            }
            catch(Exception exp)
            {
               return BadRequest(exp.Message);
            }
           
        }
    }
}
