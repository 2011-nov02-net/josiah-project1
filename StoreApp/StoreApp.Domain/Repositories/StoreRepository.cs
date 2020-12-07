using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoreApp.Data;
using StoreApp.Domain.Interfaces;
using StoreApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.Domain.Repositories
{
    /// <summary>
    /// Derived class for store repoistory that takes business logic classes and converts them to database classes
    /// Every method has a synchronous and an asynchronous version
    /// </summary>
    public class StoreRepository : IStoreRepository
    {
        private readonly StoreAppDbContext _context;
        private readonly ILogger<StoreRepository> _logger;
        public StoreRepository(StoreAppDbContext context, ILogger<StoreRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException();
            _logger = logger ?? throw new ArgumentNullException();
        }
        /// <summary>
        /// Adds a customer to the database
        /// </summary>
        /// <param name="customer"></param>
        public void AddCustomer(Customer customer)
        {
            var new_customer = new CustomerEntity
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email
            };
            _context.Customers.Add(new_customer);
            _context.SaveChanges();
            _logger.LogInformation($"Added Customer {customer.FirstName} {customer.LastName} to the database");
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
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Added Customer {customer.FirstName} {customer.LastName} to the database");

        }
        /// <summary>
        /// returns a list of all customers
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Customer> GetAllCustomers()
        {
            var customers = _context.Customers.Select(x => new Customer
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email
            }).ToList();
            _logger.LogInformation("Retrieved list of all customers");
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
            _logger.LogInformation("Retrieved list of all customers");
            return customers;
        }
        /// <summary>
        /// returns a list of all customers that match the given search string (first or last name)
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
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
            _logger.LogInformation($"Returned {customers.Count()} customer(s) from search");
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
            _logger.LogInformation($"Returned {customers.Count()} customer(s) from search");
            return customers;
        }
        /// <summary>
        /// returns the customer that matches the id given
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer GetCustomerById(int id)
        {
            var data = _context.Customers.Where(x => x.Id == id).First();
            var customer = new Customer
            {
                Id = data.Id,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Email = data.Email
            };
            _logger.LogInformation($"returned customer for ID#: {id}");
            return customer;
        }
        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            var data = await _context.Customers.Where(x => x.Id == id).FirstAsync();
            var customer = new Customer
            {
                Id = data.Id,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Email = data.Email
            };
            _logger.LogInformation($"returned customer for ID#: {id}");
            return customer;
        }
        /// <summary>
        /// adds a location to the database
        /// </summary>
        /// <param name="location"></param>
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
        /// <summary>
        /// returns a list of all locations with inventories
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Location> GetAllLocations()
        {
            var locations = _context.Locations.Include(i => i.Inventory).ThenInclude(i => i.Product).ToList().Select(x => new Location
            {
                Id = x.Id,
                Name = x.Name
            });
            _logger.LogInformation("Returned all locations from database");
            return locations;
        }
        public async Task<IEnumerable<Location>> GetAllLocationsAsync()
        {
            var data = await _context.Locations.Include(i => i.Inventory).ThenInclude(i => i.Product).ToListAsync();
            var locations = data.Select(x => new Location
            {
                Id = x.Id,
                Name = x.Name
            });
            _logger.LogInformation("Returned all locations from database");
            return locations;
        }
        /// <summary>
        /// takes a dictionary of items and adds it to the location with the name specified
        /// </summary>
        /// <param name="l"></param>
        /// <param name="items"></param>
        public void AddInventoryToLocation(Location l, Dictionary<Product, int> items)
        {
            var location = _context.Locations
                    .Include(x => x.Inventory)
                    .ThenInclude(x => x.Product)
                    .Where(x => x.Id == l.Id).First();
            var products = _context.Products.ToList();

            foreach (var item in items.Keys)
            {
                if (!products.Any(x => x.Name == item.Name))
                {
                    AddProduct(item);
                }
                if (!location.Inventory.Any(x => x.ProductId == item.Id))
                {
                    // add new item with amount
                    var result = new List<InventoryItemsEntity>(location.Inventory);
                    result.Add(new InventoryItemsEntity { ProductId = item.Id, LocationId = location.Id, Amount = items[item] });
                    location.Inventory = result;
                }
                else
                {
                    // increase amount of item
                    location.Inventory.Where(x => x.ProductId == item.Id).First().Amount += items[item];
                }
            }
            _context.SaveChanges();
        }
        public async Task AddInventoryToLocationAsync(Location l, Dictionary<Product, int> items)
        {
            var location = await _context.Locations
                .Include(x => x.Inventory)
                .ThenInclude(x => x.Product)
                .Where(x => x.Id == l.Id).FirstAsync();

            var products = await _context.Products.ToListAsync();

            foreach (var item in items.Keys)
            {
                if (!products.Any(x => x.Name == item.Name))
                {
                    await AddProductAsync(item);
                }
                if (!location.Inventory.Any(x => x.ProductId == item.Id))
                {
                    // add new item with amount
                    var result = (List<InventoryItemsEntity>)location.Inventory;
                    result.Add(new InventoryItemsEntity { ProductId = item.Id, LocationId = location.Id, Amount = items[item] });
                    location.Inventory = result;
                }
                else
                {
                    // increase amount of item
                    location.Inventory.Where(x => x.ProductId == item.Id).First().Amount += items[item];
                }
            }
            await _context.SaveChangesAsync();

        } //TODO
        /// <summary>
        /// gets location inventory for a specific location id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Location GetLocationdDetails(int id)
        {
            var location = _context.Locations
                .Include(x => x.Inventory)
                .ThenInclude(x => x.Product)
                .Where(x => x.Id == id).First();
            var result = new Location
            {
                Id = location.Id,
                Name = location.Name
            };

            foreach (var item in location.Inventory)
            {
                result.Inventory.Add(new Product { Name = item.Product.Name, Price = item.Product.Price }, item.Amount);
            }
            _logger.LogInformation($"Returned location details for location ID#: {id}");
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
            _logger.LogInformation($"Returned location details for location ID#: {id}");
            return result;
        }
        /// <summary>
        /// adds an order to the database, by adding the order, order items, and decrementing the
        /// appropriate location inventory
        /// </summary>
        /// <param name="order"></param>
        public void AddOrder(Order order)
        {
            var new_order = new OrderEntity
            {
                Time = DateTime.Now,
                CustomerId = order.Customer.Id,
                LocationId = order.Location.Id
            };
            new_order = OrderItemsToEntity(order.Items, new_order);

            // check if location inventories are adequate

            var location = _context.Locations
                .Include(x => x.Inventory)
                .ThenInclude(x => x.Product)
                .Where(x => x.Id == order.Location.Id).First();

            foreach (var item in new_order.Items)
            {
                if (!(location.Inventory.Any(x => x.ProductId == item.ProductId)))
                {
                    throw new ApplicationException($"Location does not contain Product ID: {item.ProductId}");
                }

                var stock = location.Inventory.Where(x => x.Product.Id == item.ProductId).First().Amount;

                if (stock < item.Amount)
                {
                    throw new ApplicationException($"Insufficient inventory to complete purchase - Product ID: {item.ProductId}");
                }
                else
                {
                    location.Inventory.Where(x => x.Product.Id == item.ProductId).First().Amount -= item.Amount;
                }
            }

            _context.Orders.Add(new_order);

            _context.SaveChanges();
            _logger.LogInformation($"Added order for {order.Customer.FirstName} {order.Customer.LastName} at {order.Location.Name} " +
                $"for {order.Items.Count()} item(s)");
        }
        public async Task AddOrderAsync(Order order)
        {
            var new_order = new OrderEntity
            {
                Time = DateTime.Now,
                CustomerId = order.Customer.Id,
                LocationId = order.Location.Id
            };
            new_order = OrderItemsToEntity(order.Items, new_order);

            // check if location inventories are adequate

            var location = await _context.Locations
                .Include(x => x.Inventory)
                .ThenInclude(x => x.Product)
                .Where(x => x.Id == order.Location.Id).FirstAsync();

            foreach (var item in new_order.Items)
            {
                if (!(location.Inventory.Any(x => x.ProductId == item.ProductId)))
                {
                    throw new ApplicationException($"Location does not contain Product ID: {item.ProductId}");
                }

                var stock = location.Inventory.Where(x => x.Product.Id == item.ProductId).First().Amount;

                if (stock < item.Amount)
                {
                    throw new ApplicationException($"Insufficient inventory to complete purchase - Product ID: {item.ProductId}");
                }
                else
                {
                    location.Inventory.Where(x => x.Product.Id == item.ProductId).First().Amount -= item.Amount;
                }
            }

            await _context.Orders.AddAsync(new_order);

            await _context.SaveChangesAsync();
            _logger.LogInformation($"Added order for {order.Customer.FirstName} {order.Customer.LastName} at {order.Location.Name} " +
                  $"for {order.Items.Count()} item(s)");
        }
        /// <summary>
        /// returns a list of all orders in the database
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Order> GetAllOrders()
        {
            var orders = _context.Orders
                .Include(x => x.Location)
                .Include(x => x.Customer)
                .Include(x => x.Items).ThenInclude(x => x.Product).ToList();
            _logger.LogInformation("returned list of all orders from database");
            return GetOrdersHelper(orders);
        }
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders
                .Include(x => x.Location)
                .Include(x => x.Customer)
                .Include(x => x.Items).ThenInclude(x => x.Product).ToListAsync();
            _logger.LogInformation("returned list of all orders from database");
            return GetOrdersHelper(orders);
        }
        /// <summary>
        /// returns a list of all orders by the customer id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Order> GetOrdersByCustomer(int id)
        {
            var orders = _context.Orders
                .Include(x => x.Location)
                .Include(x => x.Customer)
                .Include(x => x.Items).ThenInclude(x => x.Product)
                .Where(x => x.CustomerId == id).ToList();
            _logger.LogInformation($"Retrieved all orders for customer ID#: {id}");
            return GetOrdersHelper(orders);
        }
        public async Task<IEnumerable<Order>> GetOrdersByCustomerAsync(int id)
        {
            var orders = await _context.Orders
                .Include(x => x.Location)
                .Include(x => x.Customer)
                .Include(x => x.Items).ThenInclude(x => x.Product)
                .Where(x => x.CustomerId == id).ToListAsync();
            _logger.LogInformation($"Retrieved all orders for customer ID#: {id}");
            return GetOrdersHelper(orders);
        }
        /// <summary>
        /// returns a list of all orders at the location id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Order> GetOrdersByLocation(int id)
        {
            var orders = _context.Orders
                .Include(x => x.Location)
                .Include(x => x.Customer)
                .Include(x => x.Items).ThenInclude(x => x.Product)
                .Where(x => x.LocationId == id).ToList();
            _logger.LogInformation($"Retrieved all orders for location ID#: {id}");
            return GetOrdersHelper(orders);
        }
        public async Task<IEnumerable<Order>> GetOrdersByLocationAsync(int id)
        {
            var orders = await _context.Orders
                .Include(x => x.Location)
                .Include(x => x.Customer)
                .Include(x => x.Items).ThenInclude(x => x.Product)
                .Where(x => x.LocationId == id).ToListAsync();
            _logger.LogInformation($"Retrieved all orders for location ID#: {id}");
            return GetOrdersHelper(orders);
        }
        /// <summary>
        /// helper method for constructor orders
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
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
        /// <summary>
        /// adds a product to the database
        /// </summary>
        /// <param name="product"></param>
        public void AddProduct(Product product)
        {
            var new_product = new ProductEntity
            {
                Name = product.Name,
                Price = product.Price
            };
            _logger.LogInformation($"Added product {product.Name} to database");
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
            _logger.LogInformation($"Added product {product.Name} to database");
            await _context.Products.AddAsync(new_product);
            _context.SaveChanges();
        }
        /// <summary>
        /// return a list of all products in the database
        /// </summary>
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
        /// <summary>
        /// helper methods for converting to database objects
        /// </summary>
        public LocationEntity LocationToEntity(Location location)
        {
            var result = new LocationEntity
            {
                Name = location.Name,
                Id = location.Id
            };
            return result;
        }
        public CustomerEntity CustomerToEntity(Customer customer)
        {
            var result = new CustomerEntity
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Id = customer.Id
            };
            return result;
        }
        public ProductEntity ProductToEntity(Product product)
        {
            var result = new ProductEntity
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
            };
            return result;
        }
        public OrderEntity OrderItemsToEntity(IDictionary<Product, int> items, OrderEntity order)
        {
            List<OrderItemsEntity> result = new List<OrderItemsEntity>();
            foreach (var item in items.Keys)
            {
                result.Add(new OrderItemsEntity
                {
                    ProductId = item.Id,
                    Order = order,
                    Amount = items[item]
                });
            }
            order.Items = result;
            return order;
        }
    }
}
