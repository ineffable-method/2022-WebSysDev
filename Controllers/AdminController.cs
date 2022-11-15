﻿using Microsoft.AspNetCore.Mvc;
using Tuskla.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
namespace Tuskla.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repository;
        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index() => View(repository.Products.Where(p => !p.Category.StartsWith("Car")));

        public ViewResult Index2() => View(repository.Products.Where(p => !p.Category.StartsWith("Car")));

        public ViewResult Index3() => View(repository.Products.Where(p => !p.Category.StartsWith("Car")));

        public ViewResult Index4() => View(repository.Products.Where(p => !p.Category.StartsWith("Car")));


        public ViewResult Edit(int productId) => View(repository.Products.FirstOrDefault(p => p.ProductID == productId));
        
        [HttpPost]
        public IActionResult Edit(ProductModelView product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been saved";
                return RedirectToAction("Index3");
            }
            else
            {
                // there is something wrong with the data values
                return View(product);
            }
        }
        public ViewResult AddProduct() => View("AddProduct", new ProductModelView());

        [HttpPost]
        public IActionResult AddProduct(ProductModelView product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been saved";
                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(product);
            }
        }


        [HttpPost]
        public IActionResult Delete(int productId)
        {
            ProductModelView deletedProduct = repository.DeleteProduct(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = $"{deletedProduct.Name} was deleted";
            }
            return RedirectToAction("Index");
        }

        public ViewResult MainAdmin()
        {
            return View("MainAdmin");
        }

    }
}
