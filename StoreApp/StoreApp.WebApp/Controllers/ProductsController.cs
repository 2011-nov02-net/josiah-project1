using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreApp.Domain.Interfaces;

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
