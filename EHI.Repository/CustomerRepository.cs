using EHI.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EHI.Repository
{
    public class CustomerRepository : RepositoryBase<Customer,CustomerDbContext>,ICustomerRepository
    {
        public CustomerRepository(CustomerDbContext context) : base(context)
        {

        }

        public async Task<Customer> CreateCustomer(Customer customer)
        {
            return await Create(customer);
        }

        public async Task<Customer> DeleteCustomer(Customer customer)
        {
            return await Delete(customer);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomerAsync()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await FindByCondition(customer => customer.CustomerId == id).FirstOrDefaultAsync();
        }        

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            return await Update(customer);
        }
    }
}
