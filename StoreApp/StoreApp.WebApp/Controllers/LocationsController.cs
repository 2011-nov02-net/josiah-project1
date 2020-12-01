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
        public IActionResult Index()
        {
            var customers = repo.GetAllLocations().Select(x => new LocationViewModel
            {
                Id = x.Id,
                Name = x.Name
            }); ;

            return View(customers);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(LocationViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var location = new Location
                    {
                        Name = viewModel.Name
                    };
                    repo.AddLocation(location);

                    return RedirectToAction(nameof(Index));
                }
                return View(viewModel);
            }
            catch
            {
                return View(viewModel);
            }
        }

        public IActionResult Details(int id)
        {
            var location = repo.GetLocation(id);

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
