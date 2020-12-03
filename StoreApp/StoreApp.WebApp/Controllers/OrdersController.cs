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
    public class OrdersController : Controller
    {
        private IStoreRepository repo { get; }
        public OrdersController(IStoreRepository Repo)
        {
            repo = Repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public async Task<IActionResult> Index()
        {
            var data = await repo.GetAllOrdersAsync();
            var orders = data.Select(x => new OrderViewModel
            {
                Customer = x.Customer,
                Location = x.Location,
                Time = x.Time,
                Items = x.Items
            });
            return View(orders);
        }
        public async Task<IActionResult> Create()
        {
            var custData = await repo.GetAllCustomersAsync();
            var customers = custData.Select(x => new CustomerViewModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email
            }).ToList();

            var locData = await repo.GetAllLocationsAsync();
            var locations = locData.Select(x => new LocationViewModel
            {
                Name = x.Name
            }).ToList();

            var prodData = await repo.GetAllProductsAsync();
            var products = prodData.Select(x => new ProductViewModel
            {
                Name = x.Name,
                Price = x.Price
            }).ToList();

            ViewData["Customers"] = customers;
            ViewData["Locations"] = locations;
            ViewData["Products"] = products;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(OrderViewModel viewModel)
        {
            return View();
        }
    }
}
