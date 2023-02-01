﻿using GeekShooping.Web.Models;
using GeekShooping.Web.Services.IServices;
using GeekShooping.Web.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekShooping.Web.Controllers
{
    public class ProductController : Controller
    {

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        // só utilizar o auhtorize quando o identity server estive pronto.
        //[Authorize] permitindo ousuário ver os product sem precisar está logado
        public async Task<IActionResult> ProductIndex()
        {
            // só utilizar o auhtorize quando o identity server estive pronto. com JWT
            
            var products = await _productService.FindAllProducts("");
            return View(products);
        }
        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var token = await HttpContext.GetTokenAsync("access_token");

                var response = await _productService.CreateProduct(model, token);
                if (response != null) return RedirectToAction(
                     nameof(ProductIndex));
            }
            return View(model);
        }

        public async Task<IActionResult> ProductUpdate(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            var products = await _productService.FindProductById(id, token);
            if (products != null) return View(products);
            return NotFound();

        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ProductUpdate(ProductModel model)
        {
            if (ModelState.IsValid)
            {
                var token = await HttpContext.GetTokenAsync("access_token");

                var response = await _productService.UpdateProduct(model, token);
                if (response != null) return RedirectToAction(
                     nameof(ProductIndex));
            }
            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> ProductDelete(int id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var products = await _productService.FindProductById(id, token);
            if (products != null) return View(products);
            return NotFound();

        }
        
        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> ProductDelete(ProductModel model)
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.DeleteProductById(model.Id, token);
            if (response) return RedirectToAction(
                 nameof(ProductIndex));

            return View(model);
        }
    }
}
