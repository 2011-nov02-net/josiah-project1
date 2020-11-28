using System;
using System.Collections.Generic;
using System.Text;
using StoreApp.Domain.Model;


namespace StoreApp.Domain.Interfaces
{
    public interface IProductRepository
    {
        public void AddProduct(Product product);
        public IEnumerable<Product> GetAllProducts();

    }
}
