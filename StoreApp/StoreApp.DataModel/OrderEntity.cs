using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Data
{
    public class OrderEntity
    {
        public int Id { get; set; }
        public CustomerEntity Customer { get; set; }
        public LocationEntity Location { get; set; }
        public DateTime Time { get; set; }
        public List<ProductEntity> Items { get; set; }
    }
}
