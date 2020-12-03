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
        public IEnumerable<Customer> SearchCustomers(string search);
        public Task<IEnumerable<Customer>> SearchCustomersAsync(string search);
        public Customer GetCustomerById(int id);
        public Task<Customer> GetCustomerByIdAsync(int id);
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
        public IEnumerable<Order> GetOrdersByCustomer(int id);
        public Task<IEnumerable<Order>> GetOrdersByCustomerAsync(int id);
        public IEnumerable<Order> GetOrdersByLocation(int id);
        public Task<IEnumerable<Order>> GetOrdersByLocationAsync(int id);
        public void AddProduct(Product product);
        public Task AddProductAsync(Product product);
        public IEnumerable<Product> GetAllProducts();
        public Task<IEnumerable<Product>> GetAllProductsAsync();
    }
}
