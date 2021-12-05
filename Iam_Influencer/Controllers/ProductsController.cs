using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Iam_Influencer.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Iam_Influencer.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ModelContext _context;
        readonly IWebHostEnvironment _hostEnvironment;
        public ProductsController(ModelContext context, IWebHostEnvironment host)
        {
            _context = context;
            _hostEnvironment = host;
        }
        // GET: Products
        public async Task<IActionResult> Index()
        {
            if (!HttpContext.Session.GetInt32("customer").HasValue)
            {
                return RedirectToAction("Login", "Authentication");
            }
            int CustomerId = (int)HttpContext.Session.GetInt32("customer");

            var modelContext = _context.Products.Include(p => p.Category).
                Include(p => p.Customer).Where(c => c.CustomerId == CustomerId);
            return View(await modelContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {

            if (!HttpContext.Session.GetInt32("customer").HasValue)
            {
                return RedirectToAction("Login", "Authentication");
            }
            int CustomerId = (int)HttpContext.Session.GetInt32("customer");
            ViewBag.customerId = CustomerId;

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            //ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Firstname");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Sale,Price,CategoryId,CustomerId,Image")] Product product)
        {
            if (ModelState.IsValid)
            {
                var img = product.Image;
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Guid.NewGuid().ToString() + "_" + img.FileName;
                //save omage name in db
                product.Imagepath = fileName;
                var path = Path.Combine(wwwRootPath + "/images/" + fileName);
                //save image in server
                using (var st = new FileStream(path, FileMode.Create))
                {
                    await img.CopyToAsync(st);
                }

                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            //ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Firstname", product.CustomerId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (!HttpContext.Session.GetInt32("customer").HasValue)
            {
                return RedirectToAction("Login", "Authentication");
            }
            int CustomerId = (int)HttpContext.Session.GetInt32("customer");
            ViewBag.customerId = CustomerId;

            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            //ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Firstname", product.CustomerId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, string imagePath2, [Bind("Id,Name,Imagepath,Sale,Price,CategoryId,CustomerId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Request.Form.Files.Count > 0)
                    {
                        var img = product.Image;
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "__" + img.FileName;
                        //save omage name in db
                        product.Imagepath = fileName;
                        var path = Path.Combine(wwwRootPath + "/images/" + fileName);
                        //save image in server
                        using (var st = new FileStream(path, FileMode.Create))
                        {
                            await img.CopyToAsync(st);
                        }
                    }
                    else
                    {
                        product.Imagepath = imagePath2;
                    }
                    _context.Products.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            //ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Firstname", product.CustomerId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {

            if (!HttpContext.Session.GetInt32("customer").HasValue)
            {
                return RedirectToAction("Login", "Authentication");
            }
            int CustomerId = (int)HttpContext.Session.GetInt32("customer");
            ViewBag.customerId = CustomerId;

            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
    .Include(p => p.Category)
    .Include(p => p.Customer)
    .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(long id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
