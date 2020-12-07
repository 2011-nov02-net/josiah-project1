using Microsoft.AspNetCore.Mvc;
using StoreApp.Domain.Interfaces;
using System;

namespace StoreApp.WebApp.Controllers
{
    public class ProductsController : Controller
    {
        private IStoreRepository repo { get; }
        public ProductsController(IStoreRepository Repo) =>
            repo = Repo ?? throw new ArgumentNullException(nameof(repo));
        public IActionResult Index()
        {
            return View();
        }
    }
}
