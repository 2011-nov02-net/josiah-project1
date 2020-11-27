﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Domain.Model
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Product> Inventory { get; set; }
    }
}
