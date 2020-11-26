using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using StoreApp.Data;
using StoreApp.Domain.Interfaces;
using StoreApp.Domain.Model;

namespace StoreApp.Domain.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StoreAppDbContext _context;

        private readonly ILogger<OrderRepository> _logger;
        public OrderRepository(StoreAppDbContext context, ILogger<OrderRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException();
            _logger = logger ?? throw new ArgumentNullException();
        }
        public void AddOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public List<Order> GetOrdersByCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetOrdersByLocation(Location location)
        {
            throw new NotImplementedException();
        }
    }
}
