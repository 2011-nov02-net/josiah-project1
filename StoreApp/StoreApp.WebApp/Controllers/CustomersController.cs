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
    public class CustomersController : Controller
    {
        private IStoreRepository repo { get; }
        public CustomersController(IStoreRepository Repo) =>
            repo = Repo ?? throw new ArgumentNullException(nameof(repo));

        public async Task<IActionResult> Index(string searchString)
        {
            IEnumerable<Customer> data;
            if (!string.IsNullOrEmpty(searchString))
            {
                data = await repo.SearchCustomersAsync(searchString);
            }
            else
            {
                data = await repo.GetAllCustomersAsync();
            }
            var customers = data.Select(x => new CustomerViewModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email
            });

            return View(customers);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CustomerViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var customer = new Customer
                    {
                        FirstName = viewModel.FirstName,
                        LastName = viewModel.LastName,
                        Email = viewModel.Email
                    };
                    await repo.AddCustomerAsync(customer);

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
            var data = await repo.GetOrdersByCustomerAsync(id);
            var orders = data.Select(x => new OrderViewModel
            {
                Id = x.Id,
                Location = x.Location,
                Customer = x.Customer,
                Time = x.Time,
                Items = x.Items
            }).ToList();

            ViewData["Orders"] = orders;

            return View();
        }
    }
}
