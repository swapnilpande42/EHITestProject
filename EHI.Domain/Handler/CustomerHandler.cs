using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EHI.Domain
{
    public class CustomerHandler : ICustomerHandler
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> CreateCustomer(Customer customer)
        {
            return await _customerRepository.CreateCustomer(customer);
            
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            return await _customerRepository.UpdateCustomer(customer);
            
        }

        public async Task<Customer> UpdateCustomer(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            customer.Status = Status.Inactive;
            return await UpdateCustomer(customer);
        }

        public async Task<Customer> DeleteCustomer(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            customer.Status = Status.Inactive;
            return await _customerRepository.Update(customer);           
        }


        public async Task<IEnumerable<Customer>> GetAllActiveCustomers()
        {
            var customers = await _customerRepository.GetAllCustomerAsync();
            return customers.Where(c => c.Status == Status.Active);
        }

        public async Task<Customer> GetCustomer(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            return customer;
        }
    }
}
