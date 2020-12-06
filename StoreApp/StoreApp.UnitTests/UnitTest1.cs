using StoreApp.Domain.Model;
using StoreApp.WebApp;
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



    }
}
