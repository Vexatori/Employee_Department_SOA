using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeDepartmentWEB.Models
{
    public class DataEmployeeDepartment
    {
        private SqlConnection sqlConnection;

        public DataEmployeeDepartment()
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;
                              Initial Catalog=demo;
                              Integrated Security=True;
                              Pooling=True";

            sqlConnection = new SqlConnection( connectionString );
            sqlConnection.Open();
        }

        public ObservableCollection<Department> GetDepartments()
        {
            ObservableCollection<Department> departments = new ObservableCollection<Department>();

            string sqlCommand = @"SELECT * FROM Departments ORDER BY Id";

            using ( SqlCommand command = new SqlCommand(sqlCommand, sqlConnection ) )
            {
                using ( SqlDataReader reader = command.ExecuteReader() )
                {
                    while( reader.Read() )
                    {
                        departments.Add(
                            new Department()
                            {
                                Department_name = reader["Department_name"].ToString(),
                                Id = ( int )reader["Id"]
                            } );
                    }
                }
            }

            return departments;
        }

        public ObservableCollection<Employee> GetEmployees ()
        {
            ObservableCollection<Employee> employees = new ObservableCollection<Employee>();

            string sqlCommand = @"SELECT * FROM Employees ORDER BY EmployeeId";

            using ( SqlCommand command = new SqlCommand( sqlCommand, sqlConnection ) )
            {
                using ( SqlDataReader reader = command.ExecuteReader() )
                {
                    while ( reader.Read() )
                    {
                        employees.Add(
                            new Employee()
                            {
                                Name = reader["Name"].ToString(),
                                Second_name = reader["Second_name"].ToString(),
                                Age = ( int )reader["Age"],
                                EmployeeId = ( int )reader["EmployeeId"],
                                DepartmentId = ( int )reader["DepartmentId"]
                            } );
                    }
                }
            }

            return employees;
        }

        public ObservableCollection<Employee> GetEmployeesFromDepartment( double id )
        {
            ObservableCollection<Employee> employees = new ObservableCollection<Employee>();

            string sqlCommand = @"SELECT Name, Second_name, Age, EmployeeId, DepartmentId FROM Employees WHERE DepartmentId = " + ( int ) id;

            using ( SqlCommand command = new SqlCommand( sqlCommand, sqlConnection ) )
            {
                using ( SqlDataReader reader = command.ExecuteReader() )
                {
                    while ( reader.Read() )
                    {
                        employees.Add(
                            new Employee()
                            {
                                Name = reader["Name"].ToString(),
                                Second_name = reader["Second_name"].ToString(),
                                Age = ( int )reader["Age"],
                                EmployeeId = ( int )reader["EmployeeId"],
                                DepartmentId = ( int )reader["DepartmentId"]
                            } );
                    }
                }
            }

            return employees;
        }

        public Employee GetEmployee ( int Id )
        {
            string sqlCommand = $@"SELECT * FROM Employees WHERE EmployeeId = {Id}";
            Employee employee = new Employee();
            using( SqlCommand command = new SqlCommand( sqlCommand, sqlConnection ) )
            {
                using( SqlDataReader reader = command.ExecuteReader() )
                {
                    while ( reader.Read() )
                    {
                        employee = new Employee()
                        {
                            Name = reader["Name"].ToString(),
                            Second_name = reader["Second_name"].ToString(),
                            Age = ( int )reader["Age"],
                            EmployeeId = ( int )reader["EmployeeId"],
                            DepartmentId = ( int )reader["DepartmentId"]
                        };
                    }
                }
            }
            return employee;
        }

        public bool DeleteEmployee( int id )
        {
            try
            {
                string sqlCommand = @"DELETE FROM Employees WHERE EmployeeId = " + id;

                using( SqlCommand command = new SqlCommand( sqlCommand, sqlConnection ) )
                {
                    command.ExecuteNonQuery();
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool AddEmployee( Employee employee )
        {
            try
            {
                string sqlCommand = $@"INSERT INTO Employees(Name, Second_name, Age, DepartmentId) 
                                    VALUES('{employee.Name}', '{employee.Second_name}', '{employee.Age}', '{employee.DepartmentId}')";

                using( SqlCommand command = new SqlCommand( sqlCommand, sqlConnection ) )
                {
                    command.ExecuteNonQuery();
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool EditEmployee( int id, Employee employee )
        {
            try
            {
                string sqlCommand = $@"UPDATE Employees SET Name = '{employee.Name}', Second_name = '{employee.Second_name}', Age = {employee.Age} WHERE EmployeeId = {id}";

                using( SqlCommand command = new SqlCommand( sqlCommand, sqlConnection ) )
                {
                    command.ExecuteNonQuery();
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}