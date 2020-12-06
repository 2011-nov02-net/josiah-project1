using StoreApp.Domain.Model;
using StoreApp.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.WebApp.Helpers
{
    public class OrderHelper
    {
        public static Order ViewToOrder(PlaceOrderViewModel order)
        {
            var items = new Dictionary<Product, int>();
            for (int i = 0; i < order.CartItems.Count; i++)
            {
                items.Add(order.CartItems[i], order.CartAmounts[i]);
            }
            var result = new Order
            {
                Customer = order.Customer,
                Location = order.Location,
                Items = items
            };
            return result;
        }
    }
}
