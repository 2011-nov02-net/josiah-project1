using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using StoreApp.Domain.Interfaces;
using StoreApp.Domain.Model;
using StoreApp.Data;

namespace StoreApp.Domain.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreAppDbContext _context;

        private readonly ILogger<ProductRepository> _logger;
        public ProductRepository(StoreAppDbContext context, ILogger<ProductRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException();
            _logger = logger ?? throw new ArgumentNullException();
        }
        public void AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public void GetAllProducts()
        {
            throw new NotImplementedException();
        }
    }
}
