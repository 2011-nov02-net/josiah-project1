using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Data
{
    public class OrderItemsEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public OrderEntity Order { get; set; }
        public ProductEntity Product { get; set; }
    }
}
