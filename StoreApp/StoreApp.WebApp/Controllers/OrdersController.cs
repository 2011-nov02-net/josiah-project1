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
        public IOrderRepository repo { get; }
        public OrdersController(IOrderRepository Repo) =>
            repo = Repo ?? throw new ArgumentNullException(nameof(repo));

        public IActionResult Index()
        {
            var orders = repo.GetAllOrders().Select(x => new Order
            {

            }
            );

            return View();
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(OrderViewModel viewModel)
        {
            return View();
        }
    }
}
