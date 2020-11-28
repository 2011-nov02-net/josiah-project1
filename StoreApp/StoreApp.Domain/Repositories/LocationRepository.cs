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
            var locations = _context.Locations.ToList().Select(x => new Location
            {
                Name = x.Name
            });

            return locations;
        }
    }
}
