using EmployeeService.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeService.Repositories
{
    public class EmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DbServer"].ConnectionString;
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            var employees = new List<Employee>();

            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = @"
                    WITH EmployeeTree AS (
                        SELECT ID, Name, ManagerID, Enable 
                        FROM Employee WHERE ID = @Id
                        UNION ALL
                        SELECT e.ID, e.Name, e.ManagerID, e.Enable
                        FROM Employee e
                        INNER JOIN EmployeeTree et ON e.ManagerID = et.ID
                    )
                    SELECT ID, Name, ManagerID, Enable FROM EmployeeTree;";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        await connection.OpenAsync();

                        using (var reader = await  command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                employees.Add(new Employee
                                {
                                    ID = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    ManagerID = reader.IsDBNull(2) ? 0 : reader.GetInt32(2),
                                    Enable = reader.GetBoolean(3)
                                });
                            }
                        }
                    }
                }
            }
            catch(SqlException ex)
            {
                throw new Exception("Database connection error", ex);
            }

            return BuildTree(employees, id);
        }

        public async Task EnableEmployee(int id, int enable)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    string query = "UPDATE Employee SET Enable = @Enable WHERE ID = @Id";
                    using (var command = new SqlCommand(query, connection))
                    {
                        bool isEnabled = enable != 0;

                        command.Parameters.AddWithValue("@Enable", isEnabled);
                        command.Parameters.AddWithValue("@Id", id);

                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch(SqlException ex)
            {
                throw new Exception("Database connection error", ex);
            }
        }

        private Employee BuildTree(List<Employee> employees, int rootId)
        {
            if (employees == null || !employees.Any())
                return null;

            var lookup = employees.ToLookup(e => e.ManagerID);

            var root = employees.FirstOrDefault(e => e.ID == rootId);

            if (root != null)
            {
                AttachEmployees(root, lookup);
            }

            return root;
        }

        private void AttachEmployees(Employee rootEmployee, ILookup<int, Employee> lookup)
        {
            rootEmployee.Employees = lookup[rootEmployee.ID].ToList();

            foreach (var child in rootEmployee.Employees)
            {
                AttachEmployees(child, lookup);
            }
        }
    }
}