using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Data
{
    public class InventoryItemsEntity
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public LocationEntity Location { get; set; }
        public ProductEntity Product { get; set; }

    }
}
