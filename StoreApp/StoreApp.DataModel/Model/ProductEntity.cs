﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Data
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
    }
}