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
    public class OrederitemsController : Controller
    {
        private readonly ModelContext _context;

        public OrederitemsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Orederitems
        public async Task<IActionResult> Index()
        {
            if (!HttpContext.Session.GetInt32("customer").HasValue)
            {
                return RedirectToAction("Login", "Authentication");
            }
            int CustomerId = (int)HttpContext.Session.GetInt32("customer");

            var order = await _context.Orederdetails.
                Where(c => c.CustomerId == CustomerId).FirstOrDefaultAsync();

            var modelContext = _context.Orederitems.
                Include(o => o.Orederdetails).Include(o => o.Product)
                .Where(or => or.OrederdetailsId == order.Id);

            return View(await modelContext.ToListAsync());
        }

        // GET: Orederitems/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orederitem = await _context.Orederitems
                .Include(o => o.Orederdetails)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orederitem == null)
            {
                return NotFound();
            }

            return View(orederitem);
        }

        // GET: Orederitems/Create
        public IActionResult Create()
        {
            ViewData["OrederdetailsId"] = new SelectList(_context.Orederdetails, "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: Orederitems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Quntity,Createddate,ProductId,OrederdetailsId")] Orederitem orederitem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orederitem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrederdetailsId"] = new SelectList(_context.Orederdetails, "Id", "Id", orederitem.OrederdetailsId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", orederitem.ProductId);
            return View(orederitem);
        }

        // GET: Orederitems/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orederitem = await _context.Orederitems.FindAsync(id);
            if (orederitem == null)
            {
                return NotFound();
            }
            ViewData["OrederdetailsId"] = new SelectList(_context.Orederdetails, "Id", "Id", orederitem.OrederdetailsId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", orederitem.ProductId);
            return View(orederitem);
        }

        // POST: Orederitems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Quntity,Createddate,ProductId,OrederdetailsId")] Orederitem orederitem)
        {
            if (id != orederitem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orederitem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrederitemExists(orederitem.Id))
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
            ViewData["OrederdetailsId"] = new SelectList(_context.Orederdetails, "Id", "Id", orederitem.OrederdetailsId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", orederitem.ProductId);
            return View(orederitem);
        }

        // GET: Orederitems/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orederitem = await _context.Orederitems
                .Include(o => o.Orederdetails)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orederitem == null)
            {
                return NotFound();
            }

            return View(orederitem);
        }

        // POST: Orederitems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var orederitem = await _context.Orederitems.FindAsync(id);
            _context.Orederitems.Remove(orederitem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrederitemExists(long id)
        {
            return _context.Orederitems.Any(e => e.Id == id);
        }
    }
}
