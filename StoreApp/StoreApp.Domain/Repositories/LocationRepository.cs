using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StoreApp.Data;
using StoreApp.Domain.Interfaces;
using StoreApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreApp.Domain.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly StoreAppDbContext _context;

        private readonly ILogger<LocationRepository> _logger;
        public LocationRepository(StoreAppDbContext context, ILogger<LocationRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException();
            _logger = logger ?? throw new ArgumentNullException();
        }

        public void AddInventoryToLocation(Location location, List<Product> items)
        {
            throw new NotImplementedException();
        }

        public void AddLocation(Location location)
        {
            var new_location = new LocationEntity
            {
                Name = location.Name
            };

            _context.Locations.Add(new_location);
            _context.SaveChanges();
        }

        public IEnumerable<Location> GetAllLocations()
        {
            var locations = _context.Locations.Include(i => i.Inventory).ToList().Select(x => new Location
            {
                Id = x.Id,
                Name = x.Name
            });

            return locations;
        }
        public Location GetLocation(int id)
        {
            var location = _context.Locations
                .Include(x => x.Inventory)
                .ThenInclude(x => x.Product)
                .Where(x => x.Id == id).First();
            var result = new Location
            {
                Name = location.Name
            };

            foreach (var item in location.Inventory)
            {
                result.Inventory.Add(new Product { Name = item.Product.Name, Price = item.Product.Price }, item.Amount);
            }
            return result;
        }
    }
}
