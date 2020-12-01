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

        public IActionResult Index(string searchString)
        {
            var customers = repo.GetAllCustomers().Select(x => new CustomerViewModel
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email
            });

            if (!string.IsNullOrEmpty(searchString))
            {
                customers = customers
                    .Where(x => x.FirstName.Contains(searchString) || x.LastName.Contains(searchString));
            }
            return View(customers);
            
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CustomerViewModel viewModel)
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
                    repo.addCustomer(customer);

                    return RedirectToAction(nameof(Index));
                }
                return View(viewModel);
            }
            catch
            {
                return View(viewModel);
            }
        }

    }
}
