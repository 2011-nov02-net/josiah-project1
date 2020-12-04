﻿using StoreApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.WebApp.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public Location Location { get; set; }
        public DateTime Time { get; set; }
        public double Price { get; set; }
        public IDictionary<Product, int> Items { get; set; }
        
    }
}
