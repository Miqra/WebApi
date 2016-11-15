using EmployeeDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class EmpoyeeController : ApiController
    {

        //ya Get olmalidi yada GetSomething get ile bashlanan method adi olmalidi yada [HttpGet] yazmaq lazimdi
        public HttpResponseMessage Get(string gender="All")
        {
            using (EmployeeEntities entities = new EmployeeEntities())
            {
                switch (gender.ToLower())
                {
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK,entities.EMPLOYEE.ToList());
                    case "male":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.EMPLOYEE.Where(x=>x.GENDER.ToLower()=="male").ToList());
                    case "female":
                        return Request.CreateResponse(HttpStatusCode.OK, entities.EMPLOYEE.Where(x => x.GENDER.ToLower() == "female").ToList());
                    default:
                       return Request.CreateErrorResponse(HttpStatusCode.BadRequest,"Value for gende must be male or female");
                }
               
            }
        }
        [HttpGet]
        public HttpResponseMessage LoadEmployeeById(int id)
        {
            using (EmployeeEntities entites = new EmployeeEntities())
            {
                var entity = entites.EMPLOYEE.FirstOrDefault(x => x.ID == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee not found");
                }
            }
        }

        public HttpResponseMessage Post([FromBody]EMPLOYEE employee)
        {
            try
            {
                using (EmployeeEntities entities = new EmployeeEntities())
                {
                    entities.EMPLOYEE.Add(employee);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + employee.ID.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (EmployeeEntities entities = new EmployeeEntities())
                {
                    var entity = entities.EMPLOYEE.FirstOrDefault(x => x.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                    }
                    else
                    {
                        entities.EMPLOYEE.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }


                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex);
            }
        }

        public HttpResponseMessage Put(int id, [FromBody]EMPLOYEE employee)
        {
            try
            {
                using (EmployeeEntities entities = new EmployeeEntities())
                {
                    var entity = entities.EMPLOYEE.FirstOrDefault(x => x.ID == id);
                    if (entity != null)
                    {
                        entity.FIRSTNAME = employee.FIRSTNAME;
                        entity.LASTNAME = employee.LASTNAME;
                        entity.GENDER = employee.GENDER;
                        entity.SALARY = employee.SALARY;
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK,entity);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Not Found");
                    }

                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex);
            }
           
        }
    }
}
