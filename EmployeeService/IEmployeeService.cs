using EmployeeService.Models;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Threading.Tasks;

namespace EmployeeService
{
    [ServiceContract]
    public interface IEmployeeService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", 
            UriTemplate = "GetEmployeeById?id={id}",
            ResponseFormat = WebMessageFormat.Json,  
            BodyStyle = WebMessageBodyStyle.Bare)]
        Task<Employee> GetEmployeeById(int id);

        [OperationContract]
        [WebInvoke(Method = "PUT", 
            UriTemplate = "EnableEmployee?id={id}", 
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        Task EnableEmployee(int id, int enable);
    }
}
