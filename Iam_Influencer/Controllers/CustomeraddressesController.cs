using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Iam_Influencer.Models;
using Microsoft.AspNetCore.Http;

namespace Iam_Influencer.Controllers
{

    public class CustomeraddressesController : Controller
    {
        private readonly ModelContext _context;

        public CustomeraddressesController(ModelContext context)
        {
            _context = context;
        }


        // GET: Customeraddresses/Details/5
        public async Task<IActionResult> Details()
        {
            if (!HttpContext.Session.GetInt32("customer").HasValue)
            {
                return RedirectToAction("Login", "Authentication");
            }
            int customerId = (int)HttpContext.Session.GetInt32("customer");
            var customer = await _context.Customers.Where(c => c.Id == customerId).FirstOrDefaultAsync();

            if (customer.AddressId == null)
            {
                return NotFound();
            }

            var customeraddress = await _context.Customeraddresses
                .FirstOrDefaultAsync(m => m.Id == customer.AddressId);
            if (customeraddress == null)
            {
                return NotFound();
            }

            return View(customeraddress);
        }

        // GET: Customeraddresses/Create
        public IActionResult Create()
        {
            if (!HttpContext.Session.GetInt32("customer").HasValue)
            {
                return RedirectToAction("Login", "Authentication");
            }
            int CustomerId = (int)HttpContext.Session.GetInt32("customer");
            ViewBag.customerId = CustomerId;
            return View();
        }

        // POST: Customeraddresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Countray,City,Street,Phone,Email")] Customeraddress customeraddress,
            int customerId)
        {
            if (ModelState.IsValid)
            {
                var customer = await _context.Customers.Where(c => c.Id == customerId).FirstOrDefaultAsync();
                if (customer == null) return NotFound();

                _context.Customeraddresses.Add(customeraddress);
                await _context.SaveChangesAsync();

                customer.AddressId = customeraddress.Id;
                _context.Customers.Update(customer);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Details));
            }

            int CustomerId = (int)HttpContext.Session.GetInt32("customer");
            ViewBag.customerId = CustomerId;
            return View(customeraddress);
        }

        // GET: Customeraddresses/Edit/5
        public async Task<IActionResult> Edit()
        {
            if (!HttpContext.Session.GetInt32("customer").HasValue)
            {
                return RedirectToAction("Login", "Authentication");
            }
            int customerId = (int)HttpContext.Session.GetInt32("customer");
            var customer = await _context.Customers.Where(c => c.Id == customerId).FirstOrDefaultAsync();
            if (customer.AddressId == null)
            {
                return NotFound();
            }

            var customeraddress = await _context.Customeraddresses.FindAsync(customer.AddressId);
            if (customeraddress == null)
            {
                return NotFound();
            }
            return View(customeraddress);
        }

        // POST: Customeraddresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Countray,City,Street,Phone,Email")] Customeraddress customeraddress)
        {
            if (id != customeraddress.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Customeraddresses.Update(customeraddress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomeraddressExists(customeraddress.Id))
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
            return View(customeraddress);
        }

        // GET: Customeraddresses/Delete/5
        public async Task<IActionResult> Delete()
        {
            if (!HttpContext.Session.GetInt32("customer").HasValue)
            {
                return RedirectToAction("Login", "Authentication");
            }
            int customerId = (int)HttpContext.Session.GetInt32("customer");
            var customer = await _context.Customers.Where(c => c.Id == customerId).FirstOrDefaultAsync();

            if (customer.AddressId == null)
            {
                return NotFound();
            }

            var customeraddress = await _context.Customeraddresses
                .FirstOrDefaultAsync(m => m.Id == customer.AddressId);
            if (customeraddress == null)
            {
                return NotFound();
            }

            return View(customeraddress);
        }

        // POST: Customeraddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var customeraddress = await _context.Customeraddresses.FindAsync(id);
            _context.Customeraddresses.Remove(customeraddress);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomeraddressExists(long id)
        {
            return _context.Customeraddresses.Any(e => e.Id == id);
        }
    }
}
