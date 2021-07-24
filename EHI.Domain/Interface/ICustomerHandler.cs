using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EHI.Domain
{
    public interface ICustomerHandler
    {
        Task<Customer> CreateCustomer(Customer customer);
        Task<Customer> UpdateCustomer(Customer customer);
        Task<Customer> UpdateCustomer(int id);
        Task<Customer> DeleteCustomer(int id);
        Task<IEnumerable<Customer>> GetAllActiveCustomers();
        Task<Customer> GetCustomer(int id);
    }
}
