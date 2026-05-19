using EmployeeService.Models;
using EmployeeService.Repositories;
using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel.Web;

namespace EmployeeService
{
    public class Service1 : IEmployeeService
    {
        private readonly EmployeeRepository _repository = new EmployeeRepository();

        public Employee GetEmployeeById(int id)
        {
            if (id <= 0)
            {
                throw new WebFaultException<string>("ID cant be equal or less than 0", HttpStatusCode.BadRequest);
            }

            try
            {
                var employee = _repository.GetEmployeeById(id);

                if (employee == null)
                {
                    throw new WebFaultException<string>($"No employee with ID = {id}", HttpStatusCode.NotFound);
                }

                return employee;
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>("Internal server error: " + ex.Message, HttpStatusCode.InternalServerError);
            }
        }

        public void EnableEmployee(int id, int enable)
        {
            if (id <= 0)
            {
                throw new WebFaultException<string>("ID cant be equal or less than 0", HttpStatusCode.BadRequest);
            }

            if (enable != 0 && enable != 1)
            {
                throw new WebFaultException<string>("Enable must be 0 or 1", HttpStatusCode.BadRequest);
            }

            try
            {
                _repository.EnableEmployee(id, enable);
            }
            catch (KeyNotFoundException ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>("Internal server error: " + ex.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}