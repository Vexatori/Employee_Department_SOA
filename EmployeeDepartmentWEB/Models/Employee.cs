using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeDepartmentWEB.Models
{
    public class Employee
    {
        public string Name { get; set; }
        public string Second_name { get; set; }
        public int Age { get; set; }
        public int DepartmentId { get; set; }
        public int EmployeeId { get; set; }
    }
}