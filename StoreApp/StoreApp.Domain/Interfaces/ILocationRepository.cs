using System;
using System.Collections.Generic;
using System.Text;
using StoreApp.Domain.Model;


namespace StoreApp.Domain.Interfaces
{
    public interface ILocationRepository
    {
        void AddLocation(Location location);
        List<Location> GetAllLocations();
        void AddInventoryToLocation(Location location, List<Product> items);

    }
}
