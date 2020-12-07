using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StoreApp.Domain.Interfaces;
using System;

namespace StoreApp.WebApp.Controllers
{
    public class ProductsController : Controller
    {
        private IStoreRepository repo { get; }
        private readonly ILogger<ProductsController> logger;

        public ProductsController(IStoreRepository Repo, ILogger<ProductsController> Logger)
        {
            repo = Repo ?? throw new ArgumentNullException(nameof(repo));
            logger = Logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
