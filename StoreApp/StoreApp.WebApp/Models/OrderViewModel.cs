using StoreApp.Domain.Model;
using System;
using System.Collections.Generic;

namespace StoreApp.WebApp.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public Location Location { get; set; }
        public DateTime Time { get; set; }
        public IDictionary<Product, int> Items { get; set; }
        public double Price { get; set; }

        public static double getPrice(IDictionary<Product, int> items)
        {
            double total = 0;
            foreach (var x in items)
            {
                total += (double)x.Key.Price * x.Value;
            }
            return total;
        }
    }
}