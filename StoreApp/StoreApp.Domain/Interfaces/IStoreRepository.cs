using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StoreApp.Domain.Model;

namespace StoreApp.Domain.Interfaces
{
    public interface IStoreRepository
    {
        public void AddCustomer(Customer customer);
        public Task AddCustomerAsync(Customer customer);
        public IEnumerable<Customer> GetAllCustomers();
        public Task<IEnumerable<Customer>> GetAllCustomersAsync();
        public bool CustomerExists(Customer customer);
        public Task<bool> CustomerExistsAsync(Customer customer);
        public IEnumerable<Customer> SearchCustomers(string search);
        public Task<IEnumerable<Customer>> SearchCustomersAsync(string search);
        public void AddLocation(Location location);
        public Task AddLocationAsync(Location location);
        public IEnumerable<Location> GetAllLocations();
        public Task<IEnumerable<Location>> GetAllLocationsAsync();
        public void AddInventoryToLocation(Location location, List<Product> items);
        public Task AddInventoryToLocationAsync(Location location, List<Product> items);
        public Location GetLocationdDetails(int id);
        public Task<Location> GetLocationDetailsAsync(int id);
        public void AddOrder(Order order);
        public Task AddOrderAsync(Order order);
        public IEnumerable<Order> GetAllOrders();
        public Task<IEnumerable<Order>> GetAllOrdersAsync();
        public IEnumerable<Order> GetOrdersByCustomer(Customer customer);
        public Task<IEnumerable<Order>> GetOrdersByCustomerAsync(Customer customer);
        public IEnumerable<Order> GetOrdersByLocation(Location location);
        public Task<IEnumerable<Order>> GetOrdersByLocationAsync(Location location);
        public void AddProduct(Product product);
        public Task AddProductAsync(Product product);
        public IEnumerable<Product> GetAllProducts();
        public Task<IEnumerable<Product>> GetAllProductsAsync();
    }
}
