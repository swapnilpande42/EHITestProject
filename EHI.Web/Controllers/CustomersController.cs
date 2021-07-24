using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EHI.Domain;
using EHI.Repository;

namespace EHI.Web.Controllers
{
    public class CustomersController : Controller
    {        
        private readonly ICustomerHandler _customerHandler;

        public CustomersController(ICustomerHandler customerHandler)
        {           
            _customerHandler = customerHandler;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            var customers = await _customerHandler.GetAllActiveCustomers();
            return View(customers.Where(c => c.Status == Status.Active));
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _customerHandler.GetCustomer(id.Value);
               
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,LastName,FirstName,Email,PhoneNumber,Status")] Customer customer)
        {
            if (ModelState.IsValid)
            {
               await _customerHandler.CreateCustomer(customer);               
               return RedirectToAction(nameof(Index));
            }
            
           return BadRequest(ModelState);
           
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _customerHandler.GetCustomer(id.Value);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerId,LastName,FirstName,Email,PhoneNumber,Status")] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _customerHandler.UpdateCustomer(customer);                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CustomerExists(customer.CustomerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _customerHandler.GetCustomer(id.Value);               
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _customerHandler.UpdateCustomer(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CustomerExists(int id)
        {
            var customers = await _customerHandler.GetAllActiveCustomers();
            return customers.Any(e => e.CustomerId == id);
        }
    }
}
