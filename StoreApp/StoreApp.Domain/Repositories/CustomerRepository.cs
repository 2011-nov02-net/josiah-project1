using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using StoreApp.Data;
using StoreApp.Domain.Interfaces;
using StoreApp.Domain.Model;
using System.Linq;

namespace StoreApp.Domain.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly StoreAppDbContext _context;

        private readonly ILogger<CustomerRepository> _logger;

        public CustomerRepository(StoreAppDbContext context, ILogger<CustomerRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException();
            _logger = logger ?? throw new ArgumentNullException();
        }

        public void addCustomer(Customer customer)
        {
            var new_customer = new CustomerEntity
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email
            };

            _context.Customers.Add(new_customer);
            _context.SaveChanges();
        }

        public bool CustomerExists(Customer customer)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            var customers = _context.Customers.ToList().Select(x => new Customer
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email
            });

            return customers;
        }
    }
}
