﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreApp.Domain.Interfaces;
using StoreApp.Domain.Model;
using StoreApp.WebApp.Helpers;
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
        /// <summary>
        /// returns a view containing all orders with their details
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var data = await repo.GetAllOrdersAsync();
            var orders = data.Select(x => new OrderViewModel
            {
                Customer = x.Customer,
                Location = x.Location,
                Time = x.Time,
                Items = x.Items,
                Price = OrderViewModel.getPrice(x.Items)
            });
            return View(orders);
        }
        /// <summary>
        /// returns a view containing drop down fields to place an order, redirects to addproduct
        /// when pressing the add product button and redirects to finalize, when create order is pressed
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task<IActionResult> Create(PlaceOrderViewModel viewModel)
        {
            if (viewModel.New)
            {
                var custData = await repo.GetAllCustomersAsync();
                var customers = custData.Select(x => new Customer
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email
                }).ToList();

                var locData = await repo.GetAllLocationsAsync();
                var locations = locData.Select(x => new Location
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();

                var prodData = await repo.GetAllProductsAsync();
                var products = prodData.Select(x => new Product
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price
                }).ToList();

                viewModel.Customers = customers;
                viewModel.Locations = locations;
                viewModel.Products = products;
                viewModel.New = false;
            }
            else
            {
                viewModel = TempData.Get<PlaceOrderViewModel>("Model");
            }

            return View(viewModel);
        }

        /// <summary>
        /// adds the selected product and amount to the cart which is then displayed to the user in the create view
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        public IActionResult AddProduct(PlaceOrderViewModel viewModel)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var chosenProduct = viewModel.chosenProductId;
                    var chosenCustomer = viewModel.chosenCustomerId;
                    var chosenLocation = viewModel.chosenLocationId;
                    var chosenProductAmount = viewModel.chosenProductAmount;

                    viewModel = TempData.Get<PlaceOrderViewModel>("Model");

                    var product = viewModel.Products.Where(x => x.Id == chosenProduct).First();
                    var new_product = new Product
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price
                    };

                    viewModel.CartItems.Add(product);
                    viewModel.CartAmounts.Add(chosenProductAmount);
                    viewModel.chosenProductAmount = chosenProductAmount;
                    viewModel.chosenProductId = chosenProduct;
                    viewModel.chosenLocationId = chosenLocation;
                    viewModel.chosenCustomerId = chosenCustomer;

                    TempData.Put("Model", viewModel);

                    return RedirectToAction(nameof(Create), viewModel);
                }
                else
                {
                    return RedirectToAction(nameof(Create), viewModel);
                }
            }
            catch (Exception e)
            {
                return RedirectToAction(nameof(Create), viewModel);
            }
        }
        /// <summary>
        /// takes the list of items in the cart by the user and creates the order, then redirects to index
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Finalize(PlaceOrderViewModel viewModel)
        {
            // TODO: validate final chosen product with the chosen location id.
            // if not valid, return viewmodel to create view with error message
            // if valid, add final product to cart and create an order


            //try
            {
                //var chosenProduct = viewModel.chosenProductId;
                var chosenCustomer = viewModel.chosenCustomerId;
                var chosenLocation = viewModel.chosenLocationId;
                //var chosenProductAmount = viewModel.chosenProductAmount;

                viewModel = TempData.Get<PlaceOrderViewModel>("Model");

                //var product = viewModel.Products.Where(x => x.Id == chosenProduct).First();
                //var new_product = new Product
                //{
                //    Id = product.Id,
                //    Name = product.Name,
                //    Price = product.Price
                //};

                //viewModel.CartItems.Add(product);
                //viewModel.CartAmounts.Add(chosenProductAmount);
                //viewModel.chosenProductId = chosenProduct;
                viewModel.chosenLocationId = chosenLocation;
                viewModel.chosenCustomerId = chosenCustomer;

                viewModel.Customer = viewModel.Customers.Where(x => x.Id == chosenCustomer).First();
                viewModel.Location = viewModel.Locations.Where(x => x.Id == chosenLocation).First();

                await repo.AddOrderAsync(OrderHelper.ViewToOrder(viewModel));

                return RedirectToAction(nameof(Index));
            }
            //catch
            //{
            //    return RedirectToAction(nameof(Index));
            //}

        }
    }
}
