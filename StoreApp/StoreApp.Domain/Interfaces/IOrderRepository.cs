using System;
using System.Collections.Generic;
using System.Text;
using StoreApp.Domain.Model;


namespace StoreApp.Domain.Interfaces
{
    public interface IOrderRepository
    {
        public void AddOrder(Order order);
        public List<Order> GetAllOrders();
        public List<Order> GetOrdersByCustomer(Customer customer);
        public List<Order> GetOrdersByLocation(Location location);
    }
}
