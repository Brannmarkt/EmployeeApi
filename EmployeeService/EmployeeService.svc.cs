using EmployeeService.Models;
using EmployeeService.Repositories;

namespace EmployeeService
{
    public class Service1 : IEmployeeService
    {
        private readonly EmployeeRepository _repository = new EmployeeRepository();

        public Employee GetEmployeeById(int id)
        {
            return _repository.GetEmployeeById(id);
        }

        public void EnableEmployee(int id, int enable)
        {
            _repository.EnableEmployee(id, enable);
        }
    }
}