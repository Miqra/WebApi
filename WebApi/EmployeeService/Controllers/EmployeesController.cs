using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeService.Controllers
{
    [Authorize]
    public class EmployeesController : ApiController
    {
        public IEnumerable<EMPLOYEE> Get()
        {
            using (EmployeeEntities entites=new EmployeeEntities())
            {
                return entites.EMPLOYEE.ToList();
            }
        }
    }
}
