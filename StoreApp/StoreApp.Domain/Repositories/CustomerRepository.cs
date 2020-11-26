using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using StoreApp.Data;
using StoreApp.Domain.Interfaces;
using StoreApp.Domain.Model;

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

        public List<Customer> GetAllCustomers()
        {
            throw new NotImplementedException();
        }
    }
}
