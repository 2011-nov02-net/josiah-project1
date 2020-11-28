﻿using StoreApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.WebApp.Models
{
    public class LocationViewModel
    {
        public string Name { get; set; }
        public IEnumerable<Product> Inventory { get; set; }
    }
}
