using StoreApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.WebApp.Models
{
    public class PlaceOrderViewModel
    {
        public PlaceOrderViewModel()
        {
            New = true;
            Cart = new Dictionary<Product, int>();
        }
        public bool New { get; set; }
        [Range(0, 99)]
        public int chosenProductAmount { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
        public int chosenCustomerId { get; set; }
        public IEnumerable<Location> Locations { get; set; }
        public int chosenLocationId { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public int chosenProductId { get; set; }
        public IDictionary<Product, int> Inventory { get; set; }
        public IDictionary<Product, int> Cart { get; set; }
    }
}
