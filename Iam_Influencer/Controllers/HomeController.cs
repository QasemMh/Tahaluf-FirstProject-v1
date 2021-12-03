using Iam_Influencer.Models;
using Iam_Influencer.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Iam_Influencer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ModelContext _context;

        public HomeController(ModelContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var slider = await _context.Sliders.ToListAsync();
            var reviews = await _context.Reviews.ToListAsync();
            var products = await _context.Products.OrderByDescending(by => by.Id).
                Take(10).ToListAsync();


            ContentViewModel viewModel = new ContentViewModel();

            viewModel.Sliders = slider;
            viewModel.Reviews = reviews;
            viewModel.Products = products;

            //reviews.ForEach(r =>
            //{
            //    viewModel.Id = r.Id;
            //    viewModel.Text = r.Text;
            //    viewModel.Title = r.Title;
            //    viewModel.Imagepath = r.Imagepath;
            //    viewModel.Name = r.Name;
            //    viewModel.Starcount = r.Starcount;
            //});

            return View(viewModel);
        }

        public async Task<IActionResult> Products(int? pageNumber)
        {
            ProductViewModel viewModel = new ProductViewModel();
            viewModel.products = await _context.Products.ToListAsync();

            int pageSize = 10;
            return View(await PaginatedList<Product>.
                CreateAsync(_context.Products,
                pageNumber ?? 1, pageSize));

        }

        public async Task<IActionResult> ViewProducts(int? id)
        {
            if (id == null) return NotFound();

            ProductViewModel viewModel = new ProductViewModel();

            var product = await _context.Products.
                Where(p => p.Id == id).FirstOrDefaultAsync();

            if (product == null) return NotFound();

            viewModel.product = product;

            var category = await _context.Categories.
                Where(cat => cat.Id == product.CategoryId).FirstOrDefaultAsync();
            ViewData["category"] = category.Name;

            var sameProducts = await _context.Products.
                Where(cat => cat.CategoryId == category.Id).Take(10).ToListAsync();
            viewModel.newProducts = sameProducts;
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
