using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EmployeeDepartmentWEB.Models;

namespace EmployeeDepartmentWEB.Controllers
{
    public class UserController : ApiController
    {
        private DataEmployeeDepartment data = new DataEmployeeDepartment();

        [Route( "getemployees" )]
        public ObservableCollection<Employee> GetEmployees () => data.GetEmployees();

        [Route( "getdepartments" )]
        public ObservableCollection<Department> GetDepartments () => data.GetDepartments();

        [Route( "getemployeesfromdepartment/{id}" )]
        public ObservableCollection<Employee> GetEmployeesFromDepartment( int id ) => data.GetEmployeesFromDepartment( id );

        [Route( "getemployee/{id}" )]
        public Employee GetEmployee ( int id ) => data.GetEmployee( id );

        [Route( "addemployee" )]
        public HttpResponseMessage Post ( [FromBody]Employee value )
        {
            if ( data.AddEmployee( value ) )
            {
                return Request.CreateResponse( HttpStatusCode.Created );
            }
            else return Request.CreateResponse( HttpStatusCode.BadRequest );
        }

        [Route( "editemployee/{id}" )]
        public HttpResponseMessage Post ( int id, [FromBody]Employee employee )
        {
            if( data.EditEmployee( id, employee ) )
            {
                return Request.CreateResponse( HttpStatusCode.Created );
            }
            else return Request.CreateResponse( HttpStatusCode.BadRequest );
        }

        [Route( "deleteemployee/{id}" )]
        public HttpResponseMessage Delete ( int id )
        {
            if ( data.DeleteEmployee( id ) )
            {
                return Request.CreateResponse( HttpStatusCode.Accepted );
            }
            else return Request.CreateResponse( HttpStatusCode.BadRequest );
        }
    }
}
