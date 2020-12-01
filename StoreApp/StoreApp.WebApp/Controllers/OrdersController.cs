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

        public IActionResult Index()
        {
            var orders = repo.GetAllOrders().Select(x => new OrderViewModel
            {
                Customer = x.Customer,
                Location = x.Location,
                Time = x.Time,
                Items = x.Items
            }
            );

            return View(orders);
        }
        public IActionResult Create()
        {
            var customers = repo.GetAllCustomers().Select(x => new CustomerViewModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email
            }).ToList();

            var locations = repo.GetAllLocations().Select(x => new LocationViewModel
            {
                Name = x.Name
            }).ToList();

            ViewData["Customers"] = customers;
            ViewData["Locations"] = locations;

            return View();
        }
        [HttpPost]
        public IActionResult Create(OrderViewModel viewModel)
        {
            return View();
        }
    }
}
