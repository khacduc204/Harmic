﻿using Harmic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Harmic.Controllers
{
    public class ProductController : Controller
    {
        private readonly HarmicContext _context;

        public ProductController(HarmicContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Route("/product/{alias}-{id}.html")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TbProducts == null)
            {
                return NotFound();
            }
            var product = await _context.TbProducts.Include(i => i.CategoryProduct).FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.productid = id;
            ViewBag.ProductReview = _context.TbProductReviews.Where(i => i.ProductId == id && i.IsActive).ToList();
            ViewBag.ProductRelated = _context.TbProducts.Where(i => i.ProductId != id && i.CategoryProductId == product.CategoryProductId).Take(5).OrderByDescending(i => i.ProductId).ToList();
            ViewBag.CategoryProduct = _context.TbProductCategories.Where(i => i.CategoryProductId == product.CategoryProductId && i.IsActive).ToList();
            return View(product);
        }
    }
}
