using System.Collections.Generic;
using System.Runtime.Serialization;

namespace EmployeeService.Models
{
    [DataContract]
    public class Employee
    {
        [DataMember(Order = 1)]
        public int ID { get; set; }

        [DataMember(Order = 2)]
        public string Name { get; set; }

        [DataMember(Order = 3)]
        public int ManagerID { get; set; }

        [DataMember(Order = 4, EmitDefaultValue = false)] 
        public bool Enable { get; set; }

        [DataMember(Order = 5)]
        public List<Employee> Employees { get; set; } = new List<Employee>();

    }
}