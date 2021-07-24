using EHI.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EHI.Domain
{
    public interface ICustomerRepository : IRepositoryBase<Customer>
    {
        Task<IEnumerable<Customer>> GetAllCustomerAsync();
        Task<Customer> GetCustomerByIdAsync(int id);      
        Task<Customer> CreateCustomer(Customer owner);
        Task<Customer> UpdateCustomer(Customer owner);
        Task<Customer> DeleteCustomer(Customer owner);
    }
}
