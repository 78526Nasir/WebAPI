using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class EmployeesController : ApiController
    {
        // GET
        public HttpResponseMessage Get(string gender = "all")
        {
            try
            {
                using (EmployeeDbEntities db = new EmployeeDbEntities())
                {
                    switch (gender.ToLower())
                    {
                        case "all":
                            return Request.CreateResponse(HttpStatusCode.OK, db.Employees.ToList());
                        case "male":
                            return Request.CreateResponse(HttpStatusCode.OK, db.Employees.Where(emp => emp.Gender.ToLower() == "male").ToList());
                        case "female":
                            return Request.CreateResponse(HttpStatusCode.OK, db.Employees.Where(emp => emp.Gender.ToLower() == "female").ToList());
                        default:
                            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Value for gender must be 'All', 'Male', or 'Female'. '" + gender + "' is invalid.");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // GET // id
        public HttpResponseMessage Get(int id)
        {
            try
            {
                using (EmployeeDbEntities db = new EmployeeDbEntities())
                {
                    Employee employee = db.Employees.Find(id);

                    if (employee != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, employee);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with ID = " + id.ToString() + " not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // POST
        public HttpResponseMessage Post([FromBody] Employee employee)
        {
            try
            {
                using (EmployeeDbEntities db = new EmployeeDbEntities())
                {
                    db.Employees.Add(employee);
                    db.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + "/" + employee.ID.ToString());

                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // DELETE
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (EmployeeDbEntities db = new EmployeeDbEntities())
                {
                    Employee employee = db.Employees.Find(id);

                    if (employee != null)
                    {
                        db.Employees.Remove(employee);
                        db.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with ID = " + id.ToString() + " not found to delete.");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // PUT
        public HttpResponseMessage Put(int id, [FromBody] Employee employee)
        {
            try
            {
                using (EmployeeDbEntities db = new EmployeeDbEntities())
                {
                    Employee entity = db.Employees.Find(id);

                    if (entity != null)
                    {
                        entity.FirstName = employee.FirstName;
                        entity.LastName = employee.LastName;
                        entity.Gender = employee.Gender;
                        entity.Salary = employee.Salary;

                        db.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with ID = " + id.ToString() + " not found to update.");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


    }
}