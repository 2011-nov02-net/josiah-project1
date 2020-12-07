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
        
        /// <summary>
        /// Index of all customers, contains a search box so you can filter through customer names
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
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
        /// <summary>
        /// returns view for a form to create a new customer
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// validates the form from the user, then redirects to the index page
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        /// <summary>
        /// returns view containing all the orders by a customer through their id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Orders(int id)
        {
            var data = await repo.GetOrdersByCustomerAsync(id);

            Dictionary<Order, double> prices = new Dictionary<Order, double>();

            var orders = data.Select(x => new OrderViewModel
            {
                Id = x.Id,
                Location = x.Location,
                Customer = x.Customer,
                Time = x.Time,
                Items = x.Items,
                Price = OrderViewModel.getPrice(x.Items)
            }).ToList();

            ViewData["Orders"] = orders;

            var data2 = await repo.GetCustomerByIdAsync(id);

            var customer = new CustomerViewModel
            {
                Id = data2.Id,
                FirstName = data2.FirstName,
                LastName = data2.LastName,
                Email = data2.Email
            };
            return View(customer);
        }

    }
}
