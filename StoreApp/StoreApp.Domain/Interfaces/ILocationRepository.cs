using System;
using System.Collections.Generic;
using System.Text;
using StoreApp.Domain.Model;


namespace StoreApp.Domain.Interfaces
{
    public interface ILocationRepository
    {
        void AddLocation(Location location);
        IEnumerable<Location> GetAllLocations();
        void AddInventoryToLocation(Location location, List<Product> items);

    }
}
