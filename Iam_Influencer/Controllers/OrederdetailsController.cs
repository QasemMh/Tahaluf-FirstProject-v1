using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Iam_Influencer.Models;

namespace Iam_Influencer.Controllers
{
    public class OrederdetailsController : Controller
    {
        private readonly ModelContext _context;

        public OrederdetailsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Orederdetails
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Orederdetails.Include(o => o.Customer).Include(o => o.Paymentdetails).Include(o => o.Shipping).Include(o => o.Stutascode);
            return View(await modelContext.ToListAsync());
        }

        // GET: Orederdetails/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orederdetail = await _context.Orederdetails
                .Include(o => o.Customer)
                .Include(o => o.Paymentdetails)
                .Include(o => o.Shipping)
                .Include(o => o.Stutascode)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orederdetail == null)
            {
                return NotFound();
            }

            return View(orederdetail);
        }

        // GET: Orederdetails/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Firstname");
            ViewData["PaymentdetailsId"] = new SelectList(_context.Paymentdetails, "Id", "AccountId");
            ViewData["ShippingId"] = new SelectList(_context.Shippings, "Id", "Method");
            ViewData["StutascodeId"] = new SelectList(_context.Stutascodes, "Id", "Stutas");
            return View();
        }

        // POST: Orederdetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Total,Createddate,PaymentdetailsId,ShippingId,StutascodeId,CustomerId")] Orederdetail orederdetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orederdetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Firstname", orederdetail.CustomerId);
            ViewData["PaymentdetailsId"] = new SelectList(_context.Paymentdetails, "Id", "AccountId", orederdetail.PaymentdetailsId);
            ViewData["ShippingId"] = new SelectList(_context.Shippings, "Id", "Method", orederdetail.ShippingId);
            ViewData["StutascodeId"] = new SelectList(_context.Stutascodes, "Id", "Stutas", orederdetail.StutascodeId);
            return View(orederdetail);
        }

        // GET: Orederdetails/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orederdetail = await _context.Orederdetails.FindAsync(id);
            if (orederdetail == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Firstname", orederdetail.CustomerId);
            ViewData["PaymentdetailsId"] = new SelectList(_context.Paymentdetails, "Id", "AccountId", orederdetail.PaymentdetailsId);
            ViewData["ShippingId"] = new SelectList(_context.Shippings, "Id", "Method", orederdetail.ShippingId);
            ViewData["StutascodeId"] = new SelectList(_context.Stutascodes, "Id", "Stutas", orederdetail.StutascodeId);
            return View(orederdetail);
        }

        // POST: Orederdetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Total,Createddate,PaymentdetailsId,ShippingId,StutascodeId,CustomerId")] Orederdetail orederdetail)
        {
            if (id != orederdetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orederdetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrederdetailExists(orederdetail.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Firstname", orederdetail.CustomerId);
            ViewData["PaymentdetailsId"] = new SelectList(_context.Paymentdetails, "Id", "AccountId", orederdetail.PaymentdetailsId);
            ViewData["ShippingId"] = new SelectList(_context.Shippings, "Id", "Method", orederdetail.ShippingId);
            ViewData["StutascodeId"] = new SelectList(_context.Stutascodes, "Id", "Stutas", orederdetail.StutascodeId);
            return View(orederdetail);
        }

        // GET: Orederdetails/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orederdetail = await _context.Orederdetails
                .Include(o => o.Customer)
                .Include(o => o.Paymentdetails)
                .Include(o => o.Shipping)
                .Include(o => o.Stutascode)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orederdetail == null)
            {
                return NotFound();
            }

            return View(orederdetail);
        }

        // POST: Orederdetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var orederdetail = await _context.Orederdetails.FindAsync(id);
            _context.Orederdetails.Remove(orederdetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrederdetailExists(long id)
        {
            return _context.Orederdetails.Any(e => e.Id == id);
        }
    }
}
