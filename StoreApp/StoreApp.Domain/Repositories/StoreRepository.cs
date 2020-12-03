using Microsoft.Extensions.Logging;
using StoreApp.Data;
using StoreApp.Domain.Interfaces;
using StoreApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
        public void AddCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
        public async Task AddCustomerAsync(Customer customer)
        {
            var new_customer = new CustomerEntity
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email
            };

            await _context.Customers.AddAsync(new_customer);
            _context.SaveChanges();
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
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            var data = await _context.Customers.ToListAsync();
            var customers = data.Select(x => new Customer
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
        public async Task<IEnumerable<Customer>> SearchCustomersAsync(string search)
        {
            var data = await _context.Customers.ToListAsync();
            var customers = data
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
        public void AddLocation(Location location)
        {
            var new_location = new LocationEntity
            {
                Name = location.Name
            };

            _context.Locations.Add(new_location);
            _context.SaveChanges();
        }
        public async Task AddLocationAsync(Location location)
        {
            var new_location = new LocationEntity
            {
                Name = location.Name
            };

            await _context.Locations.AddAsync(new_location);
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
        public async Task<IEnumerable<Location>> GetAllLocationsAsync()
        {
            var data = await _context.Locations.Include(i => i.Inventory).ToListAsync();
            var locations = data.Select(x => new Location
            {
                Id = x.Id,
                Name = x.Name
            });
            return locations;
        }
        public void AddInventoryToLocation(Location location, List<Product> items)
        {
            throw new NotImplementedException();
        } //TODO
        public async Task AddInventoryToLocationAsync(Location location, List<Product> items)
        {
            throw new NotImplementedException();
        } //TODO
        public Location GetLocationdDetails(int id)
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
        public async Task<Location> GetLocationDetailsAsync(int id)
        {
            var location = await _context.Locations
                .Include(x => x.Inventory)
                .ThenInclude(x => x.Product)
                .Where(x => x.Id == id).FirstAsync();

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
        } //TODO
        public Task AddOrderAsync(Order order)
        {
            throw new NotImplementedException();
        } //TODO
        public IEnumerable<Order> GetAllOrders()
        {
            var orders = _context.Orders;
            return GetOrdersHelper(orders);
        }
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            var orders = _context.Orders;
            return await GetOrdersHelperAsync(orders);
        }
        public IEnumerable<Order> GetOrdersByCustomer(int id)
        {
            var orders = _context.Orders
                .Include(x => x.Location)
                .Include(x => x.Customer)
                .Include(x => x.Items).ThenInclude(x => x.Product)
                .Where(x => x.CustomerId == id).ToList();
            return GetOrdersHelper(orders);
        }
        public async Task<IEnumerable<Order>> GetOrdersByCustomerAsync(int id)
        {
            var orders = await _context.Orders
                .Include(x => x.Location)
                .Include(x => x.Customer)
                .Include(x => x.Items).ThenInclude(x => x.Product)
                .Where(x => x.CustomerId == id).ToListAsync();

            return GetOrdersHelper(orders);
        }
        public IEnumerable<Order> GetOrdersByLocation(int id)
        {
            var orders = _context.Orders
                .Include(x => x.Location)
                .Include(x => x.Customer)
                .Include(x => x.Items).ThenInclude(x => x.Product)
                .Where(x => x.LocationId == id).ToList();
            return GetOrdersHelper(orders);
        }
        public async Task<IEnumerable<Order>> GetOrdersByLocationAsync(int id)
        {
            var orders = await _context.Orders
                .Include(x => x.Location)
                .Include(x => x.Customer)
                .Include(x => x.Items).ThenInclude(x => x.Product)
                .Where(x => x.LocationId == id).ToListAsync();
            return GetOrdersHelper(orders);
        }
        public IEnumerable<Order> GetOrdersHelper(IEnumerable<OrderEntity> orders)
        {
            List<Order> result = new List<Order>();
            foreach (var order in orders)
            {

                var temp_order = new Order
                {
                    Customer = new Customer { FirstName = order.Customer.FirstName, LastName = order.Customer.LastName, Id = order.CustomerId },
                    Location = new Location { Name = order.Location.Name },
                    Time = order.Time,
                    Id = order.Id,
                };
                foreach (var item in order.Items)
                {
                    temp_order.Items.Add(new Product { Name = item.Product.Name, Price = item.Product.Price, Id = item.Product.Id }, item.Amount);
                }

                result.Add(temp_order);
            }
            return result;
        }
        public async Task<IEnumerable<Order>> GetOrdersHelperAsync(IQueryable<OrderEntity> orders)
        {
            List<Order> result = new List<Order>();
            foreach (var order in orders)
            {
                var orderItems = await _context.OrderItems
                    .Include(x => x.Product)
                    .Include(x => x.Amount)
                    .Where(x => x.OrderId == order.Id).ToListAsync();

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
        public void AddProduct(Product product)
        {
            var new_product = new ProductEntity
            {
                Name = product.Name,
                Price = product.Price
            };

            _context.Products.Add(new_product);
            _context.SaveChanges();
        }
        public async Task AddProductAsync(Product product)
        {
            var new_product = new ProductEntity
            {
                Name = product.Name,
                Price = product.Price
            };

            await _context.Products.AddAsync(new_product);
            _context.SaveChanges();
        }
        public IEnumerable<Product> GetAllProducts()
        {
            var products = _context.Products.ToList().Select(x => new Product
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price
            });
            return products;
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var data = await _context.Products.ToListAsync();
            var products = data.Select(x => new Product
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price
            });
            return products;
        }
    }
}
