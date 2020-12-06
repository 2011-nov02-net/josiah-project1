using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Domain.Interfaces;
using StoreApp.Domain.Model;
using StoreApp.WebApp.Models;

namespace StoreApp.WebApp.Controllers
{
    public class LocationsController : Controller
    {
        private IStoreRepository repo { get; }
        public LocationsController(IStoreRepository Repo) =>
            repo = Repo ?? throw new ArgumentNullException(nameof(repo));
        public async Task<IActionResult> Index()
        {
            var data = await repo.GetAllLocationsAsync();
            var locations = data.Select(x => new LocationViewModel
            {
                Id = x.Id,
                Name = x.Name
            }); ;

            return View(locations);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
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

            return View(viewLocation);
        }
        public async Task<IActionResult> Inventory(int id)
        {
            var location = await repo.GetLocationDetailsAsync(id);

            var viewLocation = new LocationViewModel
            {
                Name = location.Name,
                Id = location.Id,
                Inventory = location.Inventory
            };

            return View(viewLocation);
        }
    }
}
