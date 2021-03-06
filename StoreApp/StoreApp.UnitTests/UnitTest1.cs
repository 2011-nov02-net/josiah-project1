using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using StoreApp.Data;
using StoreApp.Domain.Model;
using StoreApp.Domain.Repositories;
using System.Linq;
using Xunit;

namespace StoreApp.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void CustomerInstantiationTest()
        {
            var customer = new Customer
            {
                FirstName = "John",
                LastName = "Smith",
                Email = "jsmith@gmail.com"
            };

            Assert.True(customer.FirstName == "John" && customer.LastName == "Smith" && customer.Email == "jsmith@gmail.com");
        }
        [Fact]
        public void ProductInstantiationTest()
        {
            var product = new Product
            {
                Name = "Pop Rocks",
                Price = 3
            };
            Assert.True(product.Name == "Pop Rocks" && product.Price == 3);
        }

        [Fact]
        public void AddCustomer_Database_test()
        {
            // arrange
            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<StoreAppDbContext>().UseSqlite(connection).Options;
            var customer = new Customer
            {
                Id = 1,
                FirstName = "Jerry",
                LastName = "Smith",
                Email = "JSmith@yahoo.com"
            };

            // act
            using (var context = new StoreAppDbContext(options))
            {
                context.Database.EnsureCreated();
                var repo = new StoreRepository(context, new NullLogger<StoreRepository>());

                repo.AddCustomer(customer);
            }
            //assert
            using var context2 = new StoreAppDbContext(options);
            CustomerEntity customerActual = context2.Customers
                .Single(l => l.FirstName == "Jerry");
            Assert.Equal(customer.FirstName, customerActual.FirstName);
        }
        /*
        [Fact]
        public void AddOrder_Success_Database_test()
        {
            // arrange 
            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<StoreAppDbContext>().UseSqlite(connection).Options;
            var order = new Order
            {
                Id = 1,
                Location = new Location
                {
                    Id = 1,
                    Name = "Walmart",
                    Inventory = new Dictionary<Product, int>()
                    {
                        { new Product { Id = 1, Name = "a", Price = 1 }, 5 }
                    }
                },
                Customer = new Customer
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Smith",
                    Email = "jsmith@gmail.com"
                },
                Time = DateTime.Now
            };

            var location = new Location
            {
                Id = 1,
                Name = "Store"
            };
            var items = new Dictionary<Product, int>()
            {
                {new Product{Name = "a", Price = 1}, 5 }
            };

            // act
            using (var context = new StoreAppDbContext(options))
            {
                context.Database.EnsureCreated();
                var repo = new StoreRepository(context, new NullLogger<StoreRepository>());
                repo.AddLocation(location);
                repo.AddInventoryToLocation(location, items);

                repo.AddOrder(order);
            }
            using var context2 = new StoreAppDbContext(options);
            OrderEntity OrderActual = context2.Orders
                .Include(x => x.Items)
                .Include(x => x.Customer)
                .Single(l => l.Customer.FirstName == "John");
            Assert.Equal(OrderActual.Items.Count(), 1);
        }*/
        [Fact]
        public void AddLocation_Database_Test()
        {
            // arrange
            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<StoreAppDbContext>().UseSqlite(connection).Options;
            var location = new Location
            {
                Id = 1,
                Name = "Walmart"
            };

            // act
            using (var context = new StoreAppDbContext(options))
            {
                context.Database.EnsureCreated();
                var repo = new StoreRepository(context, new NullLogger<StoreRepository>());

                repo.AddLocation(location);
            }
            //assert
            using var context2 = new StoreAppDbContext(options);
            LocationEntity customerActual = context2.Locations
                .Single(l => l.Name == "Walmart");
            Assert.Equal(location.Name, customerActual.Name);
        }
        [Fact]
        public void AddProduct_Database_test()
        {
            // arrange
            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<StoreAppDbContext>().UseSqlite(connection).Options;
            var product = new Product
            {
                Id = 1,
                Name = "Walmart",
                Price = 5
            };

            // act
            using (var context = new StoreAppDbContext(options))
            {
                context.Database.EnsureCreated();
                var repo = new StoreRepository(context, new NullLogger<StoreRepository>());

                repo.AddProduct(product);
            }
            //assert
            using var context2 = new StoreAppDbContext(options);
            ProductEntity productActual = context2.Products
                .Single(l => l.Name == "Walmart");
            Assert.Equal(product.Name, productActual.Name);
        }
    }
}
