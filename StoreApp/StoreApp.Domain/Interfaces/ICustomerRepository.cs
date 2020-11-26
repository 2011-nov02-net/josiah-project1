using StoreApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Domain.Interfaces
{
    interface ICustomerRepository
    {
        public void addCustomer(Customer customer);
        public List<Customer> GetAllCustomers();
        public bool CustomerExists(Customer customer);
    }
}
