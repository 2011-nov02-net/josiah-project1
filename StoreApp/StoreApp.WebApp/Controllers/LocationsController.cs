using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StoreApp.Domain.Interfaces;
using StoreApp.Domain.Model;
using StoreApp.WebApp.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.WebApp.Controllers
{
    public class LocationsController : Controller
    {
        private IStoreRepository repo { get; }
        private readonly ILogger<LocationsController> logger;

        public LocationsController(IStoreRepository Repo, ILogger<LocationsController> Logger)
        {
            repo = Repo ?? throw new ArgumentNullException(nameof(repo));
            logger = Logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// lists all locations along with a link to view their orders and inventories
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var data = await repo.GetAllLocationsAsync();
            var locations = data.Select(x => new LocationViewModel
            {
                Id = x.Id,
                Name = x.Name
            }); ;
            logger.LogInformation("Displaying all locations");
            return View(locations);
        }
        /// <summary>
        /// not implemented
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// not implemented
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LocationViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var location = new Location
                    {
                        Name = viewModel.Name
                    };
                    await repo.AddLocationAsync(location);

                    return RedirectToAction(nameof(Index));
                }
                return View(viewModel);
            }
            catch
            {
                return View(viewModel);
            }
        }
        /// <summary>
        /// return a view containing all of the orders of a given location id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Orders(int id)
        {
            var location = await repo.GetLocationDetailsAsync(id);

            var orders = await repo.GetOrdersByLocationAsync(id);

            var viewLocation = new LocationViewModel
            {
                Name = location.Name,
                Id = location.Id,
                Inventory = location.Inventory
            };

            var viewOrders = orders.Select(x => new OrderViewModel
            {
                Id = x.Id,
                Customer = x.Customer,
                Location = x.Location,
                Time = x.Time,
                Items = x.Items,
                Price = OrderViewModel.getPrice(x.Items)
            }).ToList();

            ViewData["Orders"] = viewOrders;

            logger.LogInformation($"Displaying all orders at {location.Name}");

            return View(viewLocation);
        }

        /// <summary>
        /// returns a view containing the inventory items and amount for each location
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Inventory(int id)
        {
            var location = await repo.GetLocationDetailsAsync(id);

            var viewLocation = new LocationViewModel
            {
                Name = location.Name,
                Id = location.Id,
                Inventory = location.Inventory
            };

            logger.LogInformation($"Displaying inventory for {location.Name}");

            return View(viewLocation);
        }
    }
}
