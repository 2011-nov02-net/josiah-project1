﻿using StoreApp.Domain.Model;
using StoreApp.WebApp.Models;
using System.Collections.Generic;

namespace StoreApp.WebApp.Helpers
{
    /// <summary>
    /// simple helper to convert a viewmodel to order 
    /// </summary>
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
