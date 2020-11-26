using System;
using System.Collections.Generic;
using System.Text;
using StoreApp.Domain.Model;


namespace StoreApp.Domain.Interfaces
{
    interface ILocationRepository
    {
        public void AddLocation(Location location);
        public List<Location> GetAllLocations();
        public void AddInventoryToLocation(Location location, List<Product> items);

    }
}
