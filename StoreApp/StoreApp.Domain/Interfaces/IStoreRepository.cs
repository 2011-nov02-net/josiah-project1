using System;
using System.Collections.Generic;
using System.Text;
using StoreApp.Domain.Model;

namespace StoreApp.Domain.Interfaces
{
    public interface IStoreRepository
    {
        public void addCustomer(Customer customer);
        public IEnumerable<Customer> GetAllCustomers();
        public bool CustomerExists(Customer customer);
        public IEnumerable<Customer> SearchCustomers(string search);

        void AddLocation(Location location);
        IEnumerable<Location> GetAllLocations();
        void AddInventoryToLocation(Location location, List<Product> items);
        Location GetLocation(int id);

        public void AddOrder(Order order);
        public IEnumerable<Order> GetAllOrders();
        public IEnumerable<Order> GetOrdersByCustomer(Customer customer);
        public IEnumerable<Order> GetOrdersByLocation(Location location);

        public void AddProduct(Product product);
        public IEnumerable<Product> GetAllProducts();
    }
}
