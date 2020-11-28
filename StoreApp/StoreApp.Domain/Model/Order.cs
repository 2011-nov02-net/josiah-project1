using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Domain.Model
{
    public class Order
    {
        public Order()
        {
            Items = new Dictionary<Product, int>();
        }
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public Location Location { get; set; }
        public DateTime Time { get; set; }
        public IDictionary<Product, int> Items { get; set; }
    }
}
