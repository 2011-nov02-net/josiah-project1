﻿using System.Collections.Generic;

namespace StoreApp.Data
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<InventoryItemsEntity> InventoryItems { get; set; }
        public IEnumerable<OrderItemsEntity> OrderItems { get; set; }
    }
}
