﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Data
{
    public class LocationEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductEntity> Inventory { get; set; }
    }
}