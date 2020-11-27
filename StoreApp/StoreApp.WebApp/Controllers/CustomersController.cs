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
        public ICustomerRepository repo { get; }
        public CustomersController(ICustomerRepository Repo) =>
            repo = Repo ?? throw new ArgumentNullException(nameof(repo));

        public IActionResult Index()
        {
            var customers = repo.GetAllCustomers().Select(x => new CustomerViewModel
            {
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
