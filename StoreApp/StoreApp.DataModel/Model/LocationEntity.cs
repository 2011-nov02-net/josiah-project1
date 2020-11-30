using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Data
{
    public class LocationEntity
    {
        public LocationEntity()
        {
            Orders = new List<OrderEntity>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<InventoryItemsEntity> Inventory { get; set; }
        public IEnumerable<OrderEntity> Orders { get; set; }

    }
}
