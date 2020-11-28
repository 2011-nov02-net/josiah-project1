﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Domain.Model
{
    public class Location
    {
        public Location()
        {
            Inventory = new Dictionary<Product, int>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public IDictionary<Product, int> Inventory { get; set; }
    }
}
