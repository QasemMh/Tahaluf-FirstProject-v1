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
    public class CustomersController : Controller
    {
        private readonly ModelContext _context;
        readonly IWebHostEnvironment _hostEnvironment;
        public CustomersController(ModelContext context, IWebHostEnvironment host)
        {
            _context = context;
            _hostEnvironment = host;
        }

        public async Task<IActionResult> Index()
        {
            if (!HttpContext.Session.GetInt32("customer").HasValue)
            {
                return RedirectToAction("Login", "Authentication");
            }
            int CustomerId = (int)HttpContext.Session.GetInt32("customer");

            var products = await _context.Products.Where(p => p.CustomerId == CustomerId).
                             ToListAsync();
            int sum = 0;
            ViewBag.productCount = products.Count;

            products.Where(p => p.Sale != null).
                ToList().ForEach(s => { sum += (int)s.Sale; });
            ViewBag.productSales = sum;

            return View(await _context.Customers.Where(c => c.Id == CustomerId).FirstOrDefaultAsync());
        }

        // GET: Customers
        public async Task<IActionResult> Profile()
        {
            if (!HttpContext.Session.GetInt32("customer").HasValue)
            {
                return RedirectToAction("Login", "Authentication");
            }
            int CustomerId = (int)HttpContext.Session.GetInt32("customer");

            var modelContext = _context.Customers.Where(c => c.Id == CustomerId).
                Include(c => c.Address);


            var username = await _context.Logins.Where(l => l.CustomerId == CustomerId).FirstOrDefaultAsync();
            ViewBag.username = username.Username;

            return View(await modelContext.FirstOrDefaultAsync());
        }



        public async Task<IActionResult> Edit()
        {
            if (!HttpContext.Session.GetInt32("customer").HasValue)
            {
                return RedirectToAction("Login", "Authentication");
            }
            int CustomerId = (int)HttpContext.Session.GetInt32("customer");

            var customer = await _context.Customers.Where(c => c.Id == CustomerId).
                FirstOrDefaultAsync();
            if (customer == null)
            {
                return NotFound();
            }
            ViewData["AddressId"] = new SelectList(_context.Customeraddresses, "Id", "City", customer.AddressId);
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Customer customer, string imagePath2)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Request.Form.Files.Count > 0)
                    {
                        var img = customer.Image;
                        string wwwRootPath = _hostEnvironment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "__" + img.FileName;
                        //save omage name in db
                        customer.Imagepath = fileName;
                        var path = Path.Combine(wwwRootPath + "/images/" + fileName);
                        //save image in server
                        //System.IO.File.Create(path);
                        using (var st = new FileStream(path, FileMode.Create))
                        {
                            await img.CopyToAsync(st);
                        }
                    }
                    else
                    {
                        customer.Imagepath = imagePath2;
                    }

                    _context.Customers.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
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
            ViewData["AddressId"] = new SelectList(_context.Customeraddresses, "Id", "City", customer.AddressId);
            return View(customer);
        }


        private bool CustomerExists(long id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
