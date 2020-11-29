using StoreApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        public void addCustomer(Customer customer);
        public IEnumerable<Customer> GetAllCustomers();
        public bool CustomerExists(Customer customer);
        public IEnumerable<Customer> SearchCustomers(string search);
    }
}
