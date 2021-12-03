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
    public class CustomerpaymentsController : Controller
    {
        private readonly ModelContext _context;

        public CustomerpaymentsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Customerpayments
        public async Task<IActionResult> Index()
        {
            if (!HttpContext.Session.GetInt32("customer").HasValue)
            {
                return RedirectToAction("Login", "Authentication");
            }
            int customerId = (int)HttpContext.Session.GetInt32("customer");

            var modelContext = _context.Customerpayments.
                Where(c => c.CustomerId == customerId).Include(c => c.Customer);

            return View(await modelContext.ToListAsync());
        }

        // GET: Customerpayments/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerpayment = await _context.Customerpayments
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerpayment == null)
            {
                return NotFound();
            }

            return View(customerpayment);
        }

        // GET: Customerpayments/Create
        public IActionResult Create()
        {
            if (!HttpContext.Session.GetInt32("customer").HasValue)
            {
                return RedirectToAction("Login", "Authentication");
            }
            int customerId = (int)HttpContext.Session.GetInt32("customer");
            ViewData["CustomerId"] = customerId;

            return View();
        }

        // POST: Customerpayments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Balance,AccountId,ExpierDate,PaymentType,Provider,CustomerId")] Customerpayment customerpayment)
        {
            if (ModelState.IsValid)
            {
                _context.Customerpayments.Add(customerpayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            if (!HttpContext.Session.GetInt32("customer").HasValue)
            {
                return RedirectToAction("Login", "Authentication");
            }
            int customerId = (int)HttpContext.Session.GetInt32("customer");
            ViewData["CustomerId"] = customerId;
            return View(customerpayment);
        }

        // GET: Customerpayments/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerpayment = await _context.Customerpayments.FindAsync(id);
            if (customerpayment == null)
            {
                return NotFound();
            }
            if (!HttpContext.Session.GetInt32("customer").HasValue)
            {
                return RedirectToAction("Login", "Authentication");
            }
            int customerId = (int)HttpContext.Session.GetInt32("customer");
            ViewData["CustomerId"] = customerId;
            return View(customerpayment);
        }

        // POST: Customerpayments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,AccountId,ExpierDate,PaymentType,Provider,Balance,CustomerId")] Customerpayment customerpayment)
        {
            if (id != customerpayment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Customerpayments.Update(customerpayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerpaymentExists(customerpayment.Id))
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

            if (!HttpContext.Session.GetInt32("customer").HasValue)
            {
                return RedirectToAction("Login", "Authentication");
            }
            int customerId = (int)HttpContext.Session.GetInt32("customer");
            ViewData["CustomerId"] = customerId;
            return View(customerpayment);
        }

        // GET: Customerpayments/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerpayment = await _context.Customerpayments
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customerpayment == null)
            {
                return NotFound();
            }

            return View(customerpayment);
        }

        // POST: Customerpayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var customerpayment = await _context.Customerpayments.FindAsync(id);
            _context.Customerpayments.Remove(customerpayment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerpaymentExists(long id)
        {
            return _context.Customerpayments.Any(e => e.Id == id);
        }
    }
}
