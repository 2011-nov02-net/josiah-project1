using Microsoft.Extensions.Logging;
using StoreApp.Data;
using StoreApp.Domain.Interfaces;
using StoreApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace StoreApp.Domain.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly StoreAppDbContext _context;
        private readonly ILogger<StoreRepository> _logger;
        public StoreRepository(StoreAppDbContext context, ILogger<StoreRepository> logger)
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
        public IEnumerable<Customer> SearchCustomers(string search)
        {
            var customers = _context.Customers.ToList()
                .Where(x => x.FirstName.Contains(search) || x.LastName.Contains(search))
                .Select(y => new Customer
                {
                    Id = y.Id,
                    FirstName = y.FirstName,
                    LastName = y.LastName,
                    Email = y.Email
                });
            return customers;
        }
        public void AddInventoryToLocation(Location location, List<Product> items)
        {
            throw new NotImplementedException();
        }
        public void AddLocation(Location location)
        {
            var new_location = new LocationEntity
            {
                Name = location.Name
            };

            _context.Locations.Add(new_location);
            _context.SaveChanges();
        }
        public IEnumerable<Location> GetAllLocations()
        {
            var locations = _context.Locations.Include(i => i.Inventory).ToList().Select(x => new Location
            {
                Id = x.Id,
                Name = x.Name
            });

            return locations;
        }
        public Location GetLocation(int id)
        {
            var location = _context.Locations
                .Include(x => x.Inventory)
                .ThenInclude(x => x.Product)
                .Where(x => x.Id == id).First();
            var result = new Location
            {
                Name = location.Name
            };

            foreach (var item in location.Inventory)
            {
                result.Inventory.Add(new Product { Name = item.Product.Name, Price = item.Product.Price }, item.Amount);
            }
            return result;
        }
        public void AddOrder(Order order)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Order> GetAllOrders()
        {
            List<Order> result = new List<Order>();
            var orders = _context.Orders;

            foreach (var order in orders)
            {
                var orderItems = _context.OrderItems
                    .Include(x => x.Product)
                    .Include(x => x.Amount)
                    .Where(x => x.OrderId == order.Id).ToList();

                var temp_order = new Order
                {
                    Customer = new Customer { FirstName = order.Customer.FirstName, LastName = order.Customer.LastName, Id = order.CustomerId },
                    Location = new Location { Name = order.Location.Name },
                    Time = order.Time,
                    Id = order.Id,
                };
                foreach (var item in orderItems)
                {
                    temp_order.Items.Add(new Product { Name = item.Product.Name, Price = item.Product.Price, Id = item.Product.Id }, item.Amount);
                }

                result.Add(temp_order);
            }

            return result;

        }
        public IEnumerable<Order> GetOrdersByCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Order> GetOrdersByLocation(Location location)
        {
            throw new NotImplementedException();
        }
        public void AddProduct(Product product)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Product> GetAllProducts()
        {
            throw new NotImplementedException();
        }
    }
}
