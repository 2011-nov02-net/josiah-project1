using System;
using System.Collections.Generic;
using System.Text;
using StoreApp.Domain.Model;


namespace StoreApp.Domain.Interfaces
{
    public interface IOrderRepository
    {
        public void AddOrder(Order order);
        public IEnumerable<Order> GetAllOrders();
        public IEnumerable<Order> GetOrdersByCustomer(Customer customer);
        public IEnumerable<Order> GetOrdersByLocation(Location location);
    }
}
