using System;
using System.Collections.Generic;

namespace StoreApp.Data
{
    public class OrderEntity
    {
        public OrderEntity()
        {
            Items = new List<OrderItemsEntity>();
        }
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public CustomerEntity Customer { get; set; }
        public int LocationId { get; set; }
        public LocationEntity Location { get; set; }
        public DateTime Time { get; set; }
        public IEnumerable<OrderItemsEntity> Items { get; set; }
    }
}
